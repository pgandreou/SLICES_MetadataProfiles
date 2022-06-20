using System.Globalization;
using Slices.Common;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite;

public class DataCiteImporter : ISlicesImporter<DataCiteResource>
{
    private static readonly IReadOnlyDictionary<DataCiteResourceTypeGeneral, SfdoResourceType> ResourceTypeMapping =
        new Dictionary<DataCiteResourceTypeGeneral, SfdoResourceType>
        {
            [DataCiteResourceTypeGeneral.Book] = SfdoResourceType.Publication,
            [DataCiteResourceTypeGeneral.ConferencePaper] = SfdoResourceType.Publication,
            [DataCiteResourceTypeGeneral.Dissertation] = SfdoResourceType.Publication,
            [DataCiteResourceTypeGeneral.JournalArticle] = SfdoResourceType.Publication,
            [DataCiteResourceTypeGeneral.Preprint] = SfdoResourceType.Publication,

            [DataCiteResourceTypeGeneral.Dataset] = SfdoResourceType.Data,
        };

    public SfdoResource FromExternal(DataCiteResource externalModel)
    {
        LocalizedVariationSelector localizedSelector = new()
        {
            ResourceLang = externalModel.language,
            GeneralizeLanguageCallback = GeneralizeLang,
        };
        
        SfdoResource sfdo = new();

        sfdo.Identifier = new()
        {
            Type = externalModel.identifier.identifierType,
            Value = externalModel.identifier.Value
        };

        sfdo.AlternateIdentifiers = externalModel.alternateIdentifiers
            .Select(id => new SfdoIdentifier { Type = id.Type, Value = id.Value })
            .ToList();

        sfdo.Creators = externalModel.creators
            .Select(c => new SfdoCreator
            {
                Name = c.creatorName.Value,
                Identifier = PickIdentifierForCreatorLike(c.nameIdentifier),
            })
            .ToList();

        if (localizedSelector.PickBest(externalModel.titles, t => t.lang, out DataCiteResourceTitle? title))
        {
            sfdo.Name = title.Value;
        }

        if (localizedSelector.PickBest(externalModel.descriptions, t => t.lang, out DataCiteResourceDescription? description))
        {
            sfdo.Description = description.Text;
        }

        if (ResourceTypeMapping.TryGetValue(externalModel.resourceType.GeneralType, out SfdoResourceType sfdoType))
        {
            sfdo.ResourceTypes.Add(sfdoType);
        }

        sfdo.Subjects = localizedSelector.PickBest(externalModel.subjects, s => s.lang)
            .Select(s => s.Value)
            .ToList();

        // TODO: keywords

        //slicesObject.DateTimeCreated = DateTime.Now;

        sfdo.Version = externalModel.version;

        if (externalModel.contributors != null)
        {
            sfdo.Contributors = externalModel.contributors
                .Select(c => new SfdoContributor
                {
                    Name = c.contributorName.Value,
                    Identifier = PickIdentifierForCreatorLike(c.nameIdentifier),
                })
                .ToList();
        }

        sfdo.RelatedObjects = externalModel.relatedIdentifiers
            .Select(ri => new SfdoRelationLink
            {
                Identifier = new SfdoIdentifier { Type = ri.relatedIdentifierType.ToString(), Value = ri.Value },
                RelationshipType = ri.relationType.ToString(),
                ResourceType = ri.resourceTypeGeneralSpecified ? ri.relatedIdentifierType.ToString() : null,
            })
            .ToList();

        if (externalModel.language != null)
        {
            try
            {
                CultureInfo culture = CultureInfo.GetCultureInfo(externalModel.language);

                sfdo.PrimaryLanguage.Add(new LanguageIso639_3 { Code = culture.ThreeLetterISOLanguageName });
            }
            catch (CultureNotFoundException)
            {
            }
        }

        if (localizedSelector.PickBest(externalModel.rightsList, t => t.lang, out DataCiteResourceRights? rights))
        {
            sfdo.Rights = rights.Value;
            sfdo.RightsURI = new Uri(rights.rightsURI);
        }
        
        if (sfdo.ResourceTypes.ContainsAny(SfdoResourceType.Data, SfdoResourceType.Publication))
        {
            sfdo.ScientificDomains = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.ScientificSubdomains = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
        }
        
        if (sfdo.ResourceTypes.Contains(SfdoResourceType.Publication))
        {
            ILookup<DataCiteDateType, string> datesByType =
                externalModel.dates.ToLookup(date => date.dateType, date => date.Value);
            
            sfdo.DateSubmitted = datesByType[DataCiteDateType.Submitted].Count() switch
            {
                0 => SfdoOptional.WithAbsent("None specified"),
                > 1 => SfdoOptional.WithAbsent("Multiple were specified"),
                
                1 => AttemptParseDate(datesByType[DataCiteDateType.Submitted].Single()),
                
                _ => throw new Exception("ToLookup() produced a bucket with negative Count()"),
            };

            SfdoOptional<List<DateOnly>> GetDates(DataCiteDateType type)
            {
                if (!datesByType[type].Any())
                {
                    // We got none specified at all
                    return SfdoOptional.WithValue(new List<DateOnly>());
                }

                List<DateOnly> parsedDates = datesByType[type]
                    .Select(AttemptParseDate)
                    .Where(optional => optional.IsSet)
                    .Select(optional => optional.Value)
                    .ToList();

                // Ignore those which we failed to parse
                if (parsedDates.Count > 0)
                {
                    return parsedDates;
                }
                
                // All failed parsing
                return SfdoOptional.WithAbsent("Failed to parse the value(s) when importing");
            }
            
            sfdo.DatesModified = GetDates(DataCiteDateType.Updated);
            sfdo.DatesIssued = GetDates(DataCiteDateType.Issued);
            sfdo.DatesAccepted = GetDates(DataCiteDateType.Accepted);
            sfdo.DatesCopyrighted = GetDates(DataCiteDateType.Copyrighted);
        }
        
        if (sfdo.ResourceTypes.Contains(SfdoResourceType.Data))
        {
            sfdo.PaymentModel = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.Pricing = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.Address = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            
            // TODO: there are dates, but no start/end date types
            sfdo.DateTimeStart = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.DateTimeEnd = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);

            sfdo.Locations = (externalModel.geoLocations ?? Enumerable.Empty<DataCiteResourceGeoLocation>())
                .Select(location =>
                {
                    string joinedParts = string.Join(
                        ", ",
                        location.Values.Select(o => o switch
                        {
                            DataCiteGeoBox box =>
                                $"(westBoundLongitude: {box.westBoundLongitude}, eastBoundLongitude: {box.eastBoundLongitude}, southBoundLatitude: {box.southBoundLatitude}, northBoundLatitude: {box.northBoundLatitude})",

                            DataCiteGeoPlace place => $"({place.Value})",
                            DataCiteGeoPoint point => PointToString(point),
                            DataCiteGeoPolygon polygon => PolygonToString(polygon),

                            _ => throw new ArgumentOutOfRangeException(nameof(o), o, null)
                        })
                    );

                    return $"[{joinedParts}]";
                })
                .ToList();
            
            sfdo.Size = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason); // TODO: parse free form string
            sfdo.Duration = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);

            sfdo.Formats = new List<string>(externalModel.formats ?? Array.Empty<string>());
            
            sfdo.Mediums = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.CompressionFormats = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.FileInfo = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
            sfdo.DataStandard = SfdoOptional.WithAbsent(DataCiteConstants.CannotImportReason);
        }

        return sfdo;
    }

    private static SfdoIdentifier? PickIdentifierForCreatorLike(DataCiteNameIdentifier[]? dataCiteNameIdentifiers)
    {
        if (dataCiteNameIdentifiers == null) return null;

        // Try find one with scheme set
        DataCiteNameIdentifier? dcId = dataCiteNameIdentifiers.FirstOrDefault(id => !string.IsNullOrWhiteSpace(id.nameIdentifierScheme));

        // If failed, pick just first
        if (dcId == null)
        {
            dcId = dataCiteNameIdentifiers.FirstOrDefault();
        }

        // If still nothing, then we have no id
        if (dcId == null) return null;

        return new SfdoIdentifier
        {
            Type = dcId.nameIdentifierScheme,
            Value = dcId.Value,
        };
    }

    private static string GeneralizeLang(string lang)
    {
        int separatorIndex = lang.IndexOf("-", StringComparison.Ordinal);

        if (separatorIndex > 0)
        {
            return lang[..separatorIndex];
        }

        return lang;
    }

    private static SfdoOptional<DateOnly> AttemptParseDate(string dateString)
    {
        if (DateOnly.TryParse(dateString, out DateOnly dateOnly))
        {
            return dateOnly;
        }
        
        if (DateTime.TryParse(dateString, out DateTime dateTime))
        {
            return DateOnly.FromDateTime(dateTime);
        }

        return SfdoOptional.WithAbsent("Failed to parse the value when importing");
    }

    private static string PointToString(DataCiteGeoPoint point) 
        => $"({point.pointLatitude}, {point.pointLongitude})";
    
    private static string PolygonToString(DataCiteGeoPolygon polygon)
    {
        IEnumerable<string> parts = polygon.polygonPoint.Select(PointToString);

        if (polygon.inPolygonPoint != null)
        {
            parts = parts.Prepend("in: " + PointToString(polygon.inPolygonPoint));
        }
        
        return $"({string.Join(", ", parts)})";
    }
}
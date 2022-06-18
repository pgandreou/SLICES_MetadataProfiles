using System.Globalization;
using Slices.Common;
using Slices.V1.Converters.Common;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore;

public class DublinCoreImporter : ISlicesImporter<DublinCoreResource>
{
    // TODO: handle lang attribute, fill not nullables on SFDO
    public SfdoResource FromExternal(DublinCoreResource externalModel)
    {
        SfdoResource sfdo = new();

        if (externalModel.Identifiers != null)
        {
            List<SfdoIdentifier> sfdoIdentifiers = externalModel.Identifiers
                .Select(id => TryParseIdentifier(id.Value))
                .Where(id => id != null)
                .Select(id => id!.Value)
                .ToList();

            if (sfdoIdentifiers.Count > 1)
            {
                sfdo.Identifier = sfdoIdentifiers.FirstOrDefault(
                    id => id.Type != null, // Prefer one that has a type parsed
                    sfdoIdentifiers[0] // Otherwise just pick first
                );

                // The rest become alternate identifiers
                sfdoIdentifiers.Remove(sfdo.Identifier);
                sfdo.AlternateIdentifiers = sfdoIdentifiers;
            }
            else if (sfdoIdentifiers.Count > 0)
            {
                // Only 1 ID which becomes the primary (no alts)
                sfdo.Identifier = sfdoIdentifiers[0];
            }
        }

        if (externalModel.Creators != null)
        {
            sfdo.Creators = externalModel.Creators
                .Select(c => new SfdoCreator { Name = c.Value })
                .ToList();
        }

        if (externalModel.Titles != null)
        {
            sfdo.Name = externalModel.Titles.FirstOrDefault()?.Value!;
        }

        if (externalModel.Descriptions != null)
        {
            sfdo.Description = externalModel.Descriptions.FirstOrDefault()?.Value!;
        }

        if (externalModel.Subjects != null)
        {
            sfdo.Subjects = externalModel.Subjects
                .Select(s => s.Value)
                .ToList();
        }

        if (externalModel.Contributors != null)
        {
            sfdo.Contributors = externalModel.Contributors
                .Select(c => new SfdoContributor { Name = c.Value })
                .ToList();
        }

        if (externalModel.Relations != null)
        {
            sfdo.RelatedObjects = externalModel.Relations
                .Select(r => TryParseIdentifier(r.Value))
                .Where(r => r != null)
                .Select(r => new SfdoRelationLink { Identifier = r!.Value })
                .ToList();
        }

        if (externalModel.Languages != null)
        {
            foreach (DublinCoreElement language in externalModel.Languages)
            {
                try
                {
                    CultureInfo cultureInfo = CultureInfo.GetCultureInfo(language.Value);

                    if (cultureInfo != null)
                    {
                        sfdo.PrimaryLanguage.Add(new LanguageIso639_3 { Code = cultureInfo.ThreeLetterISOLanguageName });
                    }
                }
                catch (CultureNotFoundException)
                {
                }
            }
        }

        if (externalModel.Rights != null)
        {
            sfdo.Rights = externalModel.Rights.FirstOrDefault()?.Value!;
        }

        if (externalModel.Types.Any(t => t.Value == "dataset"))
        {
            sfdo.ResourceTypes.Add(SfdoResourceType.Data);
        }

        if (externalModel.Types.Any(t => t.Value.StartsWith("publication", StringComparison.InvariantCultureIgnoreCase)))
        {
            sfdo.ResourceTypes.Add(SfdoResourceType.Publication);
        }

        if (sfdo.ResourceTypes.Contains(SfdoResourceType.Publication))
        {
            sfdo.Publisher = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);

            if (externalModel.Dates.Length == 1)
            {
                sfdo.PublicationYear = DateOnly.TryParse(externalModel.Dates[0].Value, out DateOnly date)
                    ? SfdoOptional.WithValue(date.Year)
                    : SfdoOptional.WithAbsent("Failed to parse date");
            }
            else
            {
                sfdo.PublicationYear = SfdoOptional.WithAbsent(
                    "Failed to import from Dublin Core - multiple dates were specified"
                );
            }
        }

        if (sfdo.ResourceTypes.ContainsAny(SfdoResourceType.Data, SfdoResourceType.Publication))
        {
            sfdo.ScientificDomains = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.ScientificSubdomains = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
        }

        if (sfdo.ResourceTypes.Contains(SfdoResourceType.Publication))
        {
            sfdo.DateSubmitted = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DatesModified = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DatesIssued = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DatesAccepted = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DatesCopyrighted = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
        }

        if (sfdo.ResourceTypes.Contains(SfdoResourceType.Data))
        {
            sfdo.PaymentModel = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Pricing = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Address = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            
            // TODO: there is "date"s, but we don't know which one is which
            sfdo.DateTimeStart = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DateTimeEnd = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            
            sfdo.Locations = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Size = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Duration = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Formats = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.Mediums = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.CompressionFormats = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.FileInfo = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
            sfdo.DataStandard = SfdoOptional.WithAbsent(DublinCoreConstants.CannotImportReason);
        }
        
        return sfdo;
    }

    private static readonly IReadOnlySet<string> SupportedIdentifierTypes = new HashSet<string>
    {
        SfdoIdentifierTypes.Doi,
        SfdoIdentifierTypes.Url,
        SfdoIdentifierTypes.Orcid,
        SfdoIdentifierTypes.Arxiv,
    };

    private static SfdoIdentifier? TryParseIdentifier(string originalIdentifier)
    {
        if (string.IsNullOrWhiteSpace(originalIdentifier)) return null;

        foreach (string identifierType in SupportedIdentifierTypes)
        {
            string identifierPrefix = identifierType + ":";

            if (originalIdentifier.StartsWith(identifierPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return new SfdoIdentifier
                {
                    Type = identifierType,
                    Value = originalIdentifier.Substring(identifierPrefix.Length),
                };
            }
        }

        return new SfdoIdentifier
        {
            Value = originalIdentifier,
        };
    }
}
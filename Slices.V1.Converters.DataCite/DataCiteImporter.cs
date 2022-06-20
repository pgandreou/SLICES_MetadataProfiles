using System.Globalization;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite;

public class DataCiteImporter : ISlicesImporter<DataCiteResource>
{
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
}
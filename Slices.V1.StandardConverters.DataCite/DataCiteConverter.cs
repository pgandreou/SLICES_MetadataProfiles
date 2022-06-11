using Slices.V1.Standard;
using Slices.V1.StandardConverters.Common;
using Slices.V1.StandardConverters.DataCite.Model;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Slices.V1.StandardConverters.DataCite;

public class DataCiteConverter : ISlicesStandardConverter<DataCiteResource>
{
    private readonly IStandardXmlSerializer<DataCiteResource> _serializer;

    public DataCiteConverter(IStandardXmlSerializer<DataCiteResource> serializer)
    {
        _serializer = serializer;
    }

    public string ExternalStandard => throw new NotImplementedException();

    public SfdoResource FromExtrenal(DataCiteResource externalModel)
    {
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

        if (PickBestByLang(externalModel.language, externalModel.titles, t => t.lang, out DataCiteResourceTitle? title))
        {
            sfdo.Name = title.Value;
        }

        if (PickBestByLang(externalModel.language, externalModel.descriptions, t => t.lang, out DataCiteResourceDescription? description))
        {
            sfdo.Description = description.Text;
        }

        sfdo.Subjects = PickBestByLang(externalModel.language, externalModel.subjects, s => s.lang)
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

        if (PickBestByLang(externalModel.language, externalModel.rightsList, t => t.lang, out DataCiteResourceRights? rights))
        {
            sfdo.Rights = rights.Value;
            sfdo.RightsURI = new Uri(rights.rightsURI);
        }

        return sfdo;

        //return null!;
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

    private static bool PickBestByLang<TItem>(
        string? resourceLang,
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector,
        [NotNullWhen(true)] out TItem? selectedItem
    )
        where TItem : class
    {
        selectedItem = PickBestByLang(resourceLang, items, langSelector).FirstOrDefault();

        return selectedItem != null;
    }

    private static IEnumerable<TItem> PickBestByLang<TItem>(
        string? resourceLang,
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        where TItem : class
    {
        if (!items.Any()) return Array.Empty<TItem>();

        (string? Lang, TItem Item)[] langedItems = DeriveItemLangs(items, langSelector).ToArray();

        // Prefer English
        IEnumerable<(string? Lang, TItem Item)> selectedTuples = langedItems.Where(t => t.Lang == "en");

        // Otherwise prefer the resource lang
        if (!selectedTuples.Any())
        {
            resourceLang = GeneralizeLang(resourceLang);

            if (!string.IsNullOrWhiteSpace(resourceLang))
            {
                selectedTuples = langedItems.Where(t => t.Lang == resourceLang);
            }
        }

        // Otherwise try to find something that has a lang set
        if (!selectedTuples.Any())
        {
            selectedTuples = langedItems.Where(t => !string.IsNullOrWhiteSpace(t.Lang));
        }

        // Otherwise just pick all
        if (!selectedTuples.Any())
        {
            selectedTuples = langedItems;
        }

        return selectedTuples.Select(t => t.Item);
    }

    private static IEnumerable<(string? Lang, TItem Item)> DeriveItemLangs<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        => items.Select(item => (GeneralizeLang(langSelector(item)), item));

    private static string? GeneralizeLang(string? lang)
    {
        if (lang == null) return null;

        int separatorIndex = lang.IndexOf("-");

        if (separatorIndex > 0)
        {
            return lang[..separatorIndex];
        }

        return lang;
    }

    public SfdoResource FromSerializedExtrenal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExtrenal(_serializer.FromXml(serializedReader));
    }

    public DataCiteResource ToExtrenal(SfdoResource sfdo)
    {
        DataCiteResource dataCiteResource = new();

        dataCiteResource.identifier = new()
        {
            identifierType = sfdo.Identifier.Type,
            Value = sfdo.Identifier.Value,
        };

        dataCiteResource.creators = sfdo.Creators
            .Select(c =>
            {
                DataCiteCreator dataCiteCreator = new()
                {
                    creatorName = new() { Value = c.Name },
                };

                if (c.Identifier.HasValue)
                {
                    dataCiteCreator.nameIdentifier = new[] { new DataCiteNameIdentifier() {
                        nameIdentifierScheme = c.Identifier.Value.Type,
                        Value = c.Identifier.Value.Value,
                    }};
                }

                return dataCiteCreator;
            })
            .ToArray();

        dataCiteResource.titles = new[] { new DataCiteResourceTitle() { Value = sfdo.Name } };

        dataCiteResource.subjects = sfdo.Subjects
            .Select(s => new DataCiteResourceSubject { Value = s })
            .ToArray();

        dataCiteResource.contributors = sfdo.Contributors
            .Select(c =>
            {
                DataCiteResourceContributor dataCiteContributor = new DataCiteResourceContributor
                {
                    contributorName = new() { Value = c.Name },
                };

                if (c.Identifier.HasValue)
                {
                    dataCiteContributor.nameIdentifier = new[] { new DataCiteNameIdentifier() {
                        nameIdentifierScheme = c.Identifier.Value.Type,
                        Value = c.Identifier.Value.Value,
                    }};
                }

                return dataCiteContributor;
            })
            .ToArray();

        // TODO: DataCite doesn't use iso-3
        dataCiteResource.language = sfdo.PrimaryLanguage.FirstOrDefault().Code;

        dataCiteResource.alternateIdentifiers = sfdo.AlternateIdentifiers
            .Select(id => new DataCiteResourceAlternateIdentifier { Type = id.Type, Value = id.Value })
            .ToArray();

        dataCiteResource.relatedIdentifiers = sfdo.RelatedObjects
            .Select(ro =>
            {
                DataCiteResourceRelatedIdentifier dcRelatedId = new();

                {
                    if (Enum.TryParse(ro.Identifier.Type, ignoreCase: true, out DataCiteRelatedIdentifierType dataCiteType))
                    {
                        dcRelatedId.relatedIdentifierType = dataCiteType;
                        dcRelatedId.Value = ro.Identifier.Value;
                    }
                    else
                    {
                        dcRelatedId.relatedIdentifierType = DataCiteRelatedIdentifierType.Handle;
                        dcRelatedId.Value = ro.Identifier.ToString();
                    }
                }

                if (ro.ResourceType != null)
                {
                    dcRelatedId.resourceTypeGeneralSpecified = true;

                    if (Enum.TryParse(ro.ResourceType, ignoreCase: true, out DataCiteResourceTypeGeneral dataCiteType))
                    {
                        dcRelatedId.resourceTypeGeneral = dataCiteType;
                    }
                    else
                    {
                        dcRelatedId.resourceTypeGeneral = DataCiteResourceTypeGeneral.Other;
                    }
                }

                dcRelatedId.relationType = Enum.Parse<DataCiteRelationType>(ro.RelationshipType!); // TODO: this is very prone to errors

                return dcRelatedId;
            })
            .ToArray();

        dataCiteResource.version = sfdo.Version;

        if (sfdo.Rights != null || sfdo.RightsURI != null)
        {
            dataCiteResource.rightsList = new[] { new DataCiteResourceRights
            {
                rightsURI = sfdo.RightsURI?.ToString(),
                Value = sfdo.Rights,
            }};
        }

        if (sfdo.Description != null)
        {
            dataCiteResource.descriptions = new[] { new DataCiteResourceDescription { Text = sfdo.Description } };
        }

        return dataCiteResource;
    }

    public void ToSerializedExtrenal(SfdoResource sfdo, string? format, TextWriter serializedWriter)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        _serializer.ToXml(ToExtrenal(sfdo), serializedWriter);
    }
}
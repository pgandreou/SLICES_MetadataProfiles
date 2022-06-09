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

        // TODO: langs?
        sfdo.Subjects = externalModel.subjects.Select(s => s.Value).ToList();

        // TODO: keywords

        //slicesObject.DateTimeCreated = DateTime.Now;

        sfdo.Version = externalModel.version;

        // TODO: MetadataProfile

        sfdo.Contributors = externalModel.contributors
            .Select(c => new SfdoContributor
            {
                Name = c.contributorName.Value,
                Identifier = PickIdentifierForCreatorLike(c.nameIdentifier),
            })
            .ToList();

        // TODO: AccessType/AccessMode

        sfdo.RelatedObjects = externalModel.relatedIdentifiers
            .Select(ri => new SfdoRelationLink
            {
                Identifier = new SfdoIdentifier { Type = ri.relatedIdentifierType.ToString(), Value = ri.Value },
                RelationshipType = ri.relationType.ToString(),
                ResourceType = ri.resourceTypeGeneralSpecified ? ri.relatedIdentifierType.ToString() : null,
            })
            .ToList();

        try
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(externalModel.language);

            sfdo.PrimaryLanguage.Add(new LanguageIso639_3 { Code = culture.ThreeLetterISOLanguageName });
        }
        catch (CultureNotFoundException)
        {
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
        selectedItem = default;
        if (!items.Any()) return false;

        (string? Lang, TItem Item)[] langedItems = DeriveItemLangs(items, langSelector).ToArray();

        // Prefer English
        (string? Lang, TItem Item) selectedTuple = langedItems.FirstOrDefault(t => t.Lang == "en");

        // Otherwise prefer the resource lang
        if (selectedTuple.Item == null)
        {
            resourceLang = GeneralizeLang(resourceLang);

            // Otherwise prefer the resource lang
            if (!string.IsNullOrWhiteSpace(resourceLang))
            {
                selectedTuple = langedItems.FirstOrDefault(t => t.Lang == resourceLang);
            }
        }

        // Otherwise try to find something that has a lang set
        if (selectedTuple.Item == null)
        {
            selectedTuple = langedItems.FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.Lang));
        }

        // Otherwise just pick the first
        if (selectedTuple.Item == null)
        {
            selectedTuple = langedItems.First();
        }

        selectedItem = selectedTuple.Item;

        return true;
    }

    private static IEnumerable<(string? Lang, TItem Item)> DeriveItemLangs<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        => items.Select(item => (GeneralizeLang(langSelector(item)), item));

    private static string? GeneralizeLang(string? lang)
    {
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

    public DataCiteResource ToExtrenal(SfdoResource digitalObject)
    {
        throw new NotImplementedException();
    }

    public void ToSerializedExtrenal(SfdoResource digitalObject, string? format, TextWriter serializedWriter)
    {
        throw new NotImplementedException();
    }
}
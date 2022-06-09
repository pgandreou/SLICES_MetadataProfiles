using Slices.V1.Standard;
using Slices.V1.StandardConverters.Common;
using Slices.V1.StandardConverters.DataCite.Model;

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

        // TODO
        if (externalModel.titles.Any())
        {
            sfdo.Name = externalModel.titles.First().Value;
        }

        if (externalModel.descriptions.Any())
        {
            sfdo.Description = externalModel.descriptions.First().Text;
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
                Identifier = ri.Value,
                RelationshipType = ri.relationType.ToString(),
                ResourceType = ri.resourceTypeGeneralSpecified ? ri.relatedIdentifierType.ToString() : null,
            })
            .ToList();

        sfdo.PrimaryLanguage = new(new[] { new LanguageIso639_3 { Code = externalModel.language } }); // TODO: not iso639-3

        if (externalModel.rightsList.Any())
        {
            DataCiteResourceRights rights = externalModel.rightsList.First();

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
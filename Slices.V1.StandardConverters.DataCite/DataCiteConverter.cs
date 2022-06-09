using Slices.V1.Standard;
using Slices.V1.StandardConverters.Common;
using Slices.V1.StandardConverters.DataCite.Model;
using System.Diagnostics;

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
        //DigitalObject slicesObject = new();

        //slicesObject.Identifier = $"{externalModel.identifier.identifierType}:{externalModel.identifier.Value}";
        //slicesObject.InternalIdentifier = Guid.NewGuid().ToString();

        //slicesObject.AlternateIdentifier = string.Join(
        //    ";",
        //    externalModel.alternateIdentifiers.Select(id => $"{id.Type}:{id.Value}")
        //);

        //slicesObject.Creator = new List<string>(externalModel.creators.Length);
        //slicesObject.CreatorIdentifier = new List<string>(externalModel.creators.Length);

        //foreach (DataCiteCreator creator in externalModel.creators)
        //{
        //    slicesObject.Creator.Add(creator.creatorName.Value);
        //    //slicesObject.CreatorIdentifier.Add() // TODO
        //}

        //// TODO
        //if (externalModel.titles.Any())
        //{
        //    slicesObject.Name = externalModel.titles.First().Value;
        //}

        //if (externalModel.descriptions.Any())
        //{
        //    slicesObject.Description = externalModel.descriptions.First().Text;
        //}

        //// TODO: langs?
        //slicesObject.Subject = externalModel.subjects.Select(s => s.Value).ToList();

        //// TODO: keywords

        //slicesObject.DateTimeCreated = DateTime.Now;

        //slicesObject.Version = externalModel.version;

        //// TODO: MetadataProfile

        //slicesObject.Contributor = new List<string>(externalModel.contributors.Length);
        //slicesObject.ContributorIdentifier = new List<string>(externalModel.contributors.Length);

        //foreach (DataCiteResourceContributor contributor in externalModel.contributors)
        //{
        //    slicesObject.Contributor.Add(contributor.contributorName.Value);
        //    //slicesObject.ContributorIdentifier.Add(); TODO
        //}

        //// TODO: AccessType/AccessMode

        //slicesObject.RelatedObjects = new();

        //foreach (DataCiteResourceRelatedIdentifier relatedIdentifier in externalModel.relatedIdentifiers)
        //{
        //    slicesObject.RelatedObjects.Add(new SfdoRelationLink 
        //    {
        //        Identifier = relatedIdentifier.Value,
        //        RelationshipType = relatedIdentifier.relationType.ToString(),
        //        ResourceType = relatedIdentifier.resourceTypeGeneralSpecified ? relatedIdentifier.relatedIdentifierType.ToString() : null,
        //    });
        //}

        //slicesObject.PrimaryLanguage = new(new[] { new LanguageIso639_3 { Code = externalModel.language } }); // TODO: not iso639-3

        //if (externalModel.rightsList.Any())
        //{
        //    DataCiteResourceRights rights = externalModel.rightsList.First();

        //    slicesObject.Rights = rights.Value;
        //    slicesObject.RightsURI = new Uri(rights.rightsURI);
        //}

        //return slicesObject;

        return null!;
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

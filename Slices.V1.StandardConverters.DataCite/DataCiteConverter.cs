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

    public DigitalObject FromExtrenal(DataCiteResource externalModel)
    {
        DigitalObject slicesObject = new();

        slicesObject.Identifier = $"{externalModel.identifier.identifierType}:{externalModel.identifier.Value}";
        slicesObject.InternalIdentifier = Guid.NewGuid().ToString();

        slicesObject.AlternateIdentifier = string.Join(
            ";",
            externalModel.alternateIdentifiers.Select(id => $"{id.Type}:{id.Value}")
        );

        slicesObject.Creator = new List<string>(externalModel.creators.Length);
        slicesObject.CreatorIdentifier = new List<string>(externalModel.creators.Length);

        foreach (DataCiteCreator creator in externalModel.creators)
        {
            slicesObject.Creator.Add(creator.creatorName.Value);
            //slicesObject.CreatorIdentifier.Add() // TODO
        }

        // TODO
        if (externalModel.titles.Any())
        {
            slicesObject.Name = externalModel.titles.First().Value;
        }

        if (externalModel.descriptions.Any())
        {
            slicesObject.Description = externalModel.descriptions.First().Text;
        }
        
        // TODO: langs?
        slicesObject.Subject = externalModel.subjects.Select(s => s.Value).ToList();

        // TODO: keywords

        slicesObject.DateTimeCreated = DateTime.Now;

        slicesObject.Version = externalModel.version;

        // TODO

        return slicesObject;
    }

    public DigitalObject FromSerializedExtrenal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExtrenal(_serializer.FromXml(serializedReader));
    }

    public DataCiteResource ToExtrenal(DigitalObject digitalObject)
    {
        throw new NotImplementedException();
    }

    public void ToSerializedExtrenal(DigitalObject digitalObject, string? format, TextWriter serializedWriter)
    {
        throw new NotImplementedException();
    }
}

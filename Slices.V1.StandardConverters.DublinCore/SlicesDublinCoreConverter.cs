using Slices.V1.Format;
using Slices.V1.StandardConverters.Common;

namespace Slices.V1.StandardConverters.DublinCore;

public class SlicesDublinCoreConverter : ISlicesStandardConverter<DublinCoreObject>
{
    private readonly DublinCoreSerializer _serializer;

    // TODO: Interface?
    public SlicesDublinCoreConverter(DublinCoreSerializer serializer)
    {
        _serializer = serializer;
    }

    public string ExternalStandard => DublinCoreConstants.StandardId;

    public DigitalObject FromExtrenal(DublinCoreObject externalModel)
    {
        DigitalObject slicesObject = new();

        slicesObject.Description = externalModel.description;
        slicesObject.Creator = new(externalModel.creator);
        slicesObject.Name = externalModel.title;
        slicesObject.Rights = externalModel.rights;

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

    public DublinCoreObject ToExtrenal(DigitalObject digitalObject)
    {
        throw new NotImplementedException();
    }

    public void ToSerializedExtrenal(DigitalObject digitalObject, string? format, TextWriter serializedWriter)
    {
        throw new NotImplementedException();
    }
}

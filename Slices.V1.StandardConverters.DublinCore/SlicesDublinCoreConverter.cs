﻿using Slices.V1.Format;

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

    public DigitalObject FromSerializedExtrenal(string serializedValue, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExtrenal(_serializer.FromXml(serializedValue));
    }

    public DublinCoreObject ToExtrenal(DigitalObject digitalObject)
    {
        throw new NotImplementedException();
    }

    public string ToSerializedExtrenal(DigitalObject digitalObject, string? format)
    {
        throw new NotImplementedException();
    }
}

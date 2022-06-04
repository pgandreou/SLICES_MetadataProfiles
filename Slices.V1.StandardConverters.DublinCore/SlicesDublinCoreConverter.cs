using Slices.V1.Format;

namespace Slices.V1.StandardConverters.DublinCore;

internal class SlicesDublinCoreConverter : ISlicesExtrenalConverter<DublinCoreObject>
{
    public string ExternalStandard => DublinCoreConstants.StandardId;

    public DigitalObject FromExtrenal(DublinCoreObject externalModel)
    {
        throw new NotImplementedException();
    }

    public DigitalObject FromSerializedExtrenal(string serializedValue, string? format)
    {
        throw new NotImplementedException();
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

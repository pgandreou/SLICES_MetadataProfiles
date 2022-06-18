using Slices.V1.Converters.Common;

namespace Slices.V1.Converters.DublinCore;

public class DublinCoreConverter : BaseXmlStandardConverter<DublinCoreResource>
{
    public override string ExternalStandard => DublinCoreConstants.StandardId;

    public DublinCoreConverter(
        ISlicesImporter<DublinCoreResource> importer,
        ISlicesExporter<DublinCoreResource> exporter,
        IStandardXmlSerializer<DublinCoreResource> serializer
    ) : base(importer, exporter, serializer)
    {
    }
}
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite.Model;

namespace Slices.V1.Converters.DataCite;

public class DataCiteConverter : BaseXmlStandardConverter<DataCiteResource>
{
    public override string ExternalStandard => DataCiteConstants.StandardId;

    public DataCiteConverter(
        ISlicesImporter<DataCiteResource> importer,
        ISlicesExporter<DataCiteResource> exporter,
        IStandardXmlSerializer<DataCiteResource> serializer
    ) : base(importer, exporter, serializer)
    {
    }
}
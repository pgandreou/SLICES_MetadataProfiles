using Slices.V1.Converters.Common;

namespace Slices.V1.Converters.EoscProviderProfile;

public class EoscProviderConverter : BaseJsonStandardConverter<EoscProviderRecord>
{
    public override string ExternalStandard => EoscProviderConstants.StandardId;

    public EoscProviderConverter(
        ISlicesImporter<EoscProviderRecord> importer,
        ISlicesExporter<EoscProviderRecord> exporter,
        IStandardJsonSerializer<EoscProviderRecord> serializer
    ) : base(importer, exporter, serializer)
    {
    }
}
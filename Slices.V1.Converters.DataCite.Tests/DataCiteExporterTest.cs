using Slices.TestsSupport;
using Slices.V1.Converters.DataCite.Model;

namespace Slices.V1.Converters.DataCite.Tests;

public class DataCiteExporterTest
{
    [Fact]
    public void DataCiteSample()
    {
        DataCiteResource dcResource = new DataCiteExporter().ToExternal(SfdoTestSamples.DataCiteSample());
        
        Assert.NotNull(dcResource);
    }
}
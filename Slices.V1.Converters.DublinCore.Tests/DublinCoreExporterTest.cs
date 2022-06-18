using Slices.TestsSupport;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreExporterTest
{
    [Fact]
    public void DataCiteSample()
    {
        DublinCoreResource dcResource = new DublinCoreExporter().ToExternal(SfdoTestSamples.DataCiteSample());
        
        Assert.NotNull(dcResource);
    }
}
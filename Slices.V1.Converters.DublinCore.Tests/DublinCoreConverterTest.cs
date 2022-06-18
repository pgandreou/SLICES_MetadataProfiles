using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreConverterTest
{
    [Fact]
    public void FromToExternalSerialized1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");
        DublinCoreConverter converter = new(new DublinCoreImporter(), new DublinCoreExporter(), new DublinCoreSerializer());

        SfdoResource sfdo = converter.FromSerializedExternal(textReader, null);

        StringWriter writer = new();
        converter.ToSerializedExternal(sfdo, null, writer);

        string serialized = writer.ToString();

        Assert.NotNull(serialized);
    }
    
    [Fact]
    public void FromToExternalSerialized2()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\d-na-4-1.xml");
        DublinCoreConverter converter = new(new DublinCoreImporter(), new DublinCoreExporter(), new DublinCoreSerializer());

        SfdoResource sfdo = converter.FromSerializedExternal(textReader, null);

        StringWriter writer = new();
        converter.ToSerializedExternal(sfdo, null, writer);

        string serialized = writer.ToString();

        Assert.NotNull(serialized);
    }
}

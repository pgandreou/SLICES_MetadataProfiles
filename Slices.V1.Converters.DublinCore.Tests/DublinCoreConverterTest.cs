using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreConverterTest
{
    [Fact]
    public void FromExternal1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        DublinCoreConverter converter = new(serializer);

        DublinCoreResource dublinCoreObject = serializer.FromXml(textReader);
        SfdoResource slicesObject = converter.FromExternal(dublinCoreObject);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromExternalSerialized1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");
        DublinCoreConverter converter = new(new DublinCoreSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(textReader, null);

        Assert.NotNull(slicesObject);
    }
    
    [Fact]
    public void FromExternalSerialized2()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\bip4covid19.xml");
        DublinCoreConverter converter = new(new DublinCoreSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(textReader, null);

        Assert.NotNull(slicesObject);
    }
    
    [Fact]
    public void FromExternalSerialized3()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\d-na-4-1.xml");
        DublinCoreConverter converter = new(new DublinCoreSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(textReader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromToExternalSerialized1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");
        DublinCoreConverter converter = new(new DublinCoreSerializer());

        SfdoResource sfdo = converter.FromSerializedExternal(textReader, null);

        StringWriter writer = new();
        converter.ToSerializedExternal(sfdo, null, writer);

        string serialized = writer.ToString();

        Assert.NotNull(serialized);
    }
}

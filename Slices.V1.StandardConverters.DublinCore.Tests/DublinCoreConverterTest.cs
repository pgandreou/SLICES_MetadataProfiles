using Slices.TestsSupport;
using Slices.V1.Standard;

namespace Slices.V1.StandardConverters.DublinCore.Tests;

public class DublinCoreConverterTest
{
    [Fact]
    public void FromExtrenal1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        SlicesDublinCoreConverter converter = new(serializer);

        DublinCoreResource dublinCoreObject = serializer.FromXml(textReader);
        SfdoResource slicesObject = converter.FromExtrenal(dublinCoreObject);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromExtrenalSerialized1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");
        SlicesDublinCoreConverter converter = new(new DublinCoreSerializer());

        SfdoResource slicesObject = converter.FromSerializedExtrenal(textReader, null);

        Assert.NotNull(slicesObject);
    }
}

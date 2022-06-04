using Slices.TestsSupport;

namespace Slices.V1.StandardConverters.DublinCore.Tests;

public class DublinCoreSerializerTest
{
    [Fact]
    public void Test2()
    {
        DublinCoreSerializer serializer = new();
        DublinCoreObject record = new();

        record.creator = new[] { "C_A", "C_B" };
        record.date = DateTime.Now;
        record.description = "abc";
        record.identifier = new[] { "I_A", "I_B" };
        record.rights = "abc2";
        record.title = "abc3";
        record.type = new[] { "T_A", "T_B" };

        var s = serializer.ToXml(record);

        Assert.NotEmpty(s);
    }

    [Fact]
    public void Test3()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        DublinCoreObject result = serializer.FromXml(textReader);

        Assert.NotNull(result);

        Assert.Equal(9, result.creator.Length);
        Assert.Equal("MVolobueva", result.creator[2]);
    }
}
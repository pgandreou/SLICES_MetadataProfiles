using Slices.TestsSupport;

namespace Slices.V1.StandardConverters.DublinCore.Tests;

public class DublinCoreSerializerTest
{
    [Fact]
    public void Test2()
    {
        DublinCoreSerializer serializer = new();
        StringWriter writer = new();

        DublinCoreResource record = new()
        {
            Titles = new[] { new DublinCoreElement("Titles 1") },
            Creators = new[] { new DublinCoreElement("Creators 1") },
            Subjects = new[] { new DublinCoreElement("Subjects 1") },
            Descriptions = new[] { new DublinCoreElement("Descriptions 1") },
            Publishers = new[] { new DublinCoreElement("Publishers 1") },
            Contributors = new[] { new DublinCoreElement("Contributors 1") },
            Dates = new[] { new DublinCoreElement("Dates 1") },
            Types = new[] { new DublinCoreElement("Types 1") },
            Formats = new[] { new DublinCoreElement("Formats 1") },
            Identifiers = new[] { new DublinCoreElement("Identifiers 1") },
            Sources = new[] { new DublinCoreElement("Sources 1") },
            Languages = new[] { new DublinCoreElement("Languages 1") },
            Relations = new[] { new DublinCoreElement("Relations 1") },
            Coverages = new[] { new DublinCoreElement("Coverages 1") },
            Rights = new[] { new DublinCoreElement("Rights 1") },
        };

        serializer.ToXml(record, writer);
        string s = writer.ToString();

        Assert.NotEmpty(s);
    }

    [Fact]
    public void Test3()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\immunarch-0-6-9.xml");

        DublinCoreSerializer serializer = new();
        DublinCoreResource result = serializer.FromXml(textReader);

        Assert.NotNull(result);

        //Assert.Equal(9, result.creator.Length);
        //Assert.Equal("MVolobueva", result.creator[2]);
    }
}
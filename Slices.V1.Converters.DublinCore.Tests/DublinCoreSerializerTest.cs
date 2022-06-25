using Slices.Common;
using Slices.TestsSupport;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreSerializerTest
{
    [Fact]
    public async Task Test2()
    {
        DublinCoreSerializer serializer = new();
        MemoryStream stream = new();

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

        await serializer.ToXmlAsync(record, stream);
        string s = stream.ReadAsStringFromStart();

        Assert.NotEmpty(s);
    }

    [Fact]
    public async Task Test3()
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(), "ReferenceFiles\\immunarch-0-6-9.xml"
        );

        DublinCoreSerializer serializer = new();
        DublinCoreResource result = await serializer.FromXmlAsync(stream);

        Assert.NotNull(result);

        //Assert.Equal(9, result.creator.Length);
        //Assert.Equal("MVolobueva", result.creator[2]);
    }
}
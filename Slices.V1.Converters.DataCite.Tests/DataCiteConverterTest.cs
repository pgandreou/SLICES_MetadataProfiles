using Slices.TestsSupport;
using Slices.V1.Model;
using Slices.V1.Converters.DataCite.Model;

namespace Slices.V1.Converters.DataCite.Tests;

public class DataCiteConverterTest
{
    [Fact]
    public void FromDataCiteExampleTest()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromDataCite4_4ExampleTest()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4-4.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromDna41Test()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\d-na-4-1.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromBip4Covid19Test()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\bip4covid19.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExternal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void ToDataCiteExampleTest()
    {
        SfdoResource sfdo = new();

        sfdo.Identifier = new() { Type = SfdoIdentifierTypes.Doi, Value = "10.5072/example-full" };
        sfdo.AlternateIdentifiers.Add(new() { Type = SfdoIdentifierTypes.Url, Value = "https://schema.datacite.org/meta/kernel-4.4/example/datacite-example-full-v4.4.xml" });

        sfdo.Creators.Add(new()
        {
            Identifier = new() { Type = SfdoIdentifierTypes.Orcid, Value = "0000-0001-5000-0007" },
            Name = "Miller, Elizabeth",
        });

        sfdo.Name = "Full DataCite XML Example";
        sfdo.Description = "XML example of all DataCite Metadata Schema v4.4 properties.";

        sfdo.Subjects.Add("computer science");

        sfdo.Version = "4.4";

        sfdo.Contributors.Add(new()
        {
            Identifier = new() { Type = SfdoIdentifierTypes.Orcid, Value = "0000-0002-7285-027X" },
            Name = "Starr, Joan",
        });

        sfdo.RelatedObjects.Add(new()
        {
            Identifier = new() { Type = SfdoIdentifierTypes.Url, Value = "https://data.datacite.org/application/citeproc+json/10.5072/example-full" },
            RelationshipType = "HasMetadata",
        });
        sfdo.RelatedObjects.Add(new()
        {
            Identifier = new() { Type = SfdoIdentifierTypes.Arxiv, Value = "0706.0001" },
            RelationshipType = "IsReviewedBy",
            ResourceType = "Text",
        });

        sfdo.PrimaryLanguage.Add(new() { Code = "eng" });

        sfdo.RightsURI = new Uri("https://creativecommons.org/publicdomain/zero/1.0/");

        DataCiteConverter converter = new(new DataCiteSerializer());
        StringWriter writer = new();

        converter.ToSerializedExternal(sfdo, null, writer);
        string serialized = writer.ToString();

        Assert.NotNull(serialized);
    }
}
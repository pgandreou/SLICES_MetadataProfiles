using Slices.TestsSupport;
using Slices.V1.Standard;

namespace Slices.V1.StandardConverters.DataCite.Tests;

public class DataCiteConverterTest
{
    [Fact]
    public void FromDataCiteExampleTest()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExtrenal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromDataCite4_4ExampleTest()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4-4.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExtrenal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromDna41Test()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\d-na-4-1.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExtrenal(reader, null);

        Assert.NotNull(slicesObject);
    }

    [Fact]
    public void FromBip4Covid19Test()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\bip4covid19.xml");
        DataCiteConverter converter = new(new DataCiteSerializer());

        SfdoResource slicesObject = converter.FromSerializedExtrenal(reader, null);

        Assert.NotNull(slicesObject);
    }
}
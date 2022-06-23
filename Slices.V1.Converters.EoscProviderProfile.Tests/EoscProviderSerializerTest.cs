using Slices.TestsSupport;

namespace Slices.V1.Converters.EoscProviderProfile.Tests;

public class EoscProviderSerializerTest
{
    [Fact]
    public void Test1()
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\slices.json");

        EoscProviderRecord record = new EoscProviderSerializer().FromJson(textReader);
    }
}
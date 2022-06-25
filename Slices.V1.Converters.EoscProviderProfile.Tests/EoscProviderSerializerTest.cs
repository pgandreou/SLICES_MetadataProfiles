using Slices.TestsSupport;

namespace Slices.V1.Converters.EoscProviderProfile.Tests;

public class EoscProviderSerializerTest
{
    [Fact]
    public async Task Test1()
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(), "ReferenceFiles\\slices.json"
        );

        EoscProviderRecord record = await new EoscProviderSerializer().FromJsonAsync(stream);
        
        Assert.NotNull(record);
    }
}
using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.EoscProviderProfile.Tests;

public class EoscProviderImporterTest
{
    [Fact]
    public async Task Slices()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\slices.json");

        Assert.NotNull(sfdo);
    }

    private async Task<SfdoResource> ImportFromCopiedExternal(string pathRelativeToAssemblyRoot)
    {
        EoscProviderRecord dublinCoreResource = await GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);

        return new EoscProviderImporter().FromExternal(dublinCoreResource);
    }

    private async Task<EoscProviderRecord> GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(), pathRelativeToAssemblyRoot
        );

        return await new EoscProviderSerializer().FromJsonAsync(stream);
    }
}
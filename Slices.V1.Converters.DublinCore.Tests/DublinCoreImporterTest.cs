using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreImporterTest
{
    [Fact]
    public async Task Immunarch_0_6_9()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\immunarch-0-6-9.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public async Task Bip_4_Covid_19()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\bip4covid19.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public async Task D_na_4_1()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\d-na-4-1.xml");
        
        Assert.NotNull(sfdo);
    }

    private async Task<SfdoResource> ImportFromCopiedExternal(string pathRelativeToAssemblyRoot)
    {
        DublinCoreResource dublinCoreResource = await GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);
        
        return new DublinCoreImporter().FromExternal(dublinCoreResource);
    }

    private async Task<DublinCoreResource> GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(), pathRelativeToAssemblyRoot
        );

        return await new DublinCoreSerializer().FromXmlAsync(stream);
    }
}
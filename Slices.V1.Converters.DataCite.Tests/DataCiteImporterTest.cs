using Slices.TestsSupport;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite.Tests;

public class DataCiteImporterTest
{
    [Fact]
    public async Task DataCiteExample()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\datacite-example-full-v4.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public async Task DataCiteExample_4_4()
    {
        SfdoResource sfdo = await ImportFromCopiedExternal("ReferenceFiles\\datacite-example-full-v4-4.xml");
        
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
        DataCiteResource dublinCoreResource = await GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);
        
        return new DataCiteImporter().FromExternal(dublinCoreResource);
    }

    private async Task<DataCiteResource> GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(), pathRelativeToAssemblyRoot
        );

        return await new DataCiteSerializer().FromXmlAsync(stream);
    }
}
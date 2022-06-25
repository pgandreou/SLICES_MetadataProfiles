using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore.Tests;

public class DublinCoreImporterTest
{
    [Fact]
    public void Immunarch_0_6_9()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\immunarch-0-6-9.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public void Bip_4_Covid_19()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\bip4covid19.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public void D_na_4_1()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\d-na-4-1.xml");
        
        Assert.NotNull(sfdo);
    }

    private SfdoResource ImportFromCopiedExternal(string pathRelativeToAssemblyRoot)
    {
        DublinCoreResource dublinCoreResource = GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);
        
        return new DublinCoreImporter().FromExternal(dublinCoreResource);
    }

    private DublinCoreResource GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), pathRelativeToAssemblyRoot);

        return new DublinCoreSerializer().FromXmlAsync(textReader);
    }
}
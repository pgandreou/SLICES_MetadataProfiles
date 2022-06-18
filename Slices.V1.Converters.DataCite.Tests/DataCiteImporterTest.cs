using Slices.TestsSupport;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite.Tests;

public class DataCiteImporterTest
{
    [Fact]
    public void DataCiteExample()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\datacite-example-full-v4.xml");
        
        Assert.NotNull(sfdo);
    }
    
    [Fact]
    public void DataCiteExample_4_4()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\datacite-example-full-v4-4.xml");
        
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
        DataCiteResource dublinCoreResource = GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);
        
        return new DataCiteImporter().FromExternal(dublinCoreResource);
    }

    private DataCiteResource GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), pathRelativeToAssemblyRoot);

        return new DataCiteSerializer().FromXml(textReader);
    }
}
using Slices.TestsSupport;
using Slices.V1.Model;

namespace Slices.V1.Converters.EoscProviderProfile.Tests;

public class EoscProviderImporterTest
{
    [Fact]
    public void Slices()
    {
        SfdoResource sfdo = ImportFromCopiedExternal("ReferenceFiles\\slices.json");
        
        Assert.NotNull(sfdo);
    }

    private SfdoResource ImportFromCopiedExternal(string pathRelativeToAssemblyRoot)
    {
        EoscProviderRecord dublinCoreResource = GetCopiedDublinCoreResource(pathRelativeToAssemblyRoot);
        
        return new EoscProviderImporter().FromExternal(dublinCoreResource);
    }

    private EoscProviderRecord GetCopiedDublinCoreResource(string pathRelativeToAssemblyRoot)
    {
        using TextReader textReader = SlicesTestHelpers.GetCopiedFileReader(GetType(), pathRelativeToAssemblyRoot);

        return new EoscProviderSerializer().FromJson(textReader);
    }
}
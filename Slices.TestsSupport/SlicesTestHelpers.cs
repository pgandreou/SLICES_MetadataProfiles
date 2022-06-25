using System.Diagnostics.Contracts;

namespace Slices.TestsSupport;

public static class SlicesTestHelpers
{
    [Pure]
    public static string GetCopiedFilePath(Type testType, string pathRelativeToAssemblyRoot)
    {
        string assemblyPath = testType.Assembly.Location;
        string assemblyDirPath = Path.GetDirectoryName(assemblyPath)!;

        return Path.Combine(assemblyDirPath, pathRelativeToAssemblyRoot);
    }

    [Pure]
    public static FileStream GetCopiedFileReadStream(Type testType, string pathRelativeToAssemblyRoot)
    {
        string path = GetCopiedFilePath(testType, pathRelativeToAssemblyRoot);
        
        return new FileStream(path, new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
        });
    }
    
    [Pure]
    public static StreamReader GetCopiedFileReader(Type testType, string pathRelativeToAssemblyRoot)
    {
        return new StreamReader(GetCopiedFileReadStream(testType, pathRelativeToAssemblyRoot));
    }
}

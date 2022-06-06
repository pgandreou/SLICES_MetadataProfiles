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
    public static StreamReader GetCopiedFileReader(Type testType, string pathRelativeToAssemblyRoot)
    {
        string path = GetCopiedFilePath(testType, pathRelativeToAssemblyRoot);
        FileStream fs = new(path, new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
        });

        return new StreamReader(fs);
    }
}

namespace Slices.Common;

public static class StreamExtensions
{
    // https://stackoverflow.com/questions/2462391/reset-or-clear-net-memorystream
    public static void Clear(this MemoryStream stream)
    {
        stream.Position = 0;
        stream.SetLength(0);
        stream.Capacity = 0;
    }

    public static string ReadAsString(this MemoryStream stream) => new StreamReader(stream).ReadToEnd();
    
    public static string ReadAsStringFromStart(this MemoryStream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        return stream.ReadAsString();
    }
}
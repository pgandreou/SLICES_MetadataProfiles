namespace Slices.V1.Converters.Common;

public class UnsupportedExternalFormatException : Exception
{
    public string Format { get; set; } = null!;
    public string ExternalStandard { get; set; } = null!;

    public UnsupportedExternalFormatException() : base()
    {
    }

    public UnsupportedExternalFormatException(string? message) : base(message)
    {
    }

    public UnsupportedExternalFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}


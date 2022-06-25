namespace Slices.V1.Converters.Common.Exceptions;

public class UnsupportedExternalFormatException : Exception
{
    public string? Format { get; init; }
    public string? ExternalStandard { get; init; }

    public UnsupportedExternalFormatException()
    {
    }

    public UnsupportedExternalFormatException(string? message) : base(message)
    {
    }

    public UnsupportedExternalFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}


namespace Slices.V1.Converters.Common.Exceptions;

public class StandardSerializationException : Exception
{
    public StandardSerializationException()
    {
    }

    public StandardSerializationException(string? message) : base(message)
    {
    }

    public StandardSerializationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
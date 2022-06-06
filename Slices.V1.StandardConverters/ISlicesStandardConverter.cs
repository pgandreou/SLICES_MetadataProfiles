using Slices.V1.Format;
using System.Diagnostics.Contracts;

namespace Slices.V1.StandardConverters;

public interface ISlicesStandardConverter
{
    /// <summary>
    /// A constant string representing which external standard this converter is handling
    /// </summary>
    string ExternalStandard { get; }

    /// <summary>
    /// Converts a single record from the external standard to SLICES.
    /// </summary>
    /// <remarks>
    /// Implementations should only mutate <paramref name="serializedReader"/> and be safe to be called from multiple threads
    /// </remarks>
    /// <param name="serializedReader">
    /// A reader that will provide the serialized representation of the record in the external standard
    /// </param>
    /// <param name="format">
    /// The format of <paramref name="serializedValue"/>.
    /// 
    /// Null means that the format is not known by the caller and the implementation
    /// should attempt whatever is the default format used by the external standard.
    /// </param>
    /// <exception cref="UnsupportedExternalFormatException">
    /// Thrown if <paramref name="format"/> is not supported by the implementation.
    /// Not thrown if the format is null.
    /// </exception>
    /// <returns>The SLICES version of the record</returns>
    DigitalObject FromSerializedExtrenal(TextReader serializedReader, string? format);

    /// <summary>
    /// Converts a single record from SLICES to the external standard.
    /// </summary>
    /// <remarks>
    /// Implementations should only mutate <paramref name="serializedWriter"/> and be safe to be called from multiple threads.
    /// <paramref name="digitalObject"/> graph should not be modified until this method returns.
    /// </remarks>
    /// <param name="digitalObject">The record to convert</param>
    /// <param name="format">
    /// The format to use for the return value.
    /// 
    /// Null means that the implementation should assume the default format used by the external standard.
    /// </param>
    /// <param name="serializedWriter">
    /// A writer which will be fed the serialized record represented in the external standard
    /// </param>
    /// <exception cref="UnsupportedExternalFormatException">
    /// Thrown if <paramref name="format"/> is not supported by the implementation.
    /// Not thrown if the format is null.
    /// </exception>
    void ToSerializedExtrenal(DigitalObject digitalObject, string? format, TextWriter serializedWriter);
}

/// <summary>
/// Converter version that operates on non-serialized versions of external records.
/// </summary>
/// <typeparam name="TExternalModel">
/// The typed in-memory representation of the external standard record.
/// </typeparam>
public interface ISlicesStandardConverter<TExternalModel> : ISlicesStandardConverter
{
    /// <summary>
    /// Converts a single record from the external standard to SLICES.
    /// </summary>
    /// <remarks>
    /// Implementations should be pure and safe to be called from multiple threads
    /// </remarks>
    /// <returns>The SLICES version of the record</returns>
    [Pure]
    DigitalObject FromExtrenal(TExternalModel externalModel);

    /// <summary>
    /// Converts a single record from SLICES to the external standard.
    /// </summary>
    /// <remarks>
    /// Implementations should be pure and safe to be called from multiple threads.
    /// <paramref name="digitalObject"/> graph should not be modified until this method returns.
    /// </remarks>
    /// <param name="digitalObject">The record to convert</param>
    /// <returns>Record represented in the external standard</returns>
    [Pure]
    TExternalModel ToExtrenal(DigitalObject digitalObject);
}

using System.Diagnostics.Contracts;
using Slices.V1.Converters.Common.Exceptions;
using Slices.V1.Model;

namespace Slices.V1.Converters.Common;

public interface ISlicesStandardConverter
{
    /// <summary>
    /// A constant string representing which external standard this converter is handling
    /// </summary>
    string ExternalStandard { get; }

    StandardSerializationSupportedFormats SupportedFormats { get; }

    /// <summary>
    /// Converts a single record from the external standard to SLICES.
    /// </summary>
    /// <remarks>
    /// Implementations should only mutate <paramref name="serializedStream"/> and be safe to be called from multiple threads
    /// </remarks>
    /// <param name="serializedStream">
    /// A stream that will provide the serialized representation of the record in the external standard
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
    /// <exception cref="StandardSerializationException">
    /// Thrown if the deserialization has failed. 
    /// </exception>
    /// <returns>The SLICES version of the record</returns>
    Task<SfdoResource> FromSerializedExternalAsync(Stream serializedStream, string? format);

    /// <summary>
    /// Converts a single record from SLICES to the external standard.
    /// </summary>
    /// <remarks>
    /// Implementations should only mutate <paramref name="serializedWriter"/> and be safe to be called from multiple threads.
    /// <paramref name="sfdo"/> graph should not be modified until this method returns.
    /// </remarks>
    /// <param name="sfdo">The record to convert</param>
    /// <param name="format">
    /// The format to use for the return value.
    /// 
    /// Null means that the implementation should assume the default format used by the external standard.
    /// </param>
    /// <param name="serializedStream">
    /// A stream where the serialized record represented in the external standard will be written
    /// </param>
    /// <exception cref="UnsupportedExternalFormatException">
    /// Thrown if <paramref name="format"/> is not supported by the implementation.
    /// Not thrown if the format is null.
    /// </exception>
    /// <exception cref="StandardSerializationException">
    /// Thrown if the serialization has failed. 
    /// </exception>
    Task ToSerializedExternalAsync(SfdoResource sfdo, string? format, Stream serializedStream);
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
    SfdoResource FromExternal(TExternalModel externalModel);

    /// <summary>
    /// Converts a single record from SLICES to the external standard.
    /// </summary>
    /// <remarks>
    /// Implementations should be pure and safe to be called from multiple threads.
    /// <paramref name="sfdo"/> graph should not be modified until this method returns.
    /// </remarks>
    /// <param name="sfdo">The record to convert</param>
    /// <returns>Record represented in the external standard</returns>
    [Pure]
    TExternalModel ToExternal(SfdoResource sfdo);
}
using System.Diagnostics;
using Slices.V1.Converters.Common.Exceptions;
using Slices.V1.Model;

namespace Slices.V1.Converters.Common;

public abstract class BaseStandardConverter<T> : ISlicesStandardConverter<T>
{
    protected readonly ISlicesImporter<T> Importer;
    protected readonly ISlicesExporter<T> Exporter;

    protected BaseStandardConverter(ISlicesImporter<T> importer, ISlicesExporter<T> exporter)
    {
        Importer = importer;
        Exporter = exporter;
    }

    public abstract string ExternalStandard { get; }
    public abstract StandardSerializationSupportedFormats SupportedFormats { get; }

    public abstract Task<SfdoResource> FromSerializedExternalAsync(Stream serializedStream, string? format);
    public abstract Task ToSerializedExternalAsync(SfdoResource sfdo, string? format, Stream serializedStream);

    public SfdoResource FromExternal(T externalModel)
    {
        return Importer.FromExternal(externalModel);
    }

    public T ToExternal(SfdoResource sfdo)
    {
        return Exporter.ToExternal(sfdo);
    }

    protected void ValidateSingleFormatSupported(string? format)
    {
        Debug.Assert(SupportedFormats.AdditionalFormats.Count == 0);
        
        // Unspecified = default
        if (format == null) return;
        
        string supportedFormat = SupportedFormats.DefaultFormat.FormatId;
        
        if (format != supportedFormat)
        {
            throw new UnsupportedExternalFormatException($"Only \"{supportedFormat}\" is supported")
            {
                Format = format,
                ExternalStandard = ExternalStandard,
            };
        }
    }
}

public abstract class BaseXmlStandardConverter<T> : BaseStandardConverter<T>
    where T : class
{
    public override StandardSerializationSupportedFormats SupportedFormats
        => StandardSerializationSupportedFormats.Common.XmlOnly;

    protected readonly IStandardXmlSerializer<T> Serializer;

    protected BaseXmlStandardConverter(
        ISlicesImporter<T> importer,
        ISlicesExporter<T> exporter,
        IStandardXmlSerializer<T> serializer
    ) : base(importer, exporter)
    {
        Serializer = serializer;
    }

    public override async Task<SfdoResource> FromSerializedExternalAsync(Stream serializedStream, string? format)
    {
        ValidateSingleFormatSupported(format);

        return FromExternal(await Serializer.FromXmlAsync(serializedStream));
    }

    public override async Task ToSerializedExternalAsync(SfdoResource sfdo, string? format, Stream serializedStream)
    {
        ValidateSingleFormatSupported(format);
        
        await Serializer.ToXmlAsync(ToExternal(sfdo), serializedStream);
    }
}

public abstract class BaseJsonStandardConverter<T> : BaseStandardConverter<T>
{
    public override StandardSerializationSupportedFormats SupportedFormats
        => StandardSerializationSupportedFormats.Common.JsonOnly;
    
    protected readonly IStandardJsonSerializer<T> Serializer;

    protected BaseJsonStandardConverter(
        ISlicesImporter<T> importer,
        ISlicesExporter<T> exporter,
        IStandardJsonSerializer<T> serializer
    ) : base(importer, exporter)
    {
        Serializer = serializer;
    }

    public override async Task<SfdoResource> FromSerializedExternalAsync(Stream serializedStream, string? format)
    {
        ValidateSingleFormatSupported(format);

        return FromExternal(await Serializer.FromJsonAsync(serializedStream));
    }

    public override async Task ToSerializedExternalAsync(SfdoResource sfdo, string? format, Stream serializedStream)
    {
        ValidateSingleFormatSupported(format);
        
        await Serializer.ToJsonAsync(ToExternal(sfdo), serializedStream);
    }
}
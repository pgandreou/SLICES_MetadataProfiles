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
    public abstract SfdoResource FromSerializedExternal(TextReader serializedReader, string? format);
    public abstract void ToSerializedExternal(SfdoResource digitalObject, string? format, TextWriter serializedWriter);

    public SfdoResource FromExternal(T externalModel)
    {
        return Importer.FromExternal(externalModel);
    }

    public T ToExternal(SfdoResource sfdo)
    {
        return Exporter.ToExternal(sfdo);
    }
}

public abstract class BaseXmlStandardConverter<T> : BaseStandardConverter<T>
    where T : class
{
    protected readonly IStandardXmlSerializer<T> Serializer;

    protected BaseXmlStandardConverter(
        ISlicesImporter<T> importer,
        ISlicesExporter<T> exporter,
        IStandardXmlSerializer<T> serializer
    ) : base(importer, exporter)
    {
        Serializer = serializer;
    }

    public sealed override SfdoResource FromSerializedExternal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExternal(Serializer.FromXml(serializedReader));
    }

    public sealed override void ToSerializedExternal(SfdoResource sfdo, string? format, TextWriter serializedWriter)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        Serializer.ToXml(ToExternal(sfdo), serializedWriter);
    }
}

public abstract class BaseJsonStandardConverter<T> : BaseStandardConverter<T>
{
    protected readonly IStandardJsonSerializer<T> Serializer;

    protected BaseJsonStandardConverter(
        ISlicesImporter<T> importer,
        ISlicesExporter<T> exporter,
        IStandardJsonSerializer<T> serializer
    ) : base(importer, exporter)
    {
        Serializer = serializer;
    }

    public sealed override SfdoResource FromSerializedExternal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "json";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"json\" is supported");
        }

        return FromExternal(Serializer.FromJson(serializedReader));
    }

    public sealed override void ToSerializedExternal(SfdoResource sfdo, string? format, TextWriter serializedWriter)
    {
        if (format == null) format = "json";

        if (format != "json")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"json\" is supported");
        }

        Serializer.ToJson(ToExternal(sfdo), serializedWriter);
    }
}
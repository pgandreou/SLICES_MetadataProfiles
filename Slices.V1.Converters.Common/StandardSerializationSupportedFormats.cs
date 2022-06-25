namespace Slices.V1.Converters.Common;

public record StandardSerializationFormatDescriptor(string FormatId)
{
    public bool IsText { get; init; } = true;
    
    public static class Common
    {
        public static readonly StandardSerializationFormatDescriptor Xml = new("xml");
        public static readonly StandardSerializationFormatDescriptor Json = new("json");
    }
}

public class StandardSerializationSupportedFormats
{
    public readonly StandardSerializationFormatDescriptor DefaultFormat;
    public readonly IReadOnlyCollection<StandardSerializationFormatDescriptor> AdditionalFormats;

    public readonly IReadOnlyCollection<StandardSerializationFormatDescriptor> Formats;
    
    public StandardSerializationSupportedFormats(
        StandardSerializationFormatDescriptor defaultFormat,
        IReadOnlyCollection<StandardSerializationFormatDescriptor>? additionalFormats = null
    )
    {
        DefaultFormat = defaultFormat;
        AdditionalFormats = additionalFormats ?? Array.Empty<StandardSerializationFormatDescriptor>();

        Formats = AdditionalFormats
            .Prepend(defaultFormat)
            .ToArray();
    }

    public static class Common
    {
        public static readonly StandardSerializationSupportedFormats XmlOnly = new(
            StandardSerializationFormatDescriptor.Common.Xml
        );
        
        public static readonly StandardSerializationSupportedFormats JsonOnly = new(
            StandardSerializationFormatDescriptor.Common.Json
        );
    }
}
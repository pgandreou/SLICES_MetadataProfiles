namespace Slices.V1.StandardAnnotations;

public enum SlicesFieldCategory
{
    /// <summary>
    /// Important information that is used to identify and describe the resource
    /// </summary>
    PrimaryInformation,

    /// <summary>
    /// Information related to the management of the S-FDO including the
    /// versioning and metadata profile used to interpret it
    /// </summary>
    ManagementInformation,

    /// <summary>
    /// The manner and mode in which the S-FDO can be accessed
    /// </summary>
    AccessInformation,

    /// <summary>
    /// Persistent references to other objects
    /// </summary>
    Links,

    /// <summary>
    /// Language descriptors
    /// </summary>
    LanguageInformation,

    /// <summary>
    ///  Information related to registered users. The information access mode
    ///  is by default private and only identifiers are used
    /// </summary>
    UserInformation,

    /// <summary>
    /// Legal information related to the rights of access, such as licences, copyrights, etc.
    /// </summary>
    RightsAndTerms,

    // TODO rest
};

[AttributeUsage(AttributeTargets.Property)]
public class SlicesCodeAttribute : Attribute
{
    public readonly SlicesFieldCategory Category;
    public readonly int Order;

    public string Domain { get; set; } = "SLICES";

    public SlicesCodeAttribute(SlicesFieldCategory category, int order)
    {
        Category = category;
        Order = order;
    }

    public string ToCodeString() => $"{Domain}.{Category.ToCodeString()}.{Order:D2}";
}

public static class SlicesCodeExtensions
{
    public static string ToCodeString(this SlicesFieldCategory category) => category switch
    {
        SlicesFieldCategory.PrimaryInformation => "PI",
        SlicesFieldCategory.ManagementInformation => "MA",
        SlicesFieldCategory.AccessInformation => "AC",
        SlicesFieldCategory.Links => "LN",
        SlicesFieldCategory.LanguageInformation => "LA",
        SlicesFieldCategory.UserInformation => "US",
        SlicesFieldCategory.RightsAndTerms => "RT",

        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unhanded field category"),
    };
}

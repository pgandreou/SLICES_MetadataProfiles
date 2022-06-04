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

        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unhanded field category"),
    };
}

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

    /// <summary>
    /// Classification of the S-FDO in specific scientific domains
    /// </summary>
    ClassificationInformation,

    /// <summary>
    /// Specific publication information such as the date of submission and acceptance
    /// </summary>
    PublicationInformation,

    /// <summary>
    /// Information related to the financial aspects of access for the S-FDO
    /// </summary>
    FinancialInformation,

    /// <summary>
    /// Support information, such as helpdesk URLs, manuals, terms of use etc.
    /// </summary>
    SupportInformation,

    /// <summary>
    /// Information related to the funding instruments (e.g., body, project) of the S-FDO
    /// </summary>
    AttributionInformation,

    /// <summary>
    /// Fields utilized specifically with services and software and describe
    /// important information such as standards, opensource technologies and certifications
    /// </summary>
    MaturityInformation,

    /// <summary>
    /// Location and Time descriptors. Locations use Geographic coordinates when applicable
    /// </summary>
    SpatioTemporalInformation,

    /// <summary>
    ///  Specific geographic coordinate attributes i.e., latitude, longitude and altitude
    /// </summary>
    GeographicInformation,

    /// <summary>
    /// Specific dataset descriptors, such as format, size
    /// </summary>
    DatasetInformation,

    /// <summary>
    /// Software related attributes, such as repository, documentation and
    /// programming language used to develop the software
    /// </summary>
    Software,
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
        SlicesFieldCategory.ClassificationInformation => "CL",
        SlicesFieldCategory.PublicationInformation => "PB",
        SlicesFieldCategory.FinancialInformation => "FN",
        SlicesFieldCategory.SupportInformation => "SU",
        SlicesFieldCategory.AttributionInformation => "AT",
        SlicesFieldCategory.MaturityInformation => "MA", // TODO: same as ManagementInformation
        SlicesFieldCategory.SpatioTemporalInformation => "ST",
        SlicesFieldCategory.GeographicInformation => "GE",
        SlicesFieldCategory.DatasetInformation => "DA",
        SlicesFieldCategory.Software => "SW",

        _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unhanded field category"),
    };
}

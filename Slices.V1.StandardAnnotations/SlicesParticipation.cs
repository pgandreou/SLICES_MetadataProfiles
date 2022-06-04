namespace Slices.V1.StandardAnnotations;

public enum SlicesParticipationType
{
    /// <summary>
    /// A value must be provided for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The format model property must be not nullable
    /// </remarks>
    Required,

    /// <summary>
    /// A value must be provided if it exists for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The format model property must be nullable
    /// </remarks>
    RequiredIfExists,

    /// <summary>
    /// A value may be provided for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The format model property must be nullable
    /// </remarks>
    Optional,
}

[AttributeUsage(AttributeTargets.Property)]
public class SlicesParticipationAttribute : Attribute
{
    public readonly SlicesParticipationType Type;

    public SlicesParticipationAttribute(SlicesParticipationType type)
    {
        Type = type;
    }
}

namespace Slices.V1.Model.Annotations;

public enum SlicesParticipationType
{
    /// <summary>
    /// A value must be provided for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The property must be not nullable.
    /// If the property is a collection, then it is expected to be non-empty.
    /// </remarks>
    Required,

    /// <summary>
    /// A value must be provided if it exists for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The property must be nullable, unless it is a collection (which can be empty instead).
    /// </remarks>
    RequiredIfExists,

    /// <summary>
    /// A value may be provided for the metadata attribute
    /// </summary>
    /// <remarks>
    /// The property must be nullable, unless it is a collection (which can be empty instead).
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

namespace Slices.V1.Model.Annotations;

public enum SlicesParticipationType
{
    /// <summary>
    /// A value must be provided for the metadata attribute
    /// </summary>
    Required,

    /// <summary>
    /// A value must be provided if it exists for the metadata attribute
    /// </summary>
    RequiredIfExists,

    /// <summary>
    /// A value may be provided for the metadata attribute
    /// </summary>
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

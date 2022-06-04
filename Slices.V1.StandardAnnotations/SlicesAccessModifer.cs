namespace Slices.V1.StandardAnnotations;

// TODO: Better names
public enum SlicesAccessModiferType
{
    PU,
    PR,
};

[AttributeUsage(AttributeTargets.Property)]
public class SlicesAccessModiferAttribute : Attribute
{
    public readonly SlicesAccessModiferType Type;

    public SlicesAccessModiferAttribute(SlicesAccessModiferType type)
    {
        Type = type;
    }
}

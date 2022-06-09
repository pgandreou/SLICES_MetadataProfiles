namespace Slices.V1.Standard;

public class SfdoContributor : ISfdoCreatorLike
{
    public string Name { get; set; } = null!;
    public SfdoIdentifier? Identifier { get; set; }
}

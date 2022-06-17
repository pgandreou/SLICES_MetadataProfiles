namespace Slices.V1.Model;

public class SfdoContributor : ISfdoCreatorLike
{
    public string Name { get; set; } = null!;
    public SfdoIdentifier? Identifier { get; set; }
}

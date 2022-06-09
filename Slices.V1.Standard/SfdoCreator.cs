namespace Slices.V1.Standard;

public interface ISfdoCreatorLike
{
    string Name { get; set; }
    SfdoIdentifier? Identifier { get; set; }
}

public class SfdoCreator : ISfdoCreatorLike
{
    public string Name { get; set; } = null!;
    public SfdoIdentifier? Identifier { get; set; }
}

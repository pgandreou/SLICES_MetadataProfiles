namespace Slices.V1.Standard;

public struct SfdoRelationLink : IEquatable<SfdoRelationLink>
{
    public string Identifier { get; set; }
    public string RelationshipType { get; set; }
    public string ResourceType { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is SfdoRelationLink link && Equals(link);
    }

    public bool Equals(SfdoRelationLink other)
    {
        return Identifier == other.Identifier &&
               RelationshipType == other.RelationshipType &&
               ResourceType == other.ResourceType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Identifier, RelationshipType, ResourceType);
    }

    public static bool operator ==(SfdoRelationLink left, SfdoRelationLink right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SfdoRelationLink left, SfdoRelationLink right)
    {
        return !(left == right);
    }
}

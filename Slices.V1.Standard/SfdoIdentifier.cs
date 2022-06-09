namespace Slices.V1.Standard;

public static class SfdoIdentifierTypes
{
    public static readonly string Doi = "DOI";
    public static readonly string Url = "URL";
    public static readonly string Orcid = "ORCID";
}

public struct SfdoIdentifier : IEquatable<SfdoIdentifier>
{
    // TODO: restrict or free form?
    public string Type;
    public string Value;

    public override string ToString()
    {
        return $"{Type}:{Value}";
    }

    public override bool Equals(object? obj)
    {
        return obj is SfdoIdentifier identifier && Equals(identifier);
    }

    public bool Equals(SfdoIdentifier other)
    {
        return Type == other.Type &&
               Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }

    public static bool operator ==(SfdoIdentifier left, SfdoIdentifier right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SfdoIdentifier left, SfdoIdentifier right)
    {
        return !(left == right);
    }
}

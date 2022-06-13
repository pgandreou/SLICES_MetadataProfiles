namespace Slices.V1.Standard;

public static class SfdoIdentifierTypes
{
    public static readonly string Doi = "DOI";
    public static readonly string Url = "URL";
    public static readonly string Orcid = "ORCID";
    public static readonly string Arxiv = "arXiv";
}

public struct SfdoIdentifier : IEquatable<SfdoIdentifier>
{
    /// <summary>
    /// Null means the type isn't known and should be treated as "other"
    /// </summary>
    public string? Type { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
        if (Type == null)
        {
            return Value;
        }

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

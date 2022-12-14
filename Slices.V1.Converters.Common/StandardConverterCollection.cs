namespace Slices.V1.Converters.Common;

public interface IStandardConverterCollection
{
    IReadOnlyDictionary<string, ISlicesStandardConverter> CovertersByStandard { get; }
}

public class StandardConverterCollection : IStandardConverterCollection
{
    public IReadOnlyDictionary<string, ISlicesStandardConverter> CovertersByStandard { get; }

    public StandardConverterCollection(IEnumerable<ISlicesStandardConverter> converters)
    {
        CovertersByStandard = converters.ToDictionary(c => c.ExternalStandard);
    }
}

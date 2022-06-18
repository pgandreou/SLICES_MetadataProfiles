using System.Diagnostics.Contracts;
using Slices.V1.Model;

namespace Slices.V1.Converters.Common;

public interface ISlicesExporter<TExternalModel>
{
    [Pure]
    TExternalModel ToExternal(SfdoResource sfdo);
}

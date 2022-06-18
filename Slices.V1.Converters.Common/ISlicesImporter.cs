using System.Diagnostics.Contracts;
using Slices.V1.Model;

namespace Slices.V1.Converters.Common;

public interface ISlicesImporter<TExternalModel>
{
    [Pure]
    SfdoResource FromExternal(TExternalModel externalModel);
}

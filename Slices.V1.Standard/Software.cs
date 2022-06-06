using Slices.V1.StandardAnnotations;

namespace Slices.V1.Standard;

public sealed class Software
{
    #region Primary

    /// <summary>
    /// Webpage with information about the Resource usually hosted and maintained by the Provider.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 12)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public Uri? Webpage { get; set; }

    /// <summary>
    /// Link to the logo/visual identity of the Resource. The logo will be visible at the Portal.
    /// If there is no specific logo for the Resource the logo of the Provider may be used.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 13)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public Uri? Logo { get; set; }

    #endregion Primary

    // TODO
}

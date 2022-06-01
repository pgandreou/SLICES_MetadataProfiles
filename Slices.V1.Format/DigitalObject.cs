using Slices.V1.FormatAnnotations;

namespace Slices.V1.Format;

// TODO: String length

public sealed class DigitalObject
{
    #region Primary

    // TODO: Better type
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string Identifier { get; set; } = null!;

    // TODO: Better type
    /// <summary>
    /// Internal Code created within SLICES which can be resolved by the SLICES portal.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string InternalIdentifier { get; set; } = null!;

    /// <summary>
    /// An alternate identifier to the resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? AlternateIdentifier { get; set; }

    // TODO: Creators?
    /// <summary>
    /// The creator(s) of the digital object in priority order. May be a corporate/institutional or personal name.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 4)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    public List<string> Creator { get; set; } = null!;

    // TODO: Better type
    // TODO: CreatorIdentifiers?
    /// <summary>
    /// Identifier(s) of the creator(s) of the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 5)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    public List<string>? CreatorIdentifier { get; set; }

    /// <summary>
    /// A name or title by which a digital object is known.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 6)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string Name { get; set; } = null!;

    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 7)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? Description { get; set; }

    // TODO

    #endregion Primary
}
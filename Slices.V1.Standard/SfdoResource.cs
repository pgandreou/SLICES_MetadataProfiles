﻿using Slices.V1.StandardAnnotations;

namespace Slices.V1.Standard;

public enum SfdoAccessType
{
    Remote,
    Physical,
    Virtual,
    Other,
}

public enum SfdoAccessMode
{
    Free,
    FreeConditionally,
    ExcellenceBased,
}

// TODO: String length

public sealed class SfdoResource
{
    #region Primary

    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public SfdoIdentifier Identifier { get; set; }

    /// <summary>
    /// Internal Code created within SLICES which can be resolved by the SLICES portal.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public SfdoIdentifier InternalIdentifier { get; set; }

    /// <summary>
    /// An alternate identifiers to the resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoIdentifier> AlternateIdentifiers { get; set; } = new();

    /// <summary>
    /// The creator(s) of the digital object in priority order. May be a corporate/institutional or personal name.
    /// </summary>
    //[SlicesCode(SlicesFieldCategory.PrimaryInformation, 4)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    public List<SfdoCreator> Creators { get; set; } = new();

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

    // TODO: ResourceType

    /// <summary>
    /// The subject represented using key phrases, or classification codes. 
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 9)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<string> Subjects { get; set; } = new();

    /// <summary>
    /// An index term, subject term, subject heading, or descriptor, in information retrieval, is a term that captures the essence of the topic of a document.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 10)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<string> Keywords { get; set; } = new();

    /// <summary>
    /// A timestamp generated by the system.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 11)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public DateTime DateTimeCreated { get; set; }

    #endregion Primary

    #region Management

    /// <summary>
    /// Version of the resource.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.ManagementInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string Version { get; set; } = null!;

    /// <summary>
    /// The metadata profile used  to describe this  resource
    /// </summary>
    /// <remarks>The version of the SLICES standard</remarks>
    [SlicesCode(SlicesFieldCategory.ManagementInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string MetadataProfile { get; set; } = "V1";

    /// <summary>
    /// The institution or person responsible for collecting, managing, distributing, or otherwise contributing
    /// to the development of the digital object. Examples of cotributors are chair, contact group, contact person,
    /// contact for access. data collector, data curator,data manager, distributor, editor. funder. hosting institution.
    /// maintainer, producer, project leader, project manager, project member, provider, publisher, reader,
    /// registration agency, registration authority, related person, researcher, research group, review assistant,
    /// reviewer, reviewer-external, rights holder, sponsor, stats-reviewer, supervisor, translator, workpackage leader.
    /// </summary>
    //[SlicesCode(SlicesFieldCategory.ManagementInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    public List<SfdoContributor> Contributors { get; set; } = new();

    #endregion Management

    #region Access

    /// <summary>
    /// The way a user can access the Resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.AccessInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoAccessType> AccessTypes { get; set; } = new() { SfdoAccessType.Remote };

    /// <summary>
    /// Eligibility/criteria for granting access to users
    /// </summary>
    [SlicesCode(SlicesFieldCategory.AccessInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoAccessMode> AccessModes { get; set; } = new() { SfdoAccessMode.Free };

    #endregion Access

    #region Links

    /// <summary>
    /// List of other Resources required to use this Resource.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.Links, 1)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoRelationLink> RequiredObjects { get; set; } = new();

    /// <summary>
    /// List of other Resources that are commonly used with this Resource.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.Links, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoRelationLink> RelatedObjects { get; set; } = new();

    #endregion Links

    #region Language

    // TODO: should this be a list or single?
    /// <summary>
    /// The primary language of the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.LanguageInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<LanguageIso639_3> PrimaryLanguage { get; set; } = new();

    /// <summary>
    /// Other languages provided for the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.LanguageInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<LanguageIso639_3> OtherLanguages { get; set; } = new();

    #endregion Language

    #region UserInformation

    /// <summary>
    /// Main contact for the resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.UserInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    public SfdoIdentifier? Contact { get; set; }

    /// <summary>
    /// Public contact for the resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.UserInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public SfdoIdentifier? PublicContact { get; set; }

    #endregion UserInformation

    #region RightsAndTerms

    /// <summary>
    /// Rights related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string Rights { get; set; } = null!;

    /// <summary>
    /// A URI for the rights related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public Uri? RightsURI { get; set; }

    /// <summary>
    /// Access rights related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 3)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string AccessRights { get; set; } = null!;

    /// <summary>
    /// License related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 4)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string License { get; set; } = null!;

    /// <summary>
    /// A URI for the licence related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 5)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public Uri? LicenseURI { get; set; }

    /// <summary>
    /// Copyrights holder related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 6)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? CopyrightsHolder { get; set; }

    /// <summary>
    /// Confidentiality declaration for the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 7)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? ConfidentialityDeclaration { get; set; }

    /// <summary>
    /// Special permissions related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 8)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? SpecialPermissions { get; set; }

    /// <summary>
    /// Restrictions related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 9)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? Restrictions { get; set; }

    /// <summary>
    /// Citation requirements related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 10)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? CitationRequirements { get; set; }

    /// <summary>
    /// Conditions related with the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 11)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? Conditions { get; set; }

    /// <summary>
    /// Disclaimer for the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 12)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? Disclaimer { get; set; }

    /// <summary>
    /// A statement of any changes in ownership and custody of the resource since its creation
    /// that are significant for its authenticity, integrity, and interpretation.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.RightsAndTerms, 13)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public string? Provenance { get; set; }

    #endregion RightsAndTerms

    #region TypeSpecific

    public Software? Software { get; set; }

    // TODO

    #endregion TypeSpecific
}

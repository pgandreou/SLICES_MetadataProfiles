﻿using Slices.V1.Model.Annotations;

namespace Slices.V1.Model;

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

    /// <summary>
    /// The type(s) of the digital object
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 8)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    public List<SfdoResourceType> ResourceTypes { get; set; } = new();

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
    
    /// <summary>
    /// Webpage with information about the Resource usually hosted and maintained by the Provider.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 12)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Provider)]
    public SfdoOptional<Uri> WebPage { get; set; }
    
    /// <summary>
    /// Link to the logo/visual identity of the Resource. The logo will be visible at the Portal.
    /// If there is no specific logo for the Resource the logo of the Provider may be used.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 13)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Provider)]
    public SfdoOptional<Uri> Logo { get; set; }
    
    /// <summary>
    /// The name of the entity that holds, archives, publishes prints, distributes, releases, issues, or produces
    /// the resource. This property will be used to formulate the citation, so consider the prominence of the role. 
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 14)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<string> Publisher { get; set; }

    /// <summary>
    /// The year when the digital object was or will be made publicly available. In the case of resources such as
    /// software or dynamic data where there may be multiple releases in one year, include the dateType vocabulary.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PrimaryInformation, 15)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<int> PublicationYear { get; set; }

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
    
    #region Classification

    /// <summary>
    /// The branch of science, scientific discipline that is related to the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.ClassificationInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(
        SfdoResourceType.Publication,
        SfdoResourceType.Provider,
        SfdoResourceType.Data
    )]
    public SfdoOptional<List<string>> ScientificDomains { get; set; }

    /// <summary>
    /// The subbranch of science, scientific subdicipline that is related to the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.ClassificationInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(
        SfdoResourceType.Publication,
        SfdoResourceType.Provider,
        SfdoResourceType.Data
    )]
    public SfdoOptional<List<string>> ScientificSubdomains { get; set; }

    #endregion
    
    #region Publication

    /// <summary>
    /// The date the object was submitted.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PublicationInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<DateOnly> DateSubmitted { get; set; }

    /// <summary>
    /// The date(s) the object was modified.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PublicationInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<List<DateOnly>> DatesModified { get; set; }

    /// <summary>
    /// The date(s) the object was issued.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PublicationInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<List<DateOnly>> DatesIssued { get; set; }

    /// <summary>
    /// The date(s) the object was accepted.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PublicationInformation, 4)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<List<DateOnly>> DatesAccepted { get; set; }

    /// <summary>
    /// The date(s) the object was copyrighted.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.PublicationInformation, 5)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Publication)]
    public SfdoOptional<List<DateOnly>> DatesCopyrighted { get; set; }

    #endregion

    #region Financial

    /// <summary>
    /// Webpage with the supported payment models and restrictions that apply to each of them
    /// </summary>
    [SlicesCode(SlicesFieldCategory.FinancialInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<Uri> PaymentModel { get; set; }
    
    /// <summary>
    /// Webpage with the information on the price scheme for this Object in case the customer is charged for.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.FinancialInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<Uri> Pricing { get; set; }

    #endregion

    #region SpatioTemporal

    /// <summary>
    /// The address of the resource
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SpatioTemporalInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<string> Address { get; set; }

    /// <summary>
    /// Object's start date (e.g., start date of data recording)
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SpatioTemporalInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<DateTime> DateTimeStart { get; set; }

    /// <summary>
    /// Object's end date (e.g., end date of data recording)
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SpatioTemporalInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<DateTime> DateTimeEnd { get; set; }

    // TODO: type
    /// <summary>
    /// Combination of Latitude, Longitude and Altitude Coordinate(s)
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SpatioTemporalInformation, 4)]
    [SlicesParticipation(SlicesParticipationType.Optional)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<List<string>> Locations { get; set; }

    #endregion

    #region Dataset

    /// <summary>
    /// The size of the digital object in bytes.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<int> Size { get; set; }

    /// <summary>
    /// Duration in ISO 8601 format.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<string> Duration { get; set; }

    /// <summary>
    /// The format of the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 2)] // TODO
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<List<string>> Formats { get; set; }

    /// <summary>
    /// The material or physical carrier of the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 3)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<List<string>> Mediums { get; set; }
    
    /// <summary>
    /// The compression format of the distribution in which the digital object is contained in
    /// a compressed form, e.g. to reduce the size of the downloadable file. (source DCAT)
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 4)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<List<string>> CompressionFormats { get; set; }
    
    /// <summary>
    /// Metadata about file (name, title, description, format, mimetype, type of file, persistentID,
    /// download URL, format, size, compression format, checksum, checksum algorithm, bitrate,
    /// duration encodedinf format, upload date, distribution…)
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 5)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<string> FileInfo { get; set; }
    
    /// <summary>
    /// Information about data schema or standard of the digital object.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.DatasetInformation, 6)]
    [SlicesParticipation(SlicesParticipationType.RequiredIfExists)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Data)]
    public SfdoOptional<string> DataStandard { get; set; }
    
    #endregion
    
    #region Support

    /// <summary>
    /// The email to ask more information from the Provider about this Resource.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SupportInformation, 1)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PU)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Provider)]
    public SfdoOptional<string> HelpdeskEmail { get; set; }

    /// <summary>
    /// The email to contact the Provider for critical secutiry issues about this Resource.
    /// </summary>
    [SlicesCode(SlicesFieldCategory.SupportInformation, 2)]
    [SlicesParticipation(SlicesParticipationType.Required)]
    [SlicesAccessModifer(SlicesAccessModiferType.PR)]
    [SlicesAssociatedResourceTypes(SfdoResourceType.Provider)]
    public SfdoOptional<string> SecurityContactEmail { get; set; }
    
    #endregion

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
}

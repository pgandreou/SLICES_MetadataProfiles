using System.Text.Json.Serialization;

namespace Slices.V1.Converters.EoscProviderProfile;

#nullable disable

public class EoscProviderRecord
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; }
    
    [JsonPropertyName("website")]
    public string Website { get; set; }
    
    [JsonPropertyName("legal_entity")]
    public bool LegalEntity { get; set; }
    
    [JsonPropertyName("legal_status")]
    public string LegalStatus { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("logo")]
    public string Logo { get; set; }
    
    [JsonPropertyName("multimedia")]
    public string[] Multimedia { get; set; }
    
    [JsonPropertyName("scientific_domains")]
    public string[] ScientificDomains { get; set; }
    
    [JsonPropertyName("scientific_subdomains")]
    public string[] ScientificSubdomains { get; set; }
    
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }
    
    [JsonPropertyName("street_name_number")]
    public string StreetNameNumber { get; set; }
    
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }
    
    [JsonPropertyName("city")]
    public string City { get; set; }
    
    [JsonPropertyName("region")]
    public string Region { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("main_contact_first_name")]
    public string MainContactFirstName { get; set; }
    
    [JsonPropertyName("main_contact_last_name")]
    public string MainContactLastName { get; set; }
    
    [JsonPropertyName("main_contact_email")]
    public string MainContactEmail { get; set; }
    
    [JsonPropertyName("main_contact_phone")]
    public string MainContactPhone { get; set; }
    
    [JsonPropertyName("main_contact_position")]
    public string MainContactPosition { get; set; }
    
    [JsonPropertyName("public_contact_first_name")]
    public string PublicContactFirstName { get; set; }
    
    [JsonPropertyName("public_contact_last_name")]
    public string PublicContactLastName { get; set; }
    
    [JsonPropertyName("public_contact_email")]
    public string PublicContactEmail { get; set; }
    
    [JsonPropertyName("public_contact_phone")]
    public string PublicContactPhone { get; set; }
    
    [JsonPropertyName("public_contact_position")]
    public string PublicContactPosition { get; set; }
    
    [JsonPropertyName("life_cycle_status")]
    public string LifeCycleStatus { get; set; }
    
    [JsonPropertyName("certifications")]
    public string[] Certifications { get; set; }
    
    [JsonPropertyName("hosting_legal_entity")]
    public string HostingLegalEntity { get; set; }
    
    [JsonPropertyName("participating_countries")]
    public string[] ParticipatingCountries { get; set; }
    
    [JsonPropertyName("affiliations")]
    public string[] Affiliations { get; set; }
    
    [JsonPropertyName("networks")]
    public string[] Networks { get; set; }
    
    [JsonPropertyName("structure_types")]
    public string[] StructureTypes { get; set; }
    
    [JsonPropertyName("esfri_domains")]
    public string[] EsfriDomains { get; set; }
    
    [JsonPropertyName("esfri_type")]
    public string EsfriType { get; set; }
    
    [JsonPropertyName("meril_scientific_domains")]
    public string[] MerilScientificDomains { get; set; }
    
    [JsonPropertyName("meril_scientific_subdomains")]
    public string[] MerilScientificSubdomains { get; set; }
    
    [JsonPropertyName("areas_of_activity")]
    public string[] AreasOfActivity { get; set; }
    
    [JsonPropertyName("societal_grand_challenges")]
    public string[] SocietalGrandChallenges { get; set; }
    
    [JsonPropertyName("national_roadmaps")]
    public string[] NationalRoadmaps { get; set; }
}

#nullable restore
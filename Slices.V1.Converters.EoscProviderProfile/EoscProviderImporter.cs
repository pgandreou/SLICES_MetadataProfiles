using Slices.V1.Converters.Common;
using Slices.V1.Model;

namespace Slices.V1.Converters.EoscProviderProfile;

public class EoscProviderImporter : ISlicesImporter<EoscProviderRecord>
{
    public SfdoResource FromExternal(EoscProviderRecord providerProfile)
    {
        SfdoResource sfdo = new();

        sfdo.Identifier = new SfdoIdentifier { Value = providerProfile.Id }; // TODO: Parse?

        sfdo.Name = providerProfile.Name;

        sfdo.Description = providerProfile.Description;
        
        sfdo.ResourceTypes.Add(SfdoResourceType.Provider);
        
        // TODO: Tags -> subjects?

        try
        {
            sfdo.WebPage = new Uri(providerProfile.Website);
        }
        catch (FormatException)
        {
            sfdo.WebPage = SfdoOptional.WithAbsent("Failed to parse value");
        }
        
        try
        {
            sfdo.Logo = new Uri(providerProfile.Logo);
        }
        catch (FormatException)
        {
            sfdo.Logo = SfdoOptional.WithAbsent("Failed to parse value");
        }

        sfdo.ScientificDomains = providerProfile.ScientificDomains.ToList();
        sfdo.ScientificSubdomains = providerProfile.ScientificSubdomains.ToList();

        sfdo.HelpdeskEmail = SfdoOptional.WithAbsent(EoscProviderConstants.CannotImportReason);
        sfdo.SecurityContactEmail = SfdoOptional.WithAbsent(EoscProviderConstants.CannotImportReason);

        sfdo.Abbreviation = providerProfile.Abbreviation;
        sfdo.LegalEntity = providerProfile.LegalEntity;
        sfdo.LegalStatus = providerProfile.LegalStatus;
        sfdo.Multimedia = providerProfile.Multimedia.ToList();
        sfdo.Tags = providerProfile.Tags.ToList();
        sfdo.StreetNameNumber = providerProfile.StreetNameNumber;
        sfdo.PostalCode = providerProfile.PostalCode;
        sfdo.City = providerProfile.City;
        sfdo.Region = providerProfile.Region;
        sfdo.Country = providerProfile.Country;
        sfdo.MainContactFirstName = providerProfile.MainContactFirstName;
        sfdo.MainContactLastName = providerProfile.MainContactLastName;
        sfdo.MainContactEmail = providerProfile.MainContactEmail;
        sfdo.MainContactPhone = providerProfile.MainContactPhone;
        sfdo.MainContactPosition = providerProfile.MainContactPosition;
        sfdo.PublicContactFirstName = providerProfile.PublicContactFirstName;
        sfdo.PublicContactLastName = providerProfile.PublicContactLastName;
        sfdo.PublicContactEmail = providerProfile.PublicContactEmail;
        sfdo.PublicContactPhone = providerProfile.PublicContactPhone;
        sfdo.PublicContactPosition = providerProfile.PublicContactPosition;
        sfdo.LifeCycleStatus = providerProfile.LifeCycleStatus;
        sfdo.Certifications = providerProfile.Certifications.ToList();
        sfdo.HostingLegalEntity = providerProfile.HostingLegalEntity;
        sfdo.ParticipatingCountries = providerProfile.ParticipatingCountries.ToList();
        sfdo.Affiliations = providerProfile.Affiliations.ToList();
        sfdo.Networks = providerProfile.Networks.ToList();
        sfdo.StructureTypes = providerProfile.StructureTypes.ToList();
        sfdo.EsfriDomains = providerProfile.EsfriDomains.ToList();
        sfdo.EsfriType = providerProfile.EsfriType;
        sfdo.MerilScientificDomains = providerProfile.MerilScientificDomains.ToList();
        sfdo.MerilScientificSubdomains = providerProfile.MerilScientificSubdomains.ToList();
        sfdo.AreasOfActivity = providerProfile.AreasOfActivity.ToList();
        sfdo.SocietalGrandChallenges = providerProfile.SocietalGrandChallenges.ToList();
        sfdo.NationalRoadmaps = providerProfile.NationalRoadmaps.ToList();
        
        return sfdo;
    }
}

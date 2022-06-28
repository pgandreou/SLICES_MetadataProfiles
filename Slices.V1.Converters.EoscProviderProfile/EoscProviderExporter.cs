using Slices.V1.Converters.Common;
using Slices.V1.Model;

namespace Slices.V1.Converters.EoscProviderProfile;

public class EoscProviderExporter : ISlicesExporter<EoscProviderRecord>
{
    public EoscProviderRecord ToExternal(SfdoResource sfdo)
    {
        EoscProviderRecord profile = new();

        profile.Id = sfdo.Identifier.ToString();

        profile.Name = sfdo.Name;
        profile.Description = sfdo.Description.ValueOrDefault();

        profile.Website = sfdo.WebPage.ValueOrDefault()?.ToString();
        profile.Logo = sfdo.Logo.ValueOrDefault()?.ToString();

        profile.ScientificDomains = OptionalListToArray(sfdo.ScientificDomains);
        profile.ScientificSubdomains = OptionalListToArray(sfdo.ScientificDomains);

        profile.Abbreviation = sfdo.Abbreviation.ValueOrDefault();
        profile.LegalEntity = sfdo.LegalEntity.ValueOrDefault();
        profile.LegalStatus = sfdo.LegalStatus.ValueOrDefault();
        profile.Multimedia = OptionalListToArray(sfdo.Multimedia);
        profile.Tags = OptionalListToArray(sfdo.Tags);
        profile.StreetNameNumber = sfdo.StreetNameNumber.ValueOrDefault();
        profile.PostalCode = sfdo.PostalCode.ValueOrDefault();
        profile.City = sfdo.City.ValueOrDefault();
        profile.Region = sfdo.Region.ValueOrDefault();
        profile.Country = sfdo.Country.ValueOrDefault();
        profile.MainContactFirstName = sfdo.MainContactFirstName.ValueOrDefault();
        profile.MainContactLastName = sfdo.MainContactLastName.ValueOrDefault();
        profile.MainContactEmail = sfdo.MainContactEmail.ValueOrDefault();
        profile.MainContactPhone = sfdo.MainContactPhone.ValueOrDefault();
        profile.MainContactPosition = sfdo.MainContactPosition.ValueOrDefault();
        profile.PublicContactFirstName = sfdo.PublicContactFirstName.ValueOrDefault();
        profile.PublicContactLastName = sfdo.PublicContactLastName.ValueOrDefault();
        profile.PublicContactEmail = sfdo.PublicContactEmail.ValueOrDefault();
        profile.PublicContactPhone = sfdo.PublicContactPhone.ValueOrDefault();
        profile.PublicContactPosition = sfdo.PublicContactPosition.ValueOrDefault();
        profile.LifeCycleStatus = sfdo.LifeCycleStatus.ValueOrDefault();
        profile.Certifications = OptionalListToArray(sfdo.Certifications);
        profile.HostingLegalEntity = sfdo.HostingLegalEntity.ValueOrDefault();
        profile.Affiliations = OptionalListToArray(sfdo.Affiliations);
        profile.Networks = OptionalListToArray(sfdo.Networks);
        profile.StructureTypes = OptionalListToArray(sfdo.StructureTypes);
        profile.EsfriDomains = OptionalListToArray(sfdo.EsfriDomains);
        profile.EsfriType = sfdo.EsfriType.ValueOrDefault();
        profile.MerilScientificDomains = OptionalListToArray(sfdo.MerilScientificDomains);
        profile.MerilScientificSubdomains = OptionalListToArray(sfdo.MerilScientificSubdomains);
        profile.AreasOfActivity = OptionalListToArray(sfdo.AreasOfActivity);
        profile.SocietalGrandChallenges = OptionalListToArray(sfdo.SocietalGrandChallenges);
        profile.NationalRoadmaps = OptionalListToArray(sfdo.NationalRoadmaps);

        return profile;
    }

    private static T[] OptionalListToArray<T>(SfdoOptional<List<T>> optional) 
        => optional.IsSet ? optional.Value.ToArray() : Array.Empty<T>();
}
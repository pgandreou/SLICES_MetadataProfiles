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

        sfdo.SecurityContactEmail = providerProfile.MainContactEmail; // TODO
        
        return sfdo;
    }
}

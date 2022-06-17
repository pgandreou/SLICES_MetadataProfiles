using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("contributorType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteContributorType
{
    ContactPerson,

    DataCollector,

    DataCurator,

    DataManager,

    Distributor,

    Editor,

    HostingInstitution,

    Other,

    Producer,

    ProjectLeader,

    ProjectManager,

    ProjectMember,

    RegistrationAgency,

    RegistrationAuthority,

    RelatedPerson,

    ResearchGroup,

    RightsHolder,

    Researcher,

    Sponsor,

    Supervisor,

    WorkPackageLeader,
}

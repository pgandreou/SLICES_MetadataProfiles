using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceContributor
{
    public DataCiteResourceContributorName contributorName { get; set; }

    public object givenName { get; set; }

    public object familyName { get; set; }

    [XmlElement("nameIdentifier")]
    public object[] nameIdentifier { get; set; }

    [XmlElement("affiliation")]
    public object[] affiliation { get; set; }

    [XmlAttribute]
    public DataCiteContributorType contributorType { get; set; }
}

#nullable restore

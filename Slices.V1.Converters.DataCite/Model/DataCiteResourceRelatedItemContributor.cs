using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedItemContributor
{
    public DataCiteResourceRelatedItemContributorName contributorName { get; set; }

    public string givenName { get; set; }

    public string familyName { get; set; }

    [XmlAttribute]
    public DataCiteContributorType contributorType { get; set; }
}

#nullable restore

using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteCreator
{
    public DataCiteCreatorName creatorName { get; set; }

    public string givenName { get; set; }

    public string familyName { get; set; }

    [XmlElement("nameIdentifier")]
    public DataCiteNameIdentifier[] nameIdentifier { get; set; }

    [XmlElement("affiliation")]
    public DataCiteAffiliation[] affiliation { get; set; }
}

#nullable restore

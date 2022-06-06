using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteCreator
{
    public DataCiteCreatorName creatorName { get; set; }

    public object givenName { get; set; }

    public object familyName { get; set; }

    [XmlElement("nameIdentifier")]
    public object[] nameIdentifier { get; set; }

    [XmlElement("affiliation")]
    public object[] affiliation { get; set; }
}

#nullable restore

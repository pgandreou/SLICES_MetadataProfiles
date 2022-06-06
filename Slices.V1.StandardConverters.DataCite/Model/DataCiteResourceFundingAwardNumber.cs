using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceFundingAwardNumber
{
    [XmlAttribute(DataType = "anyURI")]
    public string awardURI { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

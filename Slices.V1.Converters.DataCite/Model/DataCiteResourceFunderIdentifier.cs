using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceFunderIdentifier
{
    [XmlAttribute]
    public DataCiteFunderIdentifierType funderIdentifierType { get; set; }

    [XmlAttribute(DataType = "anyURI")]
    public string schemeURI { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

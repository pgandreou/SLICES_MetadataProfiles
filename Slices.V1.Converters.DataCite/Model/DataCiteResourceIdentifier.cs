using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceIdentifier
{
    [XmlAttribute]
    public string identifierType { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

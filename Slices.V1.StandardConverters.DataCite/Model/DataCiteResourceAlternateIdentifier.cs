using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceAlternateIdentifier
{
    [XmlAttribute("alternateIdentifierType")]
    public string Type { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType("nameIdentifier", Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteNameIdentifier
{
    [XmlAttribute]
    public string nameIdentifierScheme { get; set; }

    [XmlAttribute]
    public string schemeURI { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

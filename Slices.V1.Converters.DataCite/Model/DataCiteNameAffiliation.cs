using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType("affiliation", Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteAffiliation
{
    [XmlAttribute]
    public string affiliationIdentifier { get; set; }

    [XmlAttribute]
    public string affiliationIdentifierScheme { get; set; }

    [XmlAttribute]
    public string schemeURI { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

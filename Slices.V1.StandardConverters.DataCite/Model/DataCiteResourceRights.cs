using System.Xml.Serialization;
using System.Xml.Schema;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRights
{
    [XmlAttribute(DataType = "anyURI")]
    public string rightsURI { get; set; }

    [XmlAttribute]
    public string rightsIdentifier { get; set; }

    [XmlAttribute]
    public string rightsIdentifierScheme { get; set; }

    [XmlAttribute(DataType = "anyURI")]
    public string schemeURI { get; set; }

    [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
    public string lang { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

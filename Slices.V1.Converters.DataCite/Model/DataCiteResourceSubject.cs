using System.Xml.Serialization;
using System.Xml.Schema;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceSubject
{
    [XmlAttribute]
    public string subjectScheme { get; set; }

    [XmlAttribute(DataType = "anyURI")]
    public string schemeURI { get; set; }

    [XmlAttribute(DataType = "anyURI")]
    public string valueURI { get; set; }

    [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
    public string lang { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

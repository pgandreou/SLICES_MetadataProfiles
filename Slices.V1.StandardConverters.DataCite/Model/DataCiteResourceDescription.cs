using System.Xml.Serialization;
using System.Xml.Schema;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceDescription
{
    // TODO: wat is this?
    [XmlElement("br")]
    public DataCiteResourceDescriptionBR[] Items { get; set; }

    [XmlText]
    public string[] Text { get; set; }

    [XmlAttribute]
    public DataCiteDescriptionType descriptionType { get; set; }

    [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
    public string lang { get; set; }
}

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceDescriptionBR
{
}

#nullable restore

using System.Xml.Serialization;
using System.Xml.Schema;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceTitle
{
    [XmlAttribute]
    public DataCiteTitleType titleType { get; set; }

    [XmlIgnore]
    public bool titleTypeSpecified { get; set; }

    [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
    public string lang { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

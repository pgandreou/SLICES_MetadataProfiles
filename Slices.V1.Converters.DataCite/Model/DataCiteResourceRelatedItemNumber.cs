using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedItemNumber
{
    [XmlAttribute]
    public DataCiteNumberType numberType { get; set; }

    [XmlIgnore]
    public bool numberTypeSpecified { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

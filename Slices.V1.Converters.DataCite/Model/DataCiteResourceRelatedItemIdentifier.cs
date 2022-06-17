using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedItemIdentifier
{
    [XmlAttribute]
    public DataCiteRelatedIdentifierType relatedItemIdentifierType { get; set; }

    [XmlIgnore]
    public bool relatedItemIdentifierTypeSpecified { get; set; }

    [XmlAttribute]
    public string relatedMetadataScheme { get; set; }

    [XmlAttribute(DataType = "anyURI")]
    public string schemeURI { get; set; }

    [XmlAttribute]
    public string schemeType { get; set; }

    [XmlText]
    public string Value { get; set; }
}

#nullable restore

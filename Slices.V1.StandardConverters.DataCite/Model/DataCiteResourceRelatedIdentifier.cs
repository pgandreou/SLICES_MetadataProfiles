using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedIdentifier
{
    [XmlAttribute]
    public DataCiteResourceTypeGeneral resourceTypeGeneral { get; set; }

    [XmlIgnore]
    public bool resourceTypeGeneralSpecified { get; set; }

    [XmlAttribute]
    public DataCiteRelatedIdentifierType relatedIdentifierType { get; set; }

    [XmlAttribute]
    public DataCiteRelationType relationType { get; set; }

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

using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedItem
{
    public DataCiteResourceRelatedItemIdentifier relatedItemIdentifier { get; set; }

    [XmlArrayItem("creator", IsNullable = false)]
    public DataCiteResourceRelatedItemCreator[] creators { get; set; }

    [XmlArrayItem("title", IsNullable = false)]
    public DataCiteResourceRelatedItemTitle[] titles { get; set; }

    [XmlElement(DataType = "token")]
    public string publicationYear { get; set; }

    public string volume { get; set; }

    public string issue { get; set; }

    public DataCiteResourceRelatedItemNumber number { get; set; }

    public string firstPage { get; set; }

    public string lastPage { get; set; }

    public string publisher { get; set; }

    public string edition { get; set; }

    [XmlArrayItem("contributor", IsNullable = false)]
    public DataCiteResourceRelatedItemContributor[] contributors { get; set; }

    [XmlAttribute]
    public DataCiteResourceTypeGeneral relatedItemType { get; set; }

    [XmlAttribute]
    public DataCiteRelationType relationType { get; set; }
}

#nullable restore

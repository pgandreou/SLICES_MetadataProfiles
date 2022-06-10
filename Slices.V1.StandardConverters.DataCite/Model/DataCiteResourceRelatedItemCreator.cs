using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceRelatedItemCreator
{
    public DataCiteResourceRelatedItemCreatorName creatorName { get; set; }

    public string givenName { get; set; }

    public string familyName { get; set; }
}

#nullable restore

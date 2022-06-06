using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCitePolygon
{
    [XmlElement("polygonPoint")]
    public DataCitePoint[] polygonPoint { get; set; }

    public DataCitePoint inPolygonPoint { get; set; }
}

#nullable restore

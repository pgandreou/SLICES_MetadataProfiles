using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteGeoPolygon
{
    [XmlElement("polygonPoint")]
    public DataCiteGeoPoint[] polygonPoint { get; set; }

    public DataCiteGeoPoint inPolygonPoint { get; set; }
}

#nullable restore

using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceGeoLocation
{
    [XmlElement("geoLocationBox", typeof(DataCiteGeoBox), IsNullable = false)]
    [XmlElement("geoLocationPlace", typeof(DataCiteGeoPlace), IsNullable = false)]
    [XmlElement("geoLocationPoint", typeof(DataCiteGeoPoint), IsNullable = false)]
    [XmlElement("geoLocationPolygon", typeof(DataCiteGeoPolygon), IsNullable = false)]
    public object[] Values { get; set; }
}

#nullable restore

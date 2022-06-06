using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType("point", Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCitePoint
{
    public float pointLongitude { get; set; }

    public float pointLatitude { get; set; }
}

#nullable restore

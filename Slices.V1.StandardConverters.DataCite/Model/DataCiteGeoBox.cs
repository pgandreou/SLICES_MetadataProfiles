using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType("box", Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteGeoBox
{
    public float westBoundLongitude { get; set; }

    public float eastBoundLongitude { get; set; }

    public float southBoundLatitude { get; set; }

    public float northBoundLatitude { get; set; }
}

#nullable restore

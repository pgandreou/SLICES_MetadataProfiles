using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

[Serializable]
[XmlType("funderIdentifierType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteFunderIdentifierType
{
    ISNI,

    GRID,

    ROR,

    [XmlEnum("Crossref Funder ID")]
    CrossrefFunderID,

    Other,
}

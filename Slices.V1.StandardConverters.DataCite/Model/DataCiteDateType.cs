using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

[Serializable]
[XmlType("dateType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteDateType
{
    Accepted,

    Available,

    Collected,

    Copyrighted,

    Created,

    Issued,

    Other,

    Submitted,

    Updated,

    Valid,

    Withdrawn,
}

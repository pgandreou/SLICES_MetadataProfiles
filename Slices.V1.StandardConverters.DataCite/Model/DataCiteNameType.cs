using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

[Serializable]
[XmlType("nameType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteNameType
{
    Organizational,

    Personal,
}

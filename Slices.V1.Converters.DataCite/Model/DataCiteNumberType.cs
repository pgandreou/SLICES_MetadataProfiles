using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("numberType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteNumberType
{
    Article,

    Chapter,

    Report,

    Other,
}

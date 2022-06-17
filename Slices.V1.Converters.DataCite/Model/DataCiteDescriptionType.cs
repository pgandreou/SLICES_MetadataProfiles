using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("descriptionType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteDescriptionType
{
    Abstract,

    Methods,

    SeriesInformation,

    TableOfContents,

    TechnicalInfo,

    Other,
}

using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

[Serializable]
[XmlType("resourceType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteResourceTypeGeneral
{
    Audiovisual,

    Collection,

    DataPaper,

    Dataset,

    Event,

    Image,

    InteractiveResource,

    Model,

    PhysicalObject,

    Service,

    Software,

    Sound,

    Text,

    Workflow,

    Other,
}

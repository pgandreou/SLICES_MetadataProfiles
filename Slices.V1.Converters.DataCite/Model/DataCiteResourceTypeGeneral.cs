using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("resourceType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteResourceTypeGeneral
{
    Audiovisual,

    Book,

    BookChapter,

    Collection,

    ComputationalNotebook,

    ConferencePaper,

    ConferenceProceeding,

    DataPaper,

    Dataset,

    Dissertation,

    Event,

    Image,

    InteractiveResource,

    Journal,

    JournalArticle,

    Model,

    OutputManagementPlan,

    PeerReview,

    PhysicalObject,

    Preprint,

    Report,

    Service,

    Software,

    Sound,

    Standard,

    Text,

    Workflow,

    Other,
}
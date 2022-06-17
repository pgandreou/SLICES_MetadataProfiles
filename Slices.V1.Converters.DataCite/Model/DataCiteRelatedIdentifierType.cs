using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("relatedIdentifierType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteRelatedIdentifierType
{
    ARK,

    arXiv,

    bibcode,

    DOI,

    EAN13,

    EISSN,

    Handle,

    IGSN,

    ISBN,

    ISSN,

    ISTC,

    LISSN,

    LSID,

    PMID,

    PURL,

    UPC,

    URL,

    URN,

    w3id,
}

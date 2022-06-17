using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

[Serializable]
[XmlType("relationType", Namespace = "http://datacite.org/schema/kernel-4")]
public enum DataCiteRelationType
{
    IsCitedBy,

    Cites,

    IsSupplementTo,

    IsSupplementedBy,

    IsContinuedBy,

    Continues,

    IsNewVersionOf,

    IsPreviousVersionOf,

    IsPartOf,

    HasPart,

    IsPublishedIn,

    IsReferencedBy,

    References,

    IsDocumentedBy,

    Documents,

    IsCompiledBy,

    Compiles,

    IsVariantFormOf,

    IsOriginalFormOf,

    IsIdenticalTo,

    HasMetadata,

    IsMetadataFor,

    Reviews,

    IsReviewedBy,

    IsDerivedFrom,

    IsSourceOf,

    Describes,

    IsDescribedBy,

    HasVersion,

    IsVersionOf,

    Requires,

    IsRequiredBy,

    Obsoletes,

    IsObsoletedBy,
}

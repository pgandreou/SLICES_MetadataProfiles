using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
[XmlRoot("resource", Namespace = "http://datacite.org/schema/kernel-4", IsNullable = false)]
public partial class DataCiteResource
{
    public DataCiteResourceIdentifier identifier { get; set; }

    [XmlArrayItem("creator", IsNullable = false)]
    public DataCiteCreator[] creators { get; set; }

    [XmlArrayItem("title", IsNullable = false)]
    public DataCiteResourceTitle[] titles { get; set; }

    public DataCiteResourcePublisher publisher { get; set; }

    [XmlElement(DataType = "token")]
    public string publicationYear { get; set; }

    public DataCiteResourceType resourceType { get; set; }

    [XmlArrayItem("subject", IsNullable = false)]
    public DataCiteResourceSubject[] subjects { get; set; }

    [XmlArrayItem("contributor", IsNullable = false)]
    public DataCiteResourceContributor[] contributors { get; set; }

    [XmlArrayItem("date", IsNullable = false)]
    public DataCiteResourceDate[] dates { get; set; }

    [XmlElement(DataType = "language")]
    public string language { get; set; }

    [XmlArrayItem("alternateIdentifier", IsNullable = false)]
    public DataCiteResourceAlternateIdentifier[] alternateIdentifiers { get; set; }

    [XmlArrayItem("relatedIdentifier", IsNullable = false)]
    public DataCiteResourceRelatedIdentifier[] relatedIdentifiers { get; set; }

    [XmlArrayItem("size", IsNullable = false)]
    public string[] sizes { get; set; }

    [XmlArrayItem("format", IsNullable = false)]
    public string[] formats { get; set; }

    public string version { get; set; }

    [XmlArrayItem("rights", IsNullable = false)]
    public DataCiteResourceRights[] rightsList { get; set; }

    [XmlArrayItem("description", IsNullable = false)]
    public DataCiteResourceDescription[] descriptions { get; set; }

    // TODO
    [XmlArrayItem("geoLocation", IsNullable = false)]
    [XmlArrayItem("geoLocationBox", typeof(DataCiteBox), IsNullable = false, NestingLevel = 1)]
    [XmlArrayItem("geoLocationPlace", typeof(DataCitePlace), IsNullable = false, NestingLevel = 1)]
    [XmlArrayItem("geoLocationPoint", typeof(DataCitePoint), IsNullable = false, NestingLevel = 1)]
    [XmlArrayItem("geoLocationPolygon", typeof(DataCitePolygon), IsNullable = false, NestingLevel = 1)]
    public object[][] geoLocations { get; set; }

    [XmlArrayItem("fundingReference", IsNullable = false)]
    public DataCiteResourceFundingReference[] fundingReferences { get; set; }
}

#nullable restore

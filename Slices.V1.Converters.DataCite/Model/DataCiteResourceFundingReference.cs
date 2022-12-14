using System.Xml.Serialization;

namespace Slices.V1.Converters.DataCite.Model;

#nullable disable

[Serializable]
[XmlType(AnonymousType = true, Namespace = "http://datacite.org/schema/kernel-4")]
public partial class DataCiteResourceFundingReference
{
    public string funderName { get; set; }

    public DataCiteResourceFunderIdentifier funderIdentifier { get; set; }

    public DataCiteResourceFundingAwardNumber awardNumber { get; set; }

    public string awardTitle { get; set; }
}

#nullable restore

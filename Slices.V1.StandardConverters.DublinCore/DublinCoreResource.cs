using System.Xml.Schema;
using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.DublinCore;

internal static class DublinCoreXmlNamespaces
{
    public const string Elements = "http://purl.org/dc/elements/1.1/";
    public const string OpenArchives = "http://www.openarchives.org/OAI/2.0/oai_dc/";

    public static readonly XmlSerializerNamespaces SerializerNamespaces;

    static DublinCoreXmlNamespaces()
    {
        SerializerNamespaces = new XmlSerializerNamespaces();
        SerializerNamespaces.Add("dc", Elements);
        SerializerNamespaces.Add("oai_dc", OpenArchives);
    }
};

#nullable disable

[Serializable]
[XmlType("elementType", Namespace = DublinCoreXmlNamespaces.Elements)]
public partial class DublinCoreElement
{
    [XmlAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
    public string Language { get; set; }

    [XmlText]
    public string Value { get; set; }

    public DublinCoreElement()
    {
    }

    public DublinCoreElement(string value)
    {
        Value = value;
    }
}

[Serializable]
[XmlType("oai_dcType", Namespace = DublinCoreXmlNamespaces.OpenArchives)]
[XmlRoot("dc", Namespace = DublinCoreXmlNamespaces.OpenArchives, IsNullable = false)]
public partial class DublinCoreResource
{
    [XmlNamespaceDeclarations]
    public XmlSerializerNamespaces XmlNamespaces { get; set; } = new(DublinCoreXmlNamespaces.SerializerNamespaces);

    [XmlElement("title", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Titles { get; set; }

    [XmlElement("creator", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Creators { get; set; }

    [XmlElement("subject", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Subjects { get; set; }

    [XmlElement("description", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Descriptions { get; set; }

    [XmlElement("publisher", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Publishers { get; set; }

    [XmlElement("contributor", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Contributors { get; set; }

    [XmlElement("date", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Dates { get; set; }

    [XmlElement("type", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Types { get; set; }

    [XmlElement("format", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Formats { get; set; }

    [XmlElement("identifier", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Identifiers { get; set; }

    [XmlElement("source", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Sources { get; set; }

    [XmlElement("language", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Languages { get; set; }

    [XmlElement("relation", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Relations { get; set; }

    [XmlElement("coverage", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Coverages { get; set; }

    [XmlElement("rights", Namespace = DublinCoreXmlNamespaces.Elements)]
    public DublinCoreElement[] Rights { get; set; }
}

#nullable restore


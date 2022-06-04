namespace Slices.V1.StandardConverters.DublinCore;

#nullable disable

// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.openarchives.org/OAI/2.0/oai_dc/")]
[System.Xml.Serialization.XmlRootAttribute("dc", Namespace = "http://www.openarchives.org/OAI/2.0/oai_dc/", IsNullable = false)]
public partial class DublinCoreObject
{
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("creator", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string[] creator { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/", DataType = "date")]
    public System.DateTime date { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/")]
    public string description { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("identifier", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string[] identifier { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("relation", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string[] relation { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/")]
    public string rights { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/")]
    public string title { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("type", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string[] type { get; set; }
}

#nullable restore


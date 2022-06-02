using System.Xml;
using System.Xml.Serialization;

namespace Slices.V1.ExternalConverters.DublinCore;

internal class DublinCoreSerializer
{
    private XmlSerializer xmlSerializer = new(typeof(DublinCoreObject));

    public DublinCoreObject FromXml(string serialized)
    {
        return FromXml(new StringReader(serialized));
    }

    public DublinCoreObject FromXml(TextReader reader)
    {
        return (DublinCoreObject)xmlSerializer.Deserialize(reader);
    }

    public string ToXml(DublinCoreObject record)
    {
        StringWriter writer = new();
        ToXml(record, writer);

        return writer.ToString();
    }

    public void ToXml(DublinCoreObject record, TextWriter writer)
    {
        XmlWriter xmlWriter = XmlWriter.Create(writer, new()
        {
            Indent = true,
        });

        xmlSerializer.Serialize(xmlWriter, record);
    }
}
using System.Xml;
using System.Xml.Serialization;

namespace Slices.V1.StandardConverters.Common;

public interface IStandardXmlSerializer<TModel>
    where TModel : class
{
    TModel FromXml(TextReader reader);
    void ToXml(TModel record, TextWriter writer);
}

public abstract class StandardXmlSerializerBase<TModel> : IStandardXmlSerializer<TModel>
    where TModel : class
{
    protected XmlSerializer xmlSerializer = new(typeof(TModel));

    public virtual TModel FromXml(TextReader reader)
    {
        TModel? deserialized = (TModel?)xmlSerializer.Deserialize(reader);

        if (deserialized == null)
        {
            throw new Exception("xmlSerializer.Deserialize returned null - this should not happen");
        }

        return deserialized;
    }

    public virtual void ToXml(TModel record, TextWriter writer)
    {
        XmlWriter xmlWriter = XmlWriter.Create(writer, new()
        {
            Indent = true,
        });

        xmlSerializer.Serialize(xmlWriter, record);
    }
}

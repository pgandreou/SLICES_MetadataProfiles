using System.Xml;
using System.Xml.Serialization;

namespace Slices.V1.Converters.Common;

public interface IStandardXmlSerializer<TModel>
    where TModel : class
{
    Task<TModel> FromXmlAsync(Stream stream);
    Task ToXmlAsync(TModel record, Stream stream);
}

/// <remarks>
/// <see cref="System.Xml.Serialization.XmlSerializer"/> does not support async APIs
/// so the default implementation offloads the calls to a separate "thread"
/// </remarks>
public abstract class StandardXmlSerializerBase<TModel> : IStandardXmlSerializer<TModel>
    where TModel : class
{
    protected readonly XmlSerializer XmlSerializer = new(typeof(TModel));

    public virtual Task<TModel> FromXmlAsync(Stream stream)
    {
        return Task.Run(() =>
        {
            TModel? deserialized = (TModel?)XmlSerializer.Deserialize(stream);

            if (deserialized == null)
            {
                throw new Exception("xmlSerializer.Deserialize returned null - this should not happen");
            }

            return deserialized;
        });
    }

    public virtual Task ToXmlAsync(TModel record, Stream stream)
    {
        return Task.Run(() =>
        {
            XmlWriter xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
            {
                Indent = true,
            });

            XmlSerializer.Serialize(xmlWriter, record);
        });
    }
}
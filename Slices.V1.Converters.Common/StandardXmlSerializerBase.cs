using System.Xml;
using System.Xml.Serialization;
using Slices.V1.Converters.Common.Exceptions;

namespace Slices.V1.Converters.Common;

public interface IStandardXmlSerializer<TModel>
    where TModel : class
{
    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <exception cref="StandardSerializationException">
    /// Thrown if the deserialization has failed. 
    /// </exception>
    /// <returns></returns>
    Task<TModel> FromXmlAsync(Stream stream);
    
    /// <summary>
    /// </summary>
    /// <param name="record"></param>
    /// <param name="stream"></param>
    /// <exception cref="StandardSerializationException">
    /// Thrown if the serialization has failed. 
    /// </exception>
    /// <returns></returns>
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
            TModel? deserialized;
            try
            {
                deserialized = (TModel?)XmlSerializer.Deserialize(stream);
            }
            catch (Exception e)
            {
                throw new StandardSerializationException("XmlSerializer.Deserialize threw an exception", e);
            }

            if (deserialized == null)
            {
                throw new StandardSerializationException("xmlSerializer.Deserialize returned null");
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

            try
            {
                XmlSerializer.Serialize(xmlWriter, record);
            }
            catch (InvalidOperationException e)
            {
                throw new StandardSerializationException("XmlSerializer.Serialize threw an exception", e);
            }
        });
    }
}
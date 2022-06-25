using System.Text.Json;
using Slices.V1.Converters.Common.Exceptions;

namespace Slices.V1.Converters.Common;

public interface IStandardJsonSerializer<TModel>
{
    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <exception cref="StandardSerializationException">
    /// Thrown if the deserialization has failed. 
    /// </exception>
    /// <returns></returns>
    Task<TModel> FromJsonAsync(Stream stream);
    
    /// <summary>
    /// </summary>
    /// <param name="record"></param>
    /// <param name="stream"></param>
    /// <exception cref="StandardSerializationException">
    /// Thrown if the serialization has failed. 
    /// </exception>
    /// <returns></returns>
    Task ToJsonAsync(TModel record, Stream stream);
}

public abstract class StandardJsonSerializerBase<TModel> : IStandardJsonSerializer<TModel>
{
    public virtual async Task<TModel> FromJsonAsync(Stream stream)
    {
        TModel? model;
        try
        {
            model = await JsonSerializer.DeserializeAsync<TModel>(stream);
        }
        catch (Exception e) when(e is JsonException or NotSupportedException)
        {
            throw new StandardSerializationException("JsonSerializer.DeserializeAsync threw an exception", e);
        }

        if (model == null)
        {
            throw new StandardSerializationException("JsonSerializer.DeserializeAsync returned null");
        }
        
        return model;
    }

    public virtual async Task ToJsonAsync(TModel record, Stream stream)
    {
        try
        {
            await JsonSerializer.SerializeAsync(stream, record, new JsonSerializerOptions
            {
                WriteIndented = true,
                // PropertyNamingPolicy = JsonNamingPolicy.
            });
        }
        catch (NotSupportedException e)
        {
            throw new StandardSerializationException("JsonSerializer.SerializeAsync threw an exception", e);
        }
    }
}
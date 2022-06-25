using System.Text.Json;

namespace Slices.V1.Converters.Common;

public interface IStandardJsonSerializer<TModel>
{
    Task<TModel> FromJsonAsync(Stream stream);
    Task ToJsonAsync(TModel record, Stream stream);
}

public abstract class StandardJsonSerializerBase<TModel> : IStandardJsonSerializer<TModel>
{
    public virtual async Task<TModel> FromJsonAsync(Stream stream)
    {
        TModel? model = await JsonSerializer.DeserializeAsync<TModel>(stream);

        if (model == null)
        {
            throw new Exception("JsonSerializer.Deserialize returned null");
        }
        
        return model;
    }

    public virtual async Task ToJsonAsync(TModel record, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, record, new JsonSerializerOptions
        {
            WriteIndented = true,
            // PropertyNamingPolicy = JsonNamingPolicy.
        });
    }
}
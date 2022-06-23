using System.Text.Json;

namespace Slices.V1.Converters.Common;

public interface IStandardJsonSerializer<TModel>
{
    TModel FromJson(TextReader reader);
    void ToJson(TModel record, TextWriter writer);
}

// STJ doesn't support TextReader/Writer :(
public abstract class StandardJsonSerializerBase<TModel> : IStandardJsonSerializer<TModel>
{
    public virtual TModel FromJson(TextReader reader)
    {
        TModel? model = JsonSerializer.Deserialize<TModel>(reader.ReadToEnd());

        if (model == null)
        {
            throw new Exception("JsonSerializer.Deserialize returned null");
        }
        
        return model;
    }

    public virtual void ToJson(TModel record, TextWriter writer)
    {
        string json = JsonSerializer.Serialize(record, new JsonSerializerOptions
        {
            WriteIndented = true,
            // PropertyNamingPolicy = JsonNamingPolicy.
        });
        
        writer.Write(json);
    }
}
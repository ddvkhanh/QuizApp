using System.Text.Json;
using System.Text.Json.Serialization;
using QuizApp.Database.Models;

namespace QuizApp.Server.Converters
{
    public class PolymorphicJsonConverter : JsonConverter<Question>
    {
        public override Question Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var questionType = root.GetProperty("questionType").GetString();

                return questionType switch
                {
                    "single" => JsonSerializer.Deserialize<SingleChoiceQuestion>(root.GetRawText(), options),
                    "multiple" => JsonSerializer.Deserialize<MultipleChoiceQuestion>(root.GetRawText(), options),
                    _ => throw new JsonException($"Unknown question type: {questionType}")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, Question value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}

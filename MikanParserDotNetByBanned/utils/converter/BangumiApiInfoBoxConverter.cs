using MikanParserDotNetByBanned.models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MikanParserDotNetByBanned.utils.converter
{
    internal class BangumiApiInfoBoxConverter : JsonConverter<InfoBox>
    {
        public override InfoBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var infobox = new InfoBox();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString()!;
                    reader.Read();
                    switch (propertyName)
                    {
                        case "key":
                            infobox.Key = reader.GetString();
                            break;
                        case "value":
                            infobox.Value = reader.TokenType switch
                            {
                                JsonTokenType.StartArray =>
                                    JsonSerializer.Deserialize<List<Alias>>(ref reader, options),
                                JsonTokenType.String => reader.GetString(),
                                JsonTokenType.Number => reader.GetInt32(),
                                JsonTokenType.StartObject =>
                                    JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options),
                                _ => infobox.Value
                            };

                            break;
                    }
                }
            }

            return infobox;
        }

        public override void Write(Utf8JsonWriter writer, InfoBox value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("key", value.Key);
            writer.WritePropertyName("value");

            switch (value.Value)
            {
                case List<Alias> aliases:
                    JsonSerializer.Serialize(writer, aliases, options);
                    break;
                case string stringValue:
                    writer.WriteStringValue(stringValue);
                    break;
                case int intValue:
                    writer.WriteNumberValue(intValue);
                    break;
                case Dictionary<string, object> dictValue:
                    JsonSerializer.Serialize(writer, dictValue, options);
                    break;
                default:
                    writer.WriteNullValue();
                    break;
            }

            writer.WriteEndObject();
        }
    }
}
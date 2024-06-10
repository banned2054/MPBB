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
                            if (reader.TokenType == JsonTokenType.StartArray)
                            {
                                infobox.Value = JsonSerializer.Deserialize<List<Alias>>(ref reader, options);
                            }
                            else
                            {
                                infobox.Value = reader.GetString();
                            }

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
            if (value.Value is List<Alias> aliases)
            {
                JsonSerializer.Serialize(writer, aliases, options);
            }
            else
            {
                writer.WriteStringValue(value.Value!.ToString());
            }

            writer.WriteEndObject();
        }
    }
}
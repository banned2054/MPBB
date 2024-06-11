using System.Text.Json.Serialization;

namespace MikanParserDotNetByBanned.models
{
    internal class BangumiApiSubjectJson
    {
        [JsonPropertyName("pub_date")] public string ? Date { get; set; }

        [JsonPropertyName("platform")] public string ? Platform { get; set; }

        [JsonPropertyName("images")] public Images ? Images { get; set; }

        [JsonPropertyName("summary")] public string ? Summary { get; set; }

        [JsonPropertyName("name")] public string ? Name { get; set; }

        [JsonPropertyName("name_cn")] public string ? NameCn { get; set; }

        [JsonPropertyName("tags")] public List<Tag> ? Tags { get; set; }

        [JsonPropertyName("infobox")] public List<InfoBox> ? InfoBox { get; set; }

        [JsonPropertyName("rating")] public Rating ? Rating { get; set; }

        [JsonPropertyName("total_episodes")] public int TotalEpisodes { get; set; }

        [JsonPropertyName("subject_id")] public int Id { get; set; }

        [JsonPropertyName("eps")] public int Eps { get; set; }

        [JsonPropertyName("type")] public int Type { get; set; }
    }

    internal class Images
    {
        [JsonPropertyName("small")] public string ? Small { get; set; }

        [JsonPropertyName("grid")] public string ? Grid { get; set; }

        [JsonPropertyName("large")] public string ? Large { get; set; }

        [JsonPropertyName("medium")] public string ? Medium { get; set; }

        [JsonPropertyName("common")] public string ? Common { get; set; }
    }

    internal class Tag
    {
        [JsonPropertyName("name")] public string ? Name { get; set; }

        [JsonPropertyName("count")] public int Count { get; set; }
    }

    internal class InfoBox
    {
        [JsonPropertyName("key")] public string ? Key { get; set; }

        [JsonPropertyName("value")] public object ? Value { get; set; }
    }

    internal class Rating
    {
        [JsonPropertyName("rank")] public int Rank { get; set; }

        [JsonPropertyName("total")] public int Total { get; set; }

        [JsonPropertyName("count")] public Dictionary<int, int> ? Count { get; set; }

        [JsonPropertyName("score")] public double Score { get; set; }
    }

    public class Alias
    {
        [JsonPropertyName("v")] public string? V { get; set; }
    }
}
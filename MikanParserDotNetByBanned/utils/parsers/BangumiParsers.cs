using System.Globalization;
using System.Text.Json;
using MikanParserDotNetByBanned.models;
using MikanParserDotNetByBanned.utils.converter;

namespace MikanParserDotNetByBanned.utils.parsers
{
    internal class BangumiParsers
    {
        private static readonly string[] Formats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy" };

        public static BangumiInfo GetBangumiInfoFromJson(string jsonText)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters                  = { new BangumiApiInfoBoxConverter() }
            };

            var apiJson = JsonSerializer.Deserialize<BangumiApiSubjectJson>(jsonText, options);
            var result = new BangumiInfo()
            {
                CnName       = apiJson!.NameCn!,
                OriginName = apiJson.Name!,
                SubjectId           = apiJson.Id,
                Date         = DateTime.ParseExact(apiJson.Date!, Formats, CultureInfo.InvariantCulture)
            };


            return result;
        }
    }
}
using System.Text.RegularExpressions;

namespace MikanParserDotNetByBanned.utils.parsers
{
    internal class TitleParsers
    {
        private const string BracketSplit = @"[\[\]【】]";

        public static (bool, string) GetTitle(string originText)
        {
            originText = StringUtils.DefaultTitleReplace(originText);
            originText = StringUtils.ReplaceUnnecessaryStr(originText);
            Console.WriteLine(originText);
            foreach (var nowRegexRule in AppConfig.TitleParserRegexList)
            {
                var matchObj = Regex.Match(originText, nowRegexRule, RegexOptions.IgnoreCase);
                if (!matchObj.Success || string.IsNullOrEmpty(matchObj.Groups[1].Value))
                {
                    continue;
                }

                originText = StringUtils.DefaultTitleReplace(matchObj.Groups[1].Value);
                originText = StringUtils.ReplaceUnnecessaryStr(originText);

                var splitString = Regex.Split(originText, BracketSplit);

                var nonEmptyStringList = splitString.Where(s => !string.IsNullOrEmpty(s)) // 过滤掉空字符串
                                                    .Select(s => s.Trim())                // 对每个字符串进行Trim
                                                    .Where(s => !string.IsNullOrEmpty(s)) // 再次过滤掉可能的空字符串
                                                    .ToList();

                if (nonEmptyStringList.Count <= 1)
                {
                    originText = nonEmptyStringList[0];
                }
                else
                {
                    originText = nonEmptyStringList[1];
                }

                return (true, originText);
            }

            return (false, originText);
        }

        public static (int, int) GetEpisodeRange(string input)
        {
            foreach (var pattern in AppConfig.TitleParserRegexList)
            {
                var match = Regex.Match(input, pattern);
                if (!match.Success) continue;
                var episodeString = match.Groups[2].Value;
                if (episodeString.Contains("-"))
                {
                    var parts = episodeString.Split('-');
                    if (int.TryParse(parts[0], out var startEpisode) && int.TryParse(parts[1], out var endEpisode))
                    {
                        return (startEpisode, endEpisode);
                    }
                }
                else if (int.TryParse(episodeString, out var singleEpisode))
                {
                    return (singleEpisode, -1);
                }
            }

            return (-1, -1); // 如果没有匹配到，返回(-1, -1)
        }
    }
}
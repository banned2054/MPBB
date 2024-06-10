using System.Text.RegularExpressions;

namespace MikanParserDotNetByBanned.utils.parsers
{
    internal class TitleParsers
    {
        private static readonly string BracketSplit = @"[\[\]()【】（）]";

        public static (bool, string) GetTitle(string originText)
        {
            var nowString = originText;
            nowString = StringUtils.DefaultTitleReplace(nowString);
            nowString = StringUtils.ReplaceUnnecessaryStr(nowString);
            var splitString = Regex.Split(nowString, BracketSplit);
        }
    }
}
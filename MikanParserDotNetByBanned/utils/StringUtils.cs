using System.Text.RegularExpressions;

namespace MikanParserDotNetByBanned.utils
{
    internal class StringUtils
    {
        public static string ReplaceUnnecessaryStr(string origin)
        {
            var result = origin;
            foreach (var unnecessaryStr in StaticConfig.UnnecessaryStringList)
            {
                result = Regex.Replace(result, unnecessaryStr, "");
            }

            return result;
        }

        public static string DefaultTitleReplace(string origin)
        {
            var result = origin;
            foreach (var kvp in StaticConfig.DefaultTitleReplaceDictionary)
            {
                result = result.Replace(kvp.Key, kvp.Value);
            }

            return result;
        }
    }
}
namespace MikanParserDotNetByBanned
{
    internal static class StaticConfig
    {
        public static readonly List<string>               TitleParserRegexList;
        public static readonly List<string>               UnnecessaryStringList;
        public static readonly Dictionary<string, string> DefaultTitleReplaceDictionary;
        public static readonly Dictionary<string, string> BangumiApiHeader;
        public static readonly Dictionary<string, string> BangumiApiHeaderWithoutAuthorization;


        static StaticConfig()
        {
            TitleParserRegexList = new List<string>
            {
                @"(.*)\[(?:第)?(\d+|\d+\.\d+(?:-\d+|\d+\.\d+)?)[话集話](v\d{1,2})?(?:fin)?\](.*)",
                @"(.*)第?(\d+|\d+\.\d+(?:-\d+|\d+\.\d+)?)[话話集](v\d{1,2})?(?:fin)?(.*)",
                @"(.*)(?:s\d{2})?ep?(\d+(?:-\d+)?)(v\d{1,2})?(?: )?(?:fin)?(.*)",
                @"(.*)[\[\ e](\d{1,4}(?:-\d{1,4})?|\d{1,4}\.\d{1,2}(?:-\d{1,4}\.\d{1,2})?)(v\d{1,2})?(?: )?(?:fin)?[\]\ ](.*)",
                @"(.*) - (\d{1,4}(?:-\d{1,4})?(?!\d|p)|\d{1,4}\.\d{1,2}(?:-\d{1,4}\.\d{1,2})?(?!\d|p))(v\d{1,2})?(?: )?(?:fin)?(.*)",
            };
            UnnecessaryStringList = new List<string>
            {
                @"★\d{2}月新番★",
                @"★\d{2}月新番",
                @"★剧场版★",
                @"\[\d{2}月新番\]",
                @"\[\d{1}月新番\]",
                @"\[国漫\]",
                @"\[个人制作合集\]",
                @"\[[^\[\]]*招募[^\[\]]*\]",
                @"jibaketa合成&amp;二次压制",
                @"amp;",
                @"\[BYRBT\]\."
            };
            DefaultTitleReplaceDictionary = new Dictionary<string, string>
            {
                { "【", "[" },
                { "】", "]" },
                { "1920X1080", "1080p" },
                { "1920x1080", "1080p" },
                { "1280X720", "720p" },
                { "1280x720", "720p" },
                { "[1080p@60fps]", "[1080p][60fps]" },
                { "[720@60fps]", "[720p][60fps]" }
            };
            BangumiApiHeader = new Dictionary<string, string>
            {
                { "User-Agent", "banned/MPBB (https://github.com/banned2054/MPBB)" },
                { "Authorization", "Bearer L3vSstjEQ4xeUXSPRLSFK7vQ3EzFkhkslLquYfev" },
                { "Cookie", "chii_sec_id=LjHxKm1moAnfYrO5oxPYAf0sqkm5aBAa1FeJaqk" },
            };
            BangumiApiHeaderWithoutAuthorization = new Dictionary<string, string>
            {
                { "User-Agent", "banned/MPBB (https://github.com/banned2054/MPBB)" },
                { "Cookie", "chii_sec_id=LjHxKm1moAnfYrO5oxPYAf0sqkm5aBAa1FeJaqk" },
            };
        }
    }
}
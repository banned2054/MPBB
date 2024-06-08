﻿namespace MikanParserDotNetByBanned
{
    internal static class AppConfig
    {
        public static readonly List<string>               UnnecessaryStringList;
        public static readonly Dictionary<string, string> DefaultTitleReplaceDictionary;
        public static readonly Dictionary<string, string> BangumiApiHeader;
        public static readonly Dictionary<string, string> BangumiApiHeaderWithoutAuthorization;

        static AppConfig()
        {
            UnnecessaryStringList = new List<string>
            {
                @"★\d{2}月新番★",
                @"★\d{2}月新番",
                @"★剧场版★",
                @"\[\d{2}月新番\]",
                @"\[\d{1}月新番\]",
                @"\[国漫\]",
                @"\[个人制作合集\]",
                @"\[[^\[\]]*招募[^\[\]]*\]"
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
using AngleSharp.Html.Parser;
using AngleSharp.Xml.Parser;
using MikanParserDotNetByBanned.models;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace MikanParserDotNetByBanned.utils.parsers
{
    internal class RssMikanParsers
    {
        private const string Pattern = @"bangumiId=(\d+)&subgroupid=(\d+)";

        public static List<RssItemInfo> GetMikanIemInfoFromRssXml(string xmlText)
        {
            var rssItemInfoList = new List<RssItemInfo>();

            var parser   = new XmlParser();
            var document = parser.ParseDocument(xmlText);
            var itemList = document.QuerySelectorAll("item");
            foreach (var item in itemList)
            {
                var newRssItemInfo = GetItemInfoFromOneIElement(item);
                rssItemInfoList.Add(newRssItemInfo);
            }

            return rssItemInfoList;
        }

        private static RssItemInfo GetItemInfoFromOneIElement(IParentNode item)
        {
            var titleItem     = item.QuerySelector("title");
            var linkItem      = item.QuerySelector("link");
            var torrentItem   = item.QuerySelector("torrent");
            var sizeItem      = torrentItem!.QuerySelector("contentLength");
            var pubDateItem   = torrentItem!.QuerySelector("pubDate");
            var enclosureItem = item.QuerySelector("enclosure");

            var title       = titleItem!.TextContent;
            var link        = linkItem!.TextContent;
            var sizeStr     = sizeItem!.TextContent;
            var pubDateStr  = pubDateItem!.TextContent;
            var torrentLink = enclosureItem!.GetAttribute("url")!;
            var size        = long.Parse(sizeStr);
            var pubDate     = CountUtils.ConvertStringToDatetime(pubDateStr);

            title   = StringUtils.DefaultTitleReplace(title);
            title   = StringUtils.ReplaceUnnecessaryStr(title);
            sizeStr = CountUtils.ConvertByteLongToByte(size);

            var newRssItemInfo = new RssItemInfo()
            {
                Title       = title,
                Link        = link,
                SizeStr     = sizeStr,
                PubDate     = pubDate,
                BytesSize   = size,
                TorrentLink = torrentLink
            };
            return newRssItemInfo;
        }

        public static (bool, string) GetHomeUrlFromEpisodePageHtml(string htmlText)
        {
            var parser    = new HtmlParser();
            var document  = parser.ParseDocument(htmlText);
            var urlItem   = document.QuerySelectorAll("a").First(m => m.ClassName == "mikan-rss");
            var originUrl = urlItem.GetAttribute("href")!;

            var regex = new Regex(Pattern);
            var match = regex.Match(originUrl);

            if (!match.Success) return (false, "");
            var bangumiId  = int.Parse(match.Groups[1].Value);
            var subgroupId = int.Parse(match.Groups[2].Value);

            var result = $"https://mikanani.me/Home/Bangumi/{bangumiId}#{subgroupId}";
            return (true, result);
        }

        public static (bool, string) GetBangumiUrlFromHomePageHtml(string htmlText)
        {
            var parser   = new HtmlParser();
            var document = parser.ParseDocument(htmlText);
            var leftBarIElement = document.QuerySelectorAll("div")
                                          .First(m => m.ClassName == "pull-left leftbar-container");
            var aIElementList = leftBarIElement.QuerySelectorAll("a");
            foreach (var aIElement in aIElementList)
            {
                var nowText = aIElement.TextContent;
                if (nowText.Contains("https://bgm.tv/subject/"))
                {
                    return (true, nowText);
                }
            }

            return (false, "");
        }
    }
}
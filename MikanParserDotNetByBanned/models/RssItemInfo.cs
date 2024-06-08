namespace MikanParserDotNetByBanned.models
{
    internal class RssItemInfo
    {
        public string   Title       { get; set; } = string.Empty;
        public string   Link        { get; set; } = string.Empty;
        public string   SizeStr     { get; set; } = string.Empty;
        public string   TorrentLink { get; set; } = string.Empty;
        public long     BytesSize   { get; set; }
        public DateTime PubDate     { get; set; }
    }
}
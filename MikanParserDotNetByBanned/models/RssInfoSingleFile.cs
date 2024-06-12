namespace MikanParserDotNetByBanned.models
{
    internal class RssInfoSingleFile
    {
        public int                 SubjectId      { get; set; }
        public string              OriginTitle    { get; set; } = string.Empty;
        public string              AnalysisTitle  { get; set; } = string.Empty;
        public string              OriginFileName { get; set; } = string.Empty;
        public DateTime            PubDate        { get; set; }
        public float               Episode        { get; set; }
        public int                 Version        { get; set; }
        public int                 Resolution     { get; set; }
        public int                 FrameRate      { get; set; }
        public string              MikanHomeUrl   { get; set; } = string.Empty;
        public TorrentDownloadEnum DownloadStatus { get; set; }
    }
}
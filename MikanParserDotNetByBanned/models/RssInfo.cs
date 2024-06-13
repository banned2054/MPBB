namespace MikanParserDotNetByBanned.models
{
    internal class RssInfo
    {
        public int                 SubjectId          { get; set; }
        public string              OriginTitle        { get; set; } = string.Empty;
        public string              AnalysisAnimeTitle { get; set; } = string.Empty;
        public DateTime            PubDate            { get; set; }
        public float               FirstEpisode       { get; set; }
        public int                 EndEpisode         { get; set; }
        public int                 Version            { get; set; }
        public int                 Resolution         { get; set; }
        public int                 FrameRate          { get; set; }
        public string              MikanHomeUrl       { get; set; } = string.Empty;
        public TorrentDownloadEnum DownloadStatus     { get; set; }
    }
}
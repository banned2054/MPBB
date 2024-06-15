using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikanParserDotNetByBanned.models
{
    internal class RssInfo
    {
        [Key, Column("url")]             public string              Url                { get; set; } = string.Empty;
        [Column("subject_id")]           public int                 SubjectId          { get; set; }
        [Column("origin_title")]         public string              OriginTitle        { get; set; } = string.Empty;
        [Column("analysis_anime_title")] public string              AnalysisAnimeTitle { get; set; } = string.Empty;
        [Column("pubdate")]              public DateTime            PubDate            { get; set; }
        [Column("first_episode")]        public float               FirstEpisode       { get; set; }
        [Column("end_episode")]          public int                 EndEpisode         { get; set; }
        [Column("version")]              public int                 Version            { get; set; }
        [Column("resolution")]           public int                 Resolution         { get; set; }
        [Column("frame_rate")]           public int                 FrameRate          { get; set; }
        [Column("mikan_home_url")]       public string              MikanHomeUrl       { get; set; } = string.Empty;
        [Column("download_status")]      public TorrentDownloadEnum DownloadStatus     { get; set; }
    }
}
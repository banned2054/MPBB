using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MikanParserDotNetByBanned.models
{
    internal class BangumiInfo
    {
        [Key, Column("subject_id")] public int      SubjectId     { get; set; }
        [Column("origin_name")]     public string   OriginName    { get; set; } = string.Empty;
        [Column("cn_name")]         public string   CnName        { get; set; } = string.Empty;
        [Column("pubdate")]         public DateTime Pubdate       { get; set; }
        [Column("platform")]        public string   Platform      { get; set; } = string.Empty;
        [Column("summary")]         public string   Summary       { get; set; } = string.Empty;
        [Column("rating_score")]    public double   RatingScore   { get; set; }
        [Column("total_episodes")]   public int      TotalEpisodes { get; set; }
        [Column("episode")]         public int      Episode       { get; set; }

        [Column("small_image_url")] public string SmallImageUrl { get; set; } = string.Empty;
        [Column("image_url")]       public string ImageUrl      { get; set; } = string.Empty;
    }
}
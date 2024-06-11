namespace MikanParserDotNetByBanned.models
{
    internal class BangumiInfo
    {
        public int      SubjectId     { get; set; }
        public string   OriginName    { get; set; } = string.Empty;
        public string   CnName        { get; set; } = string.Empty;
        public DateTime Date          { get; set; }
        public string   Platform      { get; set; } = string.Empty;
        public string   Summary       { get; set; } = string.Empty;
        public double   RatingScore   { get; set; }
        public int      TotalEpisodes { get; set; }
        public int      Eps           { get; set; }

        public string SmallImageUrl { get; set; } = string.Empty;
        public string ImageUrl      { get; set; } = string.Empty;
    }
}
namespace MikanParserDotNetByBanned.models.sql
{
    internal class RssInfoSqlManager
    {
        private static readonly RssContext Context;

        static RssInfoSqlManager()
        {
            Context = new RssContext();
            Context.Database.EnsureCreated();
        }

        public static void AddRssInfo(RssInfo rssInfo)
        {
            Context.RssContexts.Add(rssInfo);
            Context.SaveChanges();
        }

        public static RssInfo ? GetRssInfo(string url)
        {
            return Context.RssContexts.FirstOrDefault(r => r.Url == url);
        }

        public static void UpdateRssInfo(string url, RssInfo updatedRssInfo)
        {
            var rssInfo = Context.RssContexts.FirstOrDefault(r => r.Url == url);
            if (rssInfo == null) return;

            rssInfo.SubjectId          = updatedRssInfo.SubjectId;
            rssInfo.OriginTitle        = updatedRssInfo.OriginTitle;
            rssInfo.AnalysisAnimeTitle = updatedRssInfo.AnalysisAnimeTitle;
            rssInfo.PubDate            = updatedRssInfo.PubDate;
            rssInfo.FirstEpisode       = updatedRssInfo.FirstEpisode;
            rssInfo.EndEpisode         = updatedRssInfo.EndEpisode;
            rssInfo.Version            = updatedRssInfo.Version;
            rssInfo.Resolution         = updatedRssInfo.Resolution;
            rssInfo.FrameRate          = updatedRssInfo.FrameRate;
            rssInfo.MikanHomeUrl       = updatedRssInfo.MikanHomeUrl;
            rssInfo.DownloadStatus     = updatedRssInfo.DownloadStatus;

            Context.SaveChanges();
        }

        public static void DeleteRssInfo(int subjectId)
        {
            var rssInfo = Context.RssContexts.FirstOrDefault(r => r.SubjectId == subjectId);
            if (rssInfo == null) return;

            Context.RssContexts.Remove(rssInfo);
            Context.SaveChanges();
        }
    }
}
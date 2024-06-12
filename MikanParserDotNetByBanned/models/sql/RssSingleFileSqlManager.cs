namespace MikanParserDotNetByBanned.models.sql
{
    internal class RssInfoSqlManager
    {
        private static readonly RssSingleFileContext Context;

        static RssInfoSqlManager()
        {
            Context = new RssSingleFileContext();
            Context.Database.EnsureCreated();
        }

        public static void AddRssInfo(RssInfoSingleFile ? rssInfo)
        {
            if (rssInfo == null) return;
            Context.RssSingleFileContexts.Add(rssInfo);
            Context.SaveChanges();
        }

        public static RssInfoSingleFile ? GetRssInfo(int subjectId)
        {
            return Context.RssSingleFileContexts.FirstOrDefault(r => r.SubjectId == subjectId);
        }

        public static void UpdateRssInfo(int subjectId, RssInfoSingleFile updatedRssInfo)
        {
            var rssInfo = Context.RssSingleFileContexts.FirstOrDefault(r => r.SubjectId == subjectId);
            if (rssInfo == null) return;

            rssInfo.OriginTitle    = updatedRssInfo.OriginTitle;
            rssInfo.AnalysisTitle  = updatedRssInfo.AnalysisTitle;
            rssInfo.OriginFileName = updatedRssInfo.OriginFileName;
            rssInfo.PubDate        = updatedRssInfo.PubDate;
            rssInfo.Episode        = updatedRssInfo.Episode;
            rssInfo.Version        = updatedRssInfo.Version;
            rssInfo.Resolution     = updatedRssInfo.Resolution;
            rssInfo.FrameRate      = updatedRssInfo.FrameRate;
            rssInfo.MikanHomeUrl   = updatedRssInfo.MikanHomeUrl;
            rssInfo.DownloadStatus = updatedRssInfo.DownloadStatus;

            Context.SaveChanges();
        }

        public static void DeleteRssInfo(int subjectId)
        {
            var rssInfo = Context.RssSingleFileContexts.FirstOrDefault(r => r.SubjectId == subjectId);
            if (rssInfo == null) return;

            Context.RssSingleFileContexts.Remove(rssInfo);
            Context.SaveChanges();
        }
    }
}
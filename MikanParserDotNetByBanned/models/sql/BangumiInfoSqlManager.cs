namespace MikanParserDotNetByBanned.models.sql
{
    internal class BangumiInfoSqlManager
    {
        private static readonly BangumiContext Context;

        static BangumiInfoSqlManager()
        {
            Context = new BangumiContext();
            Context.Database.EnsureCreated();
        }

        public static void AddBangumi(BangumiInfo bangumi)
        {
            Context.BangumiInfos.Add(bangumi);
            Context.SaveChanges();
        }

        public static BangumiInfo ? GetBangumiById(int subjectId)
        {
            return Context.BangumiInfos.FirstOrDefault(b => b.SubjectId == subjectId);
        }

        public static void UpdateBangumi(int subjectId, BangumiInfo updatedBangumi)
        {
            var bangumi = Context.BangumiInfos.FirstOrDefault(b => b.SubjectId == subjectId);
            if (bangumi == null) return;
            bangumi.OriginName    = updatedBangumi.OriginName;
            bangumi.CnName        = updatedBangumi.CnName;
            bangumi.Pubdate       = updatedBangumi.Pubdate;
            bangumi.Platform      = updatedBangumi.Platform;
            bangumi.Summary       = updatedBangumi.Summary;
            bangumi.RatingScore   = updatedBangumi.RatingScore;
            bangumi.TotalEpisodes = updatedBangumi.TotalEpisodes;
            bangumi.Episode       = updatedBangumi.Episode;
            bangumi.SmallImageUrl = updatedBangumi.SmallImageUrl;
            bangumi.ImageUrl      = updatedBangumi.ImageUrl;
            Context.SaveChanges();
        }

        public static void DeleteBangumi(int subjectId)
        {
            var bangumi = Context.BangumiInfos.FirstOrDefault(b => b.SubjectId == subjectId);
            if (bangumi == null) return;
            Context.BangumiInfos.Remove(bangumi);
            Context.SaveChanges();
        }
    }
}
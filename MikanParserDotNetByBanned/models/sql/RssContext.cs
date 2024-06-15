using Microsoft.EntityFrameworkCore;

namespace MikanParserDotNetByBanned.models.sql
{
    internal class RssContext : DbContext
    {
        public DbSet<RssInfo> RssContexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas", "rss.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!); // Ensure the directory exists
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RssInfo>(entity =>
            {
                entity.ToTable("RssItemInfoTable");
                entity.HasKey(e => e.Url);
                entity.Property(e => e.SubjectId).HasColumnName("subject_id");
                entity.Property(e => e.OriginTitle).HasColumnName("origin_title");
                entity.Property(e => e.AnalysisAnimeTitle).HasColumnName("analysis_anime_title");
                entity.Property(e => e.PubDate).HasColumnName("pubdate");
                entity.Property(e => e.FirstEpisode).HasColumnName("first_episode");
                entity.Property(e => e.EndEpisode).HasColumnName("end_episode");
                entity.Property(e => e.Version).HasColumnName("version");
                entity.Property(e => e.Resolution).HasColumnName("resolution");
                entity.Property(e => e.FrameRate).HasColumnName("frame_rate");
                entity.Property(e => e.MikanHomeUrl).HasColumnName("mikan_home_url");
                entity.Property(r => r.DownloadStatus).HasConversion<string>().HasColumnName("download_status");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
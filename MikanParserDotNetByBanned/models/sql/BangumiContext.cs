using Microsoft.EntityFrameworkCore;

namespace MikanParserDotNetByBanned.models.sql
{
    internal class BangumiContext : DbContext
    {
        public DbSet<BangumiInfo> BangumiInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas", "bangumi.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!); // Ensure the directory exists
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangumiInfo>(entity =>
            {
                entity.ToTable("BangumiInfoTable");
                entity.HasKey(e => e.SubjectId);
                entity.Property(e => e.Platform).HasColumnName("platform");
                entity.Property(e => e.OriginName).HasColumnName("origin_name");
                entity.Property(e => e.CnName).HasColumnName("cn_name");
                entity.Property(e => e.Pubdate).HasColumnName("pubdate");
                entity.Property(e => e.Platform).HasColumnName("platform");
                entity.Property(e => e.Summary).HasColumnName("summary");
                entity.Property(e => e.RatingScore).HasColumnName("rating_score");
                entity.Property(e => e.TotalEpisodes).HasColumnName("total_episodes");
                entity.Property(e => e.Episode).HasColumnName("episode");
                entity.Property(e => e.SmallImageUrl).HasColumnName("small_image_url");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
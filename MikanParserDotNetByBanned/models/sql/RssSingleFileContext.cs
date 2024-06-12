using Microsoft.EntityFrameworkCore;

namespace MikanParserDotNetByBanned.models.sql
{
    internal class RssSingleFileContext : DbContext
    {
        public DbSet<RssInfoSingleFile> RssSingleFileContexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas", "bangumi.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!); // Ensure the directory exists
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RssInfoSingleFile>(entity =>
            {
                entity.ToTable("RssItemInfoTable");
                entity.Property(r => r.DownloadStatus).HasConversion<string>();
            });
        }
    }
}
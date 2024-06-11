using Microsoft.EntityFrameworkCore;

namespace MikanParserDotNetByBanned.models.sql
{
    internal class RssContext : DbContext
    {
        public DbSet<RssItemInfo> RssItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas", "rss.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)); // Ensure the directory exists
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RssItemInfo>().ToTable("RssItemInfoTable").HasKey(r => r.Id);
        }
    }
}
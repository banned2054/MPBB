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
            modelBuilder.Entity<BangumiInfo>().ToTable("BangumiInfoTable").HasKey(b => b.SubjectId);
        }
    }
}
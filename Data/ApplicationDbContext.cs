using Microsoft.EntityFrameworkCore;
using ProgrammersToolKit.Core;
using System;
using System.IO;

namespace ProgrammersToolKit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApiTestDefinition> ApiTests { get; set; }
        public DbSet<ApiTestHistoryEntry> ApiTestHistory { get; set; }
        public DbSet<ApiTestGroup> ApiTestGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "ProgrammersToolKit");
            Directory.CreateDirectory(dir);
            var dbPath = Path.Combine(dir, "apitests.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}

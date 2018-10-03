using GuessResult.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessResult.DB
{
    public class GuessResultContext : DbContext
    {
        public DbSet<GRUser> Users { get; set; }
        public DbSet<GREvent> Events { get; set; }
        public DbSet<GRUserEvent> UserEvents { get; set; }
        public DbSet<GRNewsFeed> NewsFeed { get; set; }
        public DbSet<GRNewsFeedComment> NewsFeedComment { get; set; }
        public DbSet<GRNewsFeedLike> NewsFeedLike { get; set; }
        public GuessResultContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GuessResultContext, Migrations.Configuration>());
        }
    }
}
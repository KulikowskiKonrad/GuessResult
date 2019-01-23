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
        public DbSet<GRNewsFeed> NewsFeeds { get; set; }
        public DbSet<GRNewsFeedComment> NewsFeedComments { get; set; }
        public DbSet<GRNewsFeedLike> NewsFeedLikes { get; set; }
        public DbSet<GRFile> Files { get; set; }
        public GuessResultContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GuessResultContext, Migrations.Configuration>());
        }
    }
}
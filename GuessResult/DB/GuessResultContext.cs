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

        public GuessResultContext()
            : base("DefaultConnection")
        {
        }
    }
}
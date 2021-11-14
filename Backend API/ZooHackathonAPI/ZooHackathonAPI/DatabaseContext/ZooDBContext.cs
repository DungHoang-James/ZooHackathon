using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.Entities;

namespace ZooHackathonAPI.DatabaseContext
{
    public class ZooDBContext:DbContext
    {
        public DbSet<User> Users{ get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<ReportText> ReportTexts { get; set; }

        public ZooDBContext(DbContextOptions<ZooDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().Property(r => r.UserID).IsRequired(false);
        }
    }
}

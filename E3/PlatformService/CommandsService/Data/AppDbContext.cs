using CommandsService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {

        }

        public DbSet<platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<platform>().HasMany(p => p.Commands).WithOne(p => p.Platforms).HasForeignKey(p => p.PlatformId);
            modelBuilder.Entity<Command>().HasOne(p => p.Platforms).WithMany(p => p.Commands).HasForeignKey(p => p.PlatformId);
        }
    }
}

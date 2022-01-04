using Infrastructure_Administration_Backend.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Data
{
    public class InfrastructureAdminitrationDBContext : IdentityDbContext
    {
        public InfrastructureAdminitrationDBContext(DbContextOptions<InfrastructureAdminitrationDBContext> options):base(options){}
        public DbSet<Status> status { get; set; }
        public DbSet<User> user { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(x => x.statuses).WithMany(x => x.user).HasForeignKey(x=>x.StatusId);
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

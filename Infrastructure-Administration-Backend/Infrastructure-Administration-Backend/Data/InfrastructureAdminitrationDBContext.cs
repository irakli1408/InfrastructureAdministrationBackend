using Infrastructure_Administration_Backend.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure_Administration_Backend.Data
{
    public class InfrastructureAdminitrationDBContext : IdentityDbContext<ApplicationUser>
    {
        public InfrastructureAdminitrationDBContext(DbContextOptions<InfrastructureAdminitrationDBContext> options):base(options){}
        public DbSet<Status> Statuses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
            .HasOne(x => x.Statuses)
            .WithMany(x => x.User)
            .HasForeignKey(x=>x.StatusId);
            base.OnModelCreating(modelBuilder);
        }
    }
}

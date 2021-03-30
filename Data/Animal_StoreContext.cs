using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Animal_Store.Models;

namespace Animal_Store.Data
{
    public class Animal_StoreContext : DbContext
    {
        public Animal_StoreContext (DbContextOptions<Animal_StoreContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<wish_list>()
                .HasKey(po => new { po.CustomerId, po.PetsId });

            modelBuilder.Entity<wish_list>()
                .HasOne(po => po.pet)
                .WithMany(o => o.wish_list)
                .HasForeignKey(po => po.PetsId);

            modelBuilder.Entity<wish_list>()
                .HasOne(po => po.Customer)
                .WithMany(o => o.wish_list)
                .HasForeignKey(po => po.CustomerId);
        }
        public DbSet<Animal_Store.Models.Pets> Pets { get; set; }

        public DbSet<Animal_Store.Models.Stores> Stores { get; set; }

        public DbSet<Animal_Store.Models.Charts> Charts { get; set; }

        public DbSet<Animal_Store.Models.Customer> Customer { get; set; }

        public DbSet<Animal_Store.Models.wish_list> wish_list { get; set; }
    }
}

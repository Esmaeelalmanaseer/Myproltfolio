using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
   public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<proltfolioItem>().Property(x => x.ID).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(
                new Owner {ID=Guid.NewGuid(),
                           Avatar="avatar.jpg",
                           FullName="Esmaeel Almanaseer",
                           profil=".NET CORE Devloper"
                }
                );
        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<proltfolioItem> ProltfolioItems { get; set; }
    }
}

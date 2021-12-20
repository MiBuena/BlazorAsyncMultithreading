using Microsoft.EntityFrameworkCore;
using PopulationCensus.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.DB
{
    public class PopulationContext : DbContext
    {
        public PopulationContext(DbContextOptions<PopulationContext> options)
            : base(options)
        {
        }

        public DbSet<Age> Ages { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Ethnicity> Ethnicity { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Year> Year { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CensusAreaData>()
                .HasOne(p => p.Year)
                .WithMany(b => b.Censuses)
                .HasForeignKey(p => p.YearId);

            builder.Entity<CensusAreaData>()
                .HasOne(p => p.Age)
                .WithMany(b => b.Censuses)
                .HasForeignKey(p => p.AgeId);

            builder.Entity<CensusAreaData>()
                .HasOne(p => p.Area)
                .WithMany(b => b.Censuses)
                .HasForeignKey(p => p.AreaId);

            builder.Entity<CensusAreaData>()
                .HasOne(p => p.Ethnicity)
                .WithMany(b => b.Censuses)
                .HasForeignKey(p => p.EthnicityId);

            builder.Entity<CensusAreaData>()
                .HasOne(p => p.Gender)
                .WithMany(b => b.Censuses)
                .HasForeignKey(p => p.GenderId);
        }
    }
}

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
    }
}

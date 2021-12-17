using Microsoft.EntityFrameworkCore;
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
    }
}

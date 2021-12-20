using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Entities
{
    public class CensusAreaData
    {
        public int Id { get; set; }

        public int YearId { get; set; }
        public Year Year { get; set; }

        public int AgeId { get; set; }
        public Age Age { get; set; }

        public int EthnicityId { get; set; }
        public Ethnicity Ethnicity { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Entities
{
    public class Gender : BaseContentEntity
    {
        public ICollection<CensusAreaData> Censuses { get; set; } = new List<CensusAreaData>();
    }
}

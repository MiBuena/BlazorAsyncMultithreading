namespace PopulationCensus.Data.Entities
{
    public class Age : BaseContentEntity
    {
        public ICollection<CensusAreaData> Censuses { get; set; } = new List<CensusAreaData>();
    }
}

using PopulationCensus.Data.Entities;

namespace PopulationCensus.Server.Interfaces
{
    public interface ICensusDataService
    {
        Task<IEnumerable<CensusAreaData>> GetAllDataAsync(CancellationToken token);
    }
}

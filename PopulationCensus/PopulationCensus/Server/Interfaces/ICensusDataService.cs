using PopulationCensus.Data.Entities;
using System.Runtime.CompilerServices;

namespace PopulationCensus.Server.Interfaces
{
    public interface ICensusDataService
    {
        Task<IEnumerable<CensusAreaData>> GetLimitedCensusData(CancellationToken token);

        IAsyncEnumerable<CensusAreaData> GetAllCensusData([EnumeratorCancellation] CancellationToken token);
    }
}

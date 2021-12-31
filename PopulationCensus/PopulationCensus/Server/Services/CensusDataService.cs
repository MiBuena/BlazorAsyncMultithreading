using PopulationCensus.Data.Entities;
using PopulationCensus.Data.Interfaces;
using PopulationCensus.Server.Interfaces;

namespace PopulationCensus.Server.Services
{
    public class CensusDataService : ICensusDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CensusDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CensusAreaData>> GetAllDataAsync(CancellationToken token)
        {
            var a = await _unitOfWork.CensusAreaDataRepository.GetListAsync(cancellationToken: token, take: 1000000);
            return a;
        }
    }
}

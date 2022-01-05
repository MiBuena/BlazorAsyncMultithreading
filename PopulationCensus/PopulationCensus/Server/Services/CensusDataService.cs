using PopulationCensus.Data.Entities;
using PopulationCensus.Data.Interfaces;
using PopulationCensus.Server.Interfaces;
using System.Runtime.CompilerServices;

namespace PopulationCensus.Server.Services
{
    public class CensusDataService : ICensusDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CensusDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CensusAreaData>> GetLimitedCensusData(CancellationToken token)
        {
            var a = await _unitOfWork.CensusAreaDataRepository.GetListAsync(cancellationToken: token, take: 1000000);
            return a;
        }

        public async IAsyncEnumerable<CensusAreaData> GetAllCensusData([EnumeratorCancellation] CancellationToken token)
        {
            var a = _unitOfWork.CensusAreaDataRepository.GetOneByOneAsync(cancellationToken: token, take: 1000000);

            await foreach (var stockPrice in a.WithCancellation(token))
            {
                token.ThrowIfCancellationRequested();
                yield return stockPrice;
            }
        }
    }
}

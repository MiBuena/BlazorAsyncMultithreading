using Microsoft.AspNetCore.Mvc;
using PopulationCensus.Data.Entities;
using PopulationCensus.Server.Interfaces;
using System.Runtime.CompilerServices;

namespace PopulationCensus.Server.Controllers
{
    public class CensusDataController : ControllerBase
    {
        private readonly ICensusDataService _censusDataService;

        public CensusDataController(ICensusDataService censusDataService)
        {
            _censusDataService = censusDataService;
        }

        [HttpGet("census-data/all")]
        public async Task<IActionResult> GetAllCensusData(CancellationToken token = default(CancellationToken))
        {
            var a = await _censusDataService.GetLimitedCensusData(token);

            return Ok(a);
        }

        [HttpGet("census-data/one-by-one")]
        public async IAsyncEnumerable<CensusAreaData> GetDataOneByOne([EnumeratorCancellation] CancellationToken token = default(CancellationToken))
        {
            var a = _censusDataService.GetAllCensusData(token);

            await foreach (var product in a.WithCancellation(token))
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(5000);
                yield return product;
            }
        }
    }
}

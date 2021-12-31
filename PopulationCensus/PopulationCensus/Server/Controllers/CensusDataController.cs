using Microsoft.AspNetCore.Mvc;
using PopulationCensus.Server.Interfaces;

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
        public async Task<IActionResult> GetAllCensusData(CancellationToken token)
        {
            var a = await _censusDataService.GetAllDataAsync(token);

            return Ok(a);
        }
    }
}

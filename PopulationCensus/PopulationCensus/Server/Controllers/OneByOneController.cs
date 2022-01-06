using Microsoft.AspNetCore.Mvc;
using PopulationCensus.Data.Entities;
using System.Runtime.CompilerServices;

namespace PopulationCensus.Server.Controllers
{
    public class OneByOneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("data/one-by-one")]
        public async IAsyncEnumerable<CensusAreaData> GetDataOneByOne([EnumeratorCancellation] CancellationToken token = default(CancellationToken))
        {
            for (int i = 0; i < 10; i++)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(2000);
                yield return new CensusAreaData();
            }
        }
    }
}

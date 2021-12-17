using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PopulationCensus.Server.Controllers
{
    public class UploadController : ControllerBase
    {
        [HttpPost("upload/single")]
        public async Task<IActionResult> MultipleAsync(IFormFile file)
        {
            try
            {
                using (var stream = new StreamReader(System.IO.File.OpenRead(@"C:\Users\Lenovo\Documents\GithubRepositories\PopulationCensus\PopulationCensus\Server\FilesToImport\DimenLookupAge8277.csv")))
                {
                    var lines = new List<string>();

                    string line;
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }
                }

                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

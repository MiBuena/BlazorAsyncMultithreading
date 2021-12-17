using PopulationCensus.Server.Interfaces;
using System.Collections.Generic;

namespace PopulationCensus.Server.Services
{
    public class LocalFileService : IFileService
    {
        public async Task<IEnumerable<string>> ReadFileAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                await reader.ReadLineAsync();

                var lines = new List<string>();

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }

                return lines;
            }
        }

        public async IAsyncEnumerable<IEnumerable<string>> ReadFileInPortionsAsync(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                await reader.ReadLineAsync();

                var lines = new List<string>();

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);

                    if (lines.Count == 1000)
                    {
                        yield return lines;
                        lines = new List<string>();
                    }

                }

                yield return lines;
            }
        }
    }
}

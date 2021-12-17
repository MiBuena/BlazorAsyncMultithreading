using PopulationCensus.Server.Interfaces;

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
    }
}

using PopulationCensus.Server.Interfaces;
using System.Collections.Generic;

namespace PopulationCensus.Server.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IStreamReaderWrapper _readerWrapper;

        public LocalFileService(IStreamReaderWrapper reader)
        {
            _readerWrapper = reader;
        }

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
            using (var reader = _readerWrapper.GetStreamReader(file))
            {
                await reader.ReadLineAsync(); //skip the title row

                var lines = new List<string>();

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);

                    if (lines.Count == 1000)
                    {
                        yield return lines; //Pass only 1000 on 1 batch to be saved to the db
                        lines = new List<string>();
                    }

                }

                yield return lines;
            }
        }
    }
}

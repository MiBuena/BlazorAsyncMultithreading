using PopulationCensus.Server.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            using (var reader = _readerWrapper.GetStreamReader(file))
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

        public async IAsyncEnumerable<string> ReadFileAsStream([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using (var reader = _readerWrapper.GetStreamReader("Files/Data8277.csv"))
            {
                await reader.ReadLineAsync();

                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return line;
                }
            }
        }

        public async Task<IEnumerable<string>> ReadFileAllLines(string path)
        {
            string? lines;
            using (var reader = _readerWrapper.GetStreamReader(path))
            {
                await reader.ReadLineAsync();

                lines = await reader.ReadToEndAsync();
            }

            return lines.Split("\r\n");
        }

        public async Task<LinkedList<string>> ReadLargeFileWithBufferRead(string path)
        {
            var lines = new LinkedList<string>();

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        await sr.ReadLineAsync();

                        string? line;
                        while ((line = await sr.ReadLineAsync()) != null)
                        {
                            lines.AddLast(line);
                        }
                    }
                }
            }

            return lines;
        }

        public async IAsyncEnumerable<string> ReadLargeFileWithBufferReadInPortions(string path)
        {
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        await sr.ReadLineAsync();

                        string? line;
                        while ((line = await sr.ReadLineAsync()) != null)
                        {
                            yield return line;
                        }
                    }
                }
            }
        }



        public async Task<List<string>> ReadLargeFileWithBufferReadList(string path)
        {
            var lines = new List<string>();

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        await sr.ReadLineAsync();

                        string? line;
                        while ((line = await sr.ReadLineAsync()) != null)
                        {
                            lines.Add(line);
                        }
                    }
                }
            }

            return lines;
        }
    }
}

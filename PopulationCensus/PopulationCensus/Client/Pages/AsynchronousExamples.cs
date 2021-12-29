using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace PopulationCensus.Client.Pages
{
    public partial class AsynchronousExamples
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        private async void LoadFiles(InputFileChangeEventArgs e)
        {
            var a = await ReadLargeFileAsync("Files/Data8277Half.csv");
        }

        public async Task<LinkedList<string>> ReadLargeFileAsync(string path)
        {
            var lines = new LinkedList<string>();

            using (var stream = await _httpClient.GetStreamAsync(path))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    await sr.ReadLineAsync();

                    string? line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        lines.AddLast(line);
                    }
                }

            }

            return lines;
        }
    }
}

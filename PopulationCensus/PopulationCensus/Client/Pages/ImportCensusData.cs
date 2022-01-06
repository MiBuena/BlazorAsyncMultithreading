using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Radzen;
using System.Runtime.CompilerServices;
using System.Text;

namespace PopulationCensus.Client.Pages
{
    public partial class ImportCensusData
    {
        public long LinesCount;

        public List<string> Lines { get; set; } = new List<string>();

        protected override async Task<Task> OnInitializedAsync()
        {
            _client.Timeout = new TimeSpan(200, 0, 0);
            return base.OnInitializedAsync();
        }

        //void OnComplete(UploadCompleteEventArgs args)
        //{
        //    var a = args.JsonResponse;
        //}

        public async void UploadLargeFile()
        {
            await foreach (var item in ReadFileAsStream())
            {
                string jsonContent = JsonConvert.SerializeObject(item);

                var stringContent =
  new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var httpResponse = await _client.PostAsync("upload/1", stringContent);

                var content = await httpResponse.Content.ReadAsStringAsync();

            }
        }

        public async IAsyncEnumerable<string> ReadFileAsStream([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var streamOfFile = await _client.GetStreamAsync("Files/Data8277.csv", cancellationToken);

            using (var reader = new StreamReader(streamOfFile))
            {
                await reader.ReadLineAsync();


                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {

                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    yield return line; //Pass only 1000 on 1 batch to be saved to the db
                }
            }
        }

        public async void UploadLargeCensusDataFile()
        {
            var httpResponse = await _client.GetAsync("upload/4");
        }

        public async void UploadLargeCensusDataFile8()
        {
            var httpResponse = await _client.GetAsync("upload/8");
        }
    }
}


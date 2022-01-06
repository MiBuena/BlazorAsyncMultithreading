using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using PopulationCensus.Data.Entities;
using System.Text.Json;

namespace PopulationCensus.Client.Pages
{
    public partial class OneByOneCancelling
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        public bool IsStartButtonDisabled_OneByOne { get; set; } = false;
        public bool IsCancelButtonDisabled_OneByOne { get; set; } = true;
        public string MessageOneByOne { get; set; }

        private CancellationTokenSource _ctsOneByOne;

        private List<CensusAreaData> censusData = new List<CensusAreaData>();

        public async void StartButton_OneByOne_Click()
        {
            IsStartButtonDisabled_OneByOne = true;
            IsCancelButtonDisabled_OneByOne = false;
            MessageOneByOne = null;

            try
            {
                _ctsOneByOne = new CancellationTokenSource();
                CancellationToken token = _ctsOneByOne.Token;

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "data/one-by-one");
                request.SetBrowserResponseStreamingEnabled(true);

                using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, token);

                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();

                IAsyncEnumerable<CensusAreaData> weatherForecasts = JsonSerializer.DeserializeAsyncEnumerable<CensusAreaData>(
         responseStream,
         new JsonSerializerOptions
         {
             PropertyNameCaseInsensitive = true,
             DefaultBufferSize = 128
         });

                await foreach (var item in weatherForecasts)
                {
                    censusData.Add(item);
                    StateHasChanged();
                }

                MessageOneByOne = "Task completed successfully.";
            }
            catch (OperationCanceledException ex)
            {
                MessageOneByOne = "Task was cancelled.";

            }
            catch (Exception ex)
            {
                MessageOneByOne = "Task completed with error.";
                throw;
            }
            finally
            {
                _ctsOneByOne.Dispose();
                _ctsOneByOne = null;

                IsStartButtonDisabled_OneByOne = false;
                IsCancelButtonDisabled_OneByOne = true;
                StateHasChanged();
            }

        }

        public void CancelButton_OneByOne_Click()
        {
            _ctsOneByOne.Cancel();
            IsCancelButtonDisabled_OneByOne = true;
        }
    }
}

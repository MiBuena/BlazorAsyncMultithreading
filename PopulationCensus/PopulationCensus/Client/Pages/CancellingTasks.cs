using Microsoft.AspNetCore.Components;

namespace PopulationCensus.Client.Pages
{
    public partial class CancellingTasks
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        public bool IsStartButtonDisabled { get; set; } = false;

        public bool IsCancelButtonDisabled { get; set; } = true;

        public string Message { get; set; }


        private CancellationTokenSource _cts;


        public async void StartButton_Click()
        {
            IsStartButtonDisabled = true;
            IsCancelButtonDisabled = false;
            Message = null;

            try
            {
                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;

                await Task.Delay(TimeSpan.FromSeconds(5), token);
                Message = "Task completed successfully.";
            }
            catch (OperationCanceledException ex)
            {
                Message = "Task was cancelled.";
            }
            catch (Exception ex)
            {
                Message = "Task completed with error.";
                throw;
            }
            finally
            {
                IsStartButtonDisabled = false;
                IsCancelButtonDisabled = true;
                StateHasChanged();
            }

            //var result = await ReadFileAsync("Files/First38lines.csv");
        }
        public void CancelButton_Click()
        {
            _cts.Cancel();
            IsCancelButtonDisabled = true;
        }


        public async Task<LinkedList<string>> ReadFileAsync(string path)
        {
            var lines = new LinkedList<string>();

            using (var stream = await _httpClient.GetStreamAsync(path))
            {
                using (BufferedStream bs = new BufferedStream(stream))
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

            await Task.Delay(5000);
            return lines;
        }
    }
}

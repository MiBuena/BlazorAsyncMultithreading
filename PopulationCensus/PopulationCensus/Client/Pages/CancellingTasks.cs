using Microsoft.AspNetCore.Components;

namespace PopulationCensus.Client.Pages
{
    public partial class CancellingTasks
    {
        [Inject]
        private HttpClient _httpClient { get; set; }



        public bool IsStartButtonDisabled_SimpleDelayTask { get; set; } = false;
        public bool IsCancelButtonDisabled_SimpleDelayTask { get; set; } = true;
        public string MessageSimpleDelayTask { get; set; }

        private CancellationTokenSource _ctsDelayTask;


        #region Cancel a simple task

        public async void StartButton_Click()
        {
            IsStartButtonDisabled_SimpleDelayTask = true;
            IsCancelButtonDisabled_SimpleDelayTask = false;
            MessageSimpleDelayTask = null;

            try
            {
                _ctsDelayTask = new CancellationTokenSource();
                CancellationToken token = _ctsDelayTask.Token;

                await Task.Delay(TimeSpan.FromSeconds(5), token);
                MessageSimpleDelayTask = "Task completed successfully.";
            }
            catch (OperationCanceledException ex)
            {
                MessageSimpleDelayTask = "Task was cancelled.";
            }
            catch (Exception ex)
            {
                MessageSimpleDelayTask = "Task completed with error.";
                throw;
            }
            finally
            {
                IsStartButtonDisabled_SimpleDelayTask = false;
                IsCancelButtonDisabled_SimpleDelayTask = true;
                StateHasChanged();
            }
        }
        public void CancelButton_Click()
        {
            _ctsDelayTask.Cancel();
            IsCancelButtonDisabled_SimpleDelayTask = true;
        }

        #endregion



        #region Cancel a simple task
        public async void ReadFile()
        {
            var a = await File.ReadAllLinesAsync("Files/Data8277Half.csv");
        }
        #endregion


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

﻿using Microsoft.AspNetCore.Components;

namespace PopulationCensus.Client.Pages
{
    public partial class CancellingTasks
    {
        [Inject]
        private HttpClient _httpClient { get; set; }


        #region Cancel a simple task

        public bool IsStartButtonDisabled_SimpleDelayTask { get; set; } = false;
        public bool IsCancelButtonDisabled_SimpleDelayTask { get; set; } = true;
        public string MessageSimpleDelayTask { get; set; }

        private CancellationTokenSource _ctsDelayTask;

        public async void StartButton_SimpleDelayTask_Click()
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
                _ctsDelayTask.Dispose();
                _ctsDelayTask = null;

                IsStartButtonDisabled_SimpleDelayTask = false;
                IsCancelButtonDisabled_SimpleDelayTask = true;
                StateHasChanged();
            }
        }
        public void CancelButton_SimpleDelayTask_Click()
        {
            _ctsDelayTask.Cancel();
            IsCancelButtonDisabled_SimpleDelayTask = true;
        }

        #endregion

        #region Cancel a long Http operation
        public bool IsStartButtonDisabled_LongHttpRequest { get; set; } = false;
        public bool IsCancelButtonDisabled_LongHttpRequest { get; set; } = true;
        public string MessageLongHttpRequest { get; set; }

        private CancellationTokenSource _ctsLongHttpRequest;


        public async void StartButton_LongHttpRequest_Click()
        {
            IsStartButtonDisabled_LongHttpRequest = true;
            IsCancelButtonDisabled_LongHttpRequest = false;
            MessageLongHttpRequest = null;

            try
            {
                _ctsLongHttpRequest = new CancellationTokenSource();
                CancellationToken token = _ctsLongHttpRequest.Token;

                using (var httpResponse = await _httpClient.GetAsync("upload/long-api-call", token))
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                }

                MessageLongHttpRequest = "Task completed successfully.";
            }
            catch (OperationCanceledException ex)
            {
                MessageLongHttpRequest = "Task was cancelled.";

            }
            catch (Exception ex)
            {
                MessageLongHttpRequest = "Task completed with error.";
                throw;
            }
            finally
            {
                _ctsLongHttpRequest.Dispose();
                _ctsLongHttpRequest = null;

                IsStartButtonDisabled_LongHttpRequest = false;
                IsCancelButtonDisabled_LongHttpRequest = true;
                StateHasChanged();
            }
        }

        public void CancelButton_LongHttpRequest_Click()
        {
            _ctsLongHttpRequest.Cancel();
            IsCancelButtonDisabled_LongHttpRequest = true;
        }

        #endregion

        #region Cancel a long DB call

        public bool IsStartButtonDisabled_LongDBRequest { get; set; } = false;
        public bool IsCancelButtonDisabled_LongDBRequest { get; set; } = true;
        public string MessageLongDBRequest { get; set; }

        private CancellationTokenSource _ctsLongDBRequest;
        
        public async void StartButton_LongDBRequest_Click()
        {
            IsStartButtonDisabled_LongDBRequest = true;
            IsCancelButtonDisabled_LongDBRequest = false;
            MessageLongDBRequest = null;

            try
            {
                _ctsLongDBRequest = new CancellationTokenSource();
                CancellationToken token = _ctsLongDBRequest.Token;

                using (var httpResponse = await _httpClient.GetAsync("census-data/all", token))
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                }

                MessageLongDBRequest = "Task completed successfully.";
            }
            catch (OperationCanceledException ex)
            {
                MessageLongDBRequest = "Task was cancelled.";

            }
            catch (Exception ex)
            {
                MessageLongDBRequest = "Task completed with error.";
                throw;
            }
            finally
            {
                _ctsLongDBRequest.Dispose();
                _ctsLongDBRequest = null;

                IsStartButtonDisabled_LongDBRequest = false;
                IsCancelButtonDisabled_LongDBRequest = true;
                StateHasChanged();
            }

        }

        public void CancelButton_LongDBRequest_Click()
        {
            _ctsLongDBRequest.Cancel();
            IsCancelButtonDisabled_LongDBRequest = true;
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

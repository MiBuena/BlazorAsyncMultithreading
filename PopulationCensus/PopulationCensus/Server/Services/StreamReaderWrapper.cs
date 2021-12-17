using PopulationCensus.Server.Interfaces;

namespace PopulationCensus.Server.Services
{
    public class StreamReaderWrapper : IStreamReaderWrapper
    {
        private StreamReader? _reader;

        public StreamReaderWrapper(IFormFile file)
        {
            _reader = new StreamReader(file.OpenReadStream());
        }

        public Task<string> ReadLineAsync()
        {
            return _reader.ReadLineAsync();
        }
        
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reader?.Dispose();
                _reader = null;
            }
        }
    }
}

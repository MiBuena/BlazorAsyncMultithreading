using PopulationCensus.Server.Interfaces;

namespace PopulationCensus.Server.Services
{
    public class StreamReaderWrapper : IStreamReaderWrapper
    {
        private StreamReader _reader;

        public StreamReader GetStreamReader(IFormFile file)
        {
            _reader = new StreamReader(file.OpenReadStream());
            return _reader;
        }

        public StreamReader GetStreamReader(string path)
        {
            _reader = new StreamReader(path);
            return _reader;
        }
    }
}

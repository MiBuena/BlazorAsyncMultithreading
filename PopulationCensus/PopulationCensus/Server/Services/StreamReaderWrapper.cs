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
    }
}

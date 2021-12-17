namespace PopulationCensus.Server.Interfaces
{
    public interface IStreamReaderWrapper : IDisposable
    {
        Task<string> ReadLineAsync();
    }
}

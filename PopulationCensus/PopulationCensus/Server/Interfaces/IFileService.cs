namespace PopulationCensus.Server.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<string>> ReadFileAsync(IFormFile file);

        IAsyncEnumerable<IEnumerable<string>> ReadFileInPortionsAsync(IFormFile file);
    }
}

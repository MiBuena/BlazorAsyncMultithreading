using System.Runtime.CompilerServices;

namespace PopulationCensus.Server.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<string>> ReadFileAsync(IFormFile file);

        IAsyncEnumerable<IEnumerable<string>> ReadFileInPortionsAsync(IFormFile file);

        Task<IEnumerable<string>> ReadFileAllLines(string path);

        IAsyncEnumerable<string> ReadFileAsStream([EnumeratorCancellation] CancellationToken cancellationToken = default);

        Task<LinkedList<string>> ReadLargeFileWithBufferRead(string path);

        IAsyncEnumerable<string> ReadLargeFileWithBufferReadInPortions(string path);
    }
}

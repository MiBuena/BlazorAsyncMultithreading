namespace PopulationCensus.Server.Interfaces
{
    public interface IImportService
    {
        Task ImportAgeFileAsync(IFormFile file);

        Task ImportAreaFileAsync(IFormFile file);
    }
}

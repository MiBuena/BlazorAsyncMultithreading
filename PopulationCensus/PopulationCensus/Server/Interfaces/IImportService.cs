namespace PopulationCensus.Server.Interfaces
{
    public interface IImportService
    {
        Task ImportAgeFileAsync(IFormFile file);
        Task ImportAreaFileAsync(IFormFile file);
        Task ImportEthnicityFileAsync(IFormFile file);
        Task ImportGenderFileAsync(IFormFile file);
        Task ImportYearsFileAsync(IFormFile file);
        Task ImportCensusDataFileAsync4(CancellationToken token = default(CancellationToken));
        Task ImportCensusDataFileAsync8();
    }
}

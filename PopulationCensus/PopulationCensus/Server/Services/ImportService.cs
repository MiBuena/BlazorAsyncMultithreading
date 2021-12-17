using PopulationCensus.Data.Entities;
using PopulationCensus.Server.Interfaces;

namespace PopulationCensus.Server.Services
{
    public class ImportService : IImportService
    {
        private readonly IFileService _fileService;

        public ImportService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task ImportAgeFileAsync(IFormFile file)
        {
            var fileContent = await _fileService.ReadFileAsync(file);

            var collection = new List<Age>();

            foreach (var item in fileContent)
            {
                var ageData = item.Split(',');
                var ageEntity = new Age()
                {
                    Code = ageData[0],
                    Description = ageData[1],
                };

                collection.Add(ageEntity);
            }
        }
    }
}

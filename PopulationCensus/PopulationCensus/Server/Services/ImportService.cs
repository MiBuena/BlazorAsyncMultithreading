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
            var fileContent = _fileService.ReadFileInPortionsAsync(file);

            await foreach (var item in fileContent)
            {
                var a = ExtractAgeEntities(item);
            }
        }

        private IEnumerable<Age> ExtractAgeEntities(IEnumerable<string> lines)
        {
            var collection = new List<Age>();

            foreach (var item2 in lines)
            {
                var ageData = item2.Split(',');
                var ageEntity = new Age()
                {
                    Code = ageData[0],
                    Description = ageData[1],
                };

                collection.Add(ageEntity);
            }

            return collection;
        }
    }
}

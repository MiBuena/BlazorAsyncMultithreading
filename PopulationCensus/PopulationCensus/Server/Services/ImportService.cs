using PopulationCensus.Data.Entities;
using PopulationCensus.Data.Interfaces;
using PopulationCensus.Server.Interfaces;

namespace PopulationCensus.Server.Services
{
    public class ImportService : IImportService
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public ImportService(IFileService fileService, IUnitOfWork unitOfWork)
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task ImportAgeFileAsync(IFormFile file)
        {
            var fileContent = _fileService.ReadFileInPortionsAsync(file);

            await foreach (var item in fileContent)
            {
                var extractedAges = ExtractAgeEntities(item);
                _unitOfWork.AgesRepository.AddRange(extractedAges);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private IEnumerable<Age> ExtractAgeEntities(IEnumerable<string> lines)
        {
            var collection = new List<Age>();

            foreach (var line in lines)
            {
                var ageData = line.Split(',');
                var ageEntity = new Age()
                {
                    Code = ageData[0],
                    Description = ageData[1],
                };

                collection.Add(ageEntity);
            }

            return collection;
        }

        public async Task ImportAreaFileAsync(IFormFile file)
        {
            var fileContent = _fileService.ReadFileInPortionsAsync(file);

            await foreach (var item in fileContent)
            {
                var extractedAges = ExtractAreaEntities(item);
                _unitOfWork.AreasRepository.AddRange(extractedAges);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private IEnumerable<Area> ExtractAreaEntities(IEnumerable<string> lines)
        {
            var collection = new List<Area>();

            foreach (var line in lines)
            {
                var ageData = line.Split(',');
                var ageEntity = new Area()
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

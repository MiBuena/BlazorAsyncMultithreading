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

        public async Task ImportEthnicityFileAsync(IFormFile file)
        {
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedEthnicities = ExtractEthnicityEntities(fileContent);
            _unitOfWork.EthnicitiesRepository.AddRange(extractedEthnicities);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Ethnicity> ExtractEthnicityEntities(IEnumerable<string> lines)
        {
            var collection = new List<Ethnicity>();

            foreach (var line in lines)
            {
                var ethnicityData = line.Split(',');
                var ethnicityEntity = new Ethnicity()
                {
                    Code = ethnicityData[0],
                    Description = ethnicityData[1],
                };

                collection.Add(ethnicityEntity);
            }

            return collection;
        }


        public async Task ImportGenderFileAsync(IFormFile file)
        {
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedGenderEntities = ExtractGenderEntities(fileContent);
            _unitOfWork.GendersRepository.AddRange(extractedGenderEntities);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Gender> ExtractGenderEntities(IEnumerable<string> lines)
        {
            var collection = new List<Gender>();

            foreach (var line in lines)
            {
                var ethnicityData = line.Split(',');
                var ethnicityEntity = new Gender()
                {
                    Code = ethnicityData[0],
                    Description = ethnicityData[1],
                };

                collection.Add(ethnicityEntity);
            }

            return collection;
        }


        public async Task ImportYearsFileAsync(IFormFile file)
        {
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedGenderEntities = ExtractYearEntities(fileContent);
            _unitOfWork.YearsRepository.AddRange(extractedGenderEntities);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Year> ExtractYearEntities(IEnumerable<string> lines)
        {
            var collection = new List<Year>();

            foreach (var line in lines)
            {
                var ethnicityData = line.Split(',');
                var ethnicityEntity = new Year()
                {
                    Code = ethnicityData[0],
                    Description = ethnicityData[1],
                };

                collection.Add(ethnicityEntity);
            }

            return collection;
        }
    }
}

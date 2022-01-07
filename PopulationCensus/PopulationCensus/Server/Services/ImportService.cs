using Microsoft.Data.SqlClient;
using PopulationCensus.Data.Entities;
using PopulationCensus.Data.Interfaces;
using PopulationCensus.Server.Interfaces;
using System.Data;
using System.Diagnostics;

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
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedAges = ExtractAgeEntities(fileContent);
            _unitOfWork.AgesRepository.AddRange(extractedAges);
            await _unitOfWork.SaveChangesAsync();
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
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedAreas = ExtractAreaEntities(fileContent);
            _unitOfWork.AreasRepository.AddRange(extractedAreas);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Area> ExtractAreaEntities(IEnumerable<string> lines)
        {
            var collection = new List<Area>();

            foreach (var line in lines)
            {
                var areaData = line.Split(',');
                var areaEntity = new Area()
                {
                    Code = areaData[0],
                    Description = areaData[1],
                };

                collection.Add(areaEntity);
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
                var genderData = line.Split(',');
                var genderEntity = new Gender()
                {
                    Code = genderData[0],
                    Description = genderData[1],
                };

                collection.Add(genderEntity);
            }

            return collection;
        }


        public async Task ImportYearsFileAsync(IFormFile file)
        {
            var fileContent = await _fileService.ReadFileAsync(file);

            var extractedYearEntities = ExtractYearEntities(fileContent);
            _unitOfWork.YearsRepository.AddRange(extractedYearEntities);
            await _unitOfWork.SaveChangesAsync();
        }

        private IEnumerable<Year> ExtractYearEntities(IEnumerable<string> lines)
        {
            var collection = new List<Year>();

            foreach (var line in lines)
            {
                var yearData = line.Split(',');
                var yearEntity = new Year()
                {
                    Code = yearData[0],
                    Description = yearData[1],
                };

                collection.Add(yearEntity);
            }

            return collection;
        }

        public async Task ReadCensusDataFileWithIAsyncEnumerableAsync(CancellationToken token = default(CancellationToken))
        {
            var years = await _unitOfWork.YearsRepository.GetListAsync();
            var ages = await _unitOfWork.AgesRepository.GetListAsync();
            var ethnicities = await _unitOfWork.EthnicitiesRepository.GetListAsync();
            var genders = await _unitOfWork.GendersRepository.GetListAsync();
            var areas = await _unitOfWork.AreasRepository.GetListAsync();


            var yearsDictionary = years.ToDictionary(x => x.Code);
            var ageDictionary = ages.ToDictionary(x => x.Code);
            var ethnicitiesDictionary = ethnicities.ToDictionary(x => x.Code);
            var gendersDictionary = genders.ToDictionary(x => x.Code);
            var areasDictionary = areas.ToDictionary(x => x.Code);

            var fileContent = _fileService.ReadLargeFileWithBufferReadInPortions("Files/Data8277Half.csv");

            var totalList = new LinkedList<CensusAreaData>();

            await foreach (var item in fileContent)
            {
                var censusDataEntity = ExtractCensusDataEntity(item, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary);
                totalList.AddLast(censusDataEntity);
            }
        }

        public async Task ProcessCensusDataInParallelAsync(CancellationToken token = default(CancellationToken))
        {
            var years = await _unitOfWork.YearsRepository.GetListAsync();
            var ages = await _unitOfWork.AgesRepository.GetListAsync();
            var ethnicities = await _unitOfWork.EthnicitiesRepository.GetListAsync();
            var genders = await _unitOfWork.GendersRepository.GetListAsync();
            var areas = await _unitOfWork.AreasRepository.GetListAsync();


            var yearsDictionary = years.ToDictionary(x => x.Code);
            var ageDictionary = ages.ToDictionary(x => x.Code);
            var ethnicitiesDictionary = ethnicities.ToDictionary(x => x.Code);
            var gendersDictionary = genders.ToDictionary(x => x.Code);
            var areasDictionary = areas.ToDictionary(x => x.Code);

            var fileContent = await _fileService.ReadLargeFileWithBufferRead("Files/Data8277Half.csv");


            var censusEntities = fileContent
                .AsParallel()
                .AsOrdered()
                .WithCancellation(token)
                .Select(x => ExtractCensusDataEntity(x, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary))
                .ToList();
        }

        private CensusAreaData ExtractCensusDataEntity(string line, IDictionary<string, Year> years,
            IDictionary<string, Age> ages, IDictionary<string, Ethnicity> ethnicities,
            IDictionary<string, Gender> genders, IDictionary<string, Area> areas)
        {
            var ethnicityData = line.Split(',');

            var year = years[ethnicityData[0]];
            var age = ages[ethnicityData[1]];
            var ethnicity = ethnicities[ethnicityData[2]];
            var gender = genders[ethnicityData[3]];
            var area = areas[ethnicityData[4]];

            int count = 0;
            int.TryParse(ethnicityData[5], out count);

            var ethnicityEntity = new CensusAreaData()
            {
                YearId = year.Id,
                AgeId = age.Id,
                EthnicityId = ethnicity.Id,
                GenderId = gender.Id,
                AreaId = area.Id,
                Count = count
            };

            return ethnicityEntity;
        }
    }
}

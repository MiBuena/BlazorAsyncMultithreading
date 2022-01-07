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

        public async Task ImportCensusDataFileAsync4(CancellationToken token = default(CancellationToken))
        {
            var timer = new Stopwatch();
            timer.Start();

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
                var a = ExtractCensusDataEntity(item, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary);
                totalList.AddLast(a);
            }

            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;

        }

        public async Task ImportCensusDataFileAsync8(CancellationToken token = default(CancellationToken))
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


            var timer = new Stopwatch();
            timer.Start();

            //var totalList = new LinkedList<CensusAreaData>();

            //foreach (var item in fileContent)
            //{
            //    var b = ExtractCensusDataEntity(item, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary);
            //    totalList.AddLast(b);
            //}

            TimeSpan sequentialLoop = timer.Elapsed;
            timer.Restart();



            var censusEntities = fileContent
    .AsParallel()
    .AsOrdered()
    .WithCancellation(token)
    .Select(x => ExtractCensusDataEntity(x, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary))
    .ToList();

            //        TimeSpan plinq = timer.Elapsed;
            //        timer.Restart();

            //        var a = from x in fileContent.AsParallel().AsOrdered()
            //                select ExtractCensusDataEntity(x, yearsDictionary, ageDictionary, ethnicitiesDictionary, gendersDictionary, areasDictionary);

            //        var m = a.ToList();


            TimeSpan plinq2 = timer.Elapsed;
            timer.Stop();

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

        private void ExtractCensusDataEntity(string line, IDictionary<string, Year> years,
    IDictionary<string, Age> ages, IDictionary<string, Ethnicity> ethnicities,
    IDictionary<string, Gender> genders, IDictionary<string, Area> areas,
    LinkedList<CensusAreaData> collection)
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

            collection.AddLast(ethnicityEntity);
        }

        //public void A(IEnumerable<CensusAreaData> inputCollection)
        //{
        //    string connectionString = GetConnectionString();
        //    // Open a connection to the AdventureWorks database.
        //    using (SqlConnection connection =
        //               new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        // Perform an initial count on the destination table.
        //        SqlCommand commandRowCount = new SqlCommand(
        //            "SELECT COUNT(*) FROM " +
        //            "dbo.CensusAreaData;",
        //            connection);
        //        long countStart = System.Convert.ToInt32(
        //            commandRowCount.ExecuteScalar());
        //        Console.WriteLine("Starting row count = {0}", countStart);

        //        // Create a table with some rows.
        //        DataTable newProducts = MakeTable(inputCollection);

        //        // Create the SqlBulkCopy object.
        //        // Note that the column positions in the source DataTable
        //        // match the column positions in the destination table so
        //        // there is no need to map columns.
        //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
        //        {
        //            bulkCopy.DestinationTableName =
        //                "dbo.CensusAreaData";

        //            try
        //            {
        //                bulkCopy.BulkCopyTimeout = 10000;
        //                // Write from the source to the destination.
        //                bulkCopy.WriteToServer(newProducts);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }

        //        // Perform a final count on the destination
        //        // table to see how many rows were added.
        //        long countEnd = System.Convert.ToInt32(
        //            commandRowCount.ExecuteScalar());
        //        Console.WriteLine("Ending row count = {0}", countEnd);
        //        Console.WriteLine("{0} rows were added.", countEnd - countStart);
        //        Console.WriteLine("Press Enter to finish.");
        //    }
        //}

        //private static DataTable MakeTable(IEnumerable<CensusAreaData> inputData)
        //// Create a new DataTable named NewProducts.
        //{
        //    DataTable newProducts = new DataTable("NewCensusData");

        //    // Add three column objects to the table.
        //    DataColumn id = new DataColumn();
        //    id.DataType = System.Type.GetType("System.Int32");
        //    id.ColumnName = "ID";
        //    id.AutoIncrement = true;
        //    newProducts.Columns.Add(id);

        //    DataColumn yearId = new DataColumn();
        //    yearId.DataType = System.Type.GetType("System.Int32");
        //    yearId.ColumnName = "YearID";
        //    newProducts.Columns.Add(yearId);

        //    DataColumn ageId = new DataColumn();
        //    ageId.DataType = System.Type.GetType("System.Int32");
        //    ageId.ColumnName = "AgeID";
        //    newProducts.Columns.Add(ageId);

        //    DataColumn ethnicityId = new DataColumn();
        //    ethnicityId.DataType = System.Type.GetType("System.Int32");
        //    ethnicityId.ColumnName = "EthnicityID";
        //    newProducts.Columns.Add(ethnicityId);

        //    DataColumn genderId = new DataColumn();
        //    genderId.DataType = System.Type.GetType("System.Int32");
        //    genderId.ColumnName = "GenderID";
        //    newProducts.Columns.Add(genderId);

        //    DataColumn areaId = new DataColumn();
        //    areaId.DataType = System.Type.GetType("System.Int32");
        //    areaId.ColumnName = "AreaID";
        //    newProducts.Columns.Add(areaId);

        //    DataColumn count = new DataColumn();
        //    count.DataType = System.Type.GetType("System.Int32");
        //    count.ColumnName = "count";
        //    newProducts.Columns.Add(count);


        //    // Create an array for DataColumn objects.
        //    DataColumn[] keys = new DataColumn[1];
        //    keys[0] = id;
        //    newProducts.PrimaryKey = keys;


        //    foreach (var item in inputData)
        //    {
        //        DataRow row = newProducts.NewRow();
        //        row["YearID"] = item.YearId;
        //        row["AgeID"] = item.AgeId;
        //        row["EthnicityID"] = item.EthnicityId;
        //        row["GenderID"] = item.GenderId;
        //        row["AreaID"] = item.AreaId;
        //        row["count"] = item.Count;
        //        newProducts.Rows.Add(row);

        //    }

        //    newProducts.AcceptChanges();

        //    // Return the new DataTable.
        //    return newProducts;
        //}

        //private void AddRow()
        //{

        //}

        //private static string GetConnectionString()
        //// To avoid storing the connection string in your code,
        //// you can retrieve it from a configuration file.
        //{
        //    return "Server=DESKTOP-HCHKBF5\\SQLEXPRESS;Database=PopulationCensusContext;Trusted_Connection=True;MultipleActiveResultSets=true";
        //}


        //private async Task<List<CensusAreaData>> ExtractCensusDataEntitiesAsyncList(IEnumerable<string> lines)
        //{
        //    var collection = new List<CensusAreaData>();

        //    foreach (var line in lines)
        //    {
        //        var ethnicityData = line.Split(',');

        //        var year = await _unitOfWork.YearsRepository.FirstOrDefaultAsync(x => x.Code == ethnicityData[0]);
        //        var age = await _unitOfWork.AgesRepository.FirstOrDefaultAsync(x => x.Code == ethnicityData[1]);
        //        var ethnicity = await _unitOfWork.EthnicitiesRepository.FirstOrDefaultAsync(x => x.Code == ethnicityData[2]);
        //        var gender = await _unitOfWork.GendersRepository.FirstOrDefaultAsync(x => x.Code == ethnicityData[3]);
        //        var area = await _unitOfWork.AreasRepository.FirstOrDefaultAsync(x => x.Code == ethnicityData[4]);

        //        var ethnicityEntity = new CensusAreaData()
        //        {
        //            YearId = year.Id,
        //            AgeId = age.Id,
        //            EthnicityId = ethnicity.Id,
        //            GenderId = gender.Id,
        //            AreaId = area.Id
        //        };

        //        collection.Add(ethnicityEntity);
        //    }

        //    return collection;
        //}


    }
}

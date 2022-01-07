using Microsoft.AspNetCore.Mvc;
using PopulationCensus.Server.Interfaces;
using System.IO;

namespace PopulationCensus.Server.Controllers
{
    public class UploadController : ControllerBase
    {
        private readonly IImportService _importService;

        public UploadController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("upload/age-file")]
        public async Task<IActionResult> UploadAgeFile(IFormFile file)
        {
            try
            {
                await _importService.ImportAgeFileAsync(file);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/area-file")]
        public async Task<IActionResult> UploadAreaFile(IFormFile file)
        {
            try
            {
                await _importService.ImportAreaFileAsync(file);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/ethnicity-file")]
        public async Task<IActionResult> UploadEthnicityFile(IFormFile file)
        {
            try
            {
                await _importService.ImportEthnicityFileAsync(file);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/gender-file")]
        public async Task<IActionResult> UploadGenderFile(IFormFile file)
        {
            try
            {
                await _importService.ImportGenderFileAsync(file);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/year-file")]
        public async Task<IActionResult> UploadYearFile(IFormFile file)
        {
            try
            {
                await _importService.ImportYearsFileAsync(file);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/1")]
        public async Task<IActionResult> UploadLargeFile([FromBody] string values)
        {
            return StatusCode(200);
        }

        [HttpGet("upload/4")]
        public async Task<IActionResult> UploadCensusDataFile()
        {
            await _importService.ImportCensusDataFileAsync4();
            return StatusCode(200);
        }

        [HttpGet("upload/8")]
        public async Task<IActionResult> UploadCensusDataFile8()
        {
            await _importService.ImportCensusDataFileAsync8();
            return StatusCode(200);
        }


        [HttpGet("upload/long-api-call")]
        public async Task<IActionResult> LongApiCall(CancellationToken token = default(CancellationToken))
        {
            //for (var i = 0; i < 10; i++)
            //{
            //    token.ThrowIfCancellationRequested();

            //    System.Diagnostics.Debug.WriteLine(i);
            //    await Task.Delay(1000);
            //}
            //await _importService.ImportCensusDataFileAsync4(token);
            await _importService.ImportCensusDataFileAsync8(token);

            return StatusCode(200);
        }

        [HttpGet("upload/long-api-call-delay")]
        public async Task<IActionResult> LongApiCallDelay(CancellationToken token = default(CancellationToken))
        {
            await Task.Delay(10000, token);

            return StatusCode(200);
        }
    }
}

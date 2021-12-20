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
    }
}

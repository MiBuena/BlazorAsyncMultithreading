﻿using Microsoft.AspNetCore.Mvc;
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
    }
}

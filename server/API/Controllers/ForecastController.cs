using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Common.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Service.Abstractions;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;
        private readonly IAsyncRunner _asyncRunner;

        public ForecastController(IForecastService forecastService, IAsyncRunner asyncRunner)
        {
            _forecastService = forecastService;
            _asyncRunner = asyncRunner;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForecast()
        {
            return Ok(await _forecastService.GetAllForecast());
        }

        [HttpPost]
        public async Task<IActionResult> AddForecasts(IEnumerable<ForecastCommand> forecasts)
        {
            return Ok(await _forecastService.AddForecasts(forecasts));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteForecast(Guid id)
        {
            return Ok(await _forecastService.DeleteForecast(id));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            Stream stream = new MemoryStream();
            await file.CopyToAsync(stream);
            _asyncRunner.Run<IForecastService>((cis) =>
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        int row = 2;
                        var forecast = new List<ForecastCommand>();
                        while (!String.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()))
                        {
                            forecast.Add(new ForecastCommand()
                            {
                                Org = worksheet.Cells[row, 2].Text.ToString(),
                                Manager = worksheet.Cells[row, 3].Text,
                                USFocal = worksheet.Cells[row, 4].Text,
                                Project = worksheet.Cells[row, 5].Text,
                                SkillGroup = worksheet.Cells[row, 6].Text,
                                Business = worksheet.Cells[row, 7].Text,
                                Capability = worksheet.Cells[row, 8].Text,
                                Chargeline = worksheet.Cells[row, 9].Text,
                                ForecastConfidence = worksheet.Cells[row, 10].Text,
                                Comments = worksheet.Cells[row, 11].Text,
                                Jan = Convert.ToDecimal(worksheet.Cells[row, 12].Text),
                                Feb = Convert.ToDecimal(worksheet.Cells[row, 13].Text),
                                Mar = Convert.ToDecimal(worksheet.Cells[row, 14].Text),
                                Apr = Convert.ToDecimal(worksheet.Cells[row, 15].Text),
                                May = Convert.ToDecimal(worksheet.Cells[row, 16].Text),
                                June = Convert.ToDecimal(worksheet.Cells[row, 17].Text),
                                July = Convert.ToDecimal(worksheet.Cells[row, 18].Text),
                                Sep = Convert.ToDecimal(worksheet.Cells[row, 19].Text),
                                Oct = Convert.ToDecimal(worksheet.Cells[row, 20].Text),
                                Nov = Convert.ToDecimal(worksheet.Cells[row, 21].Text),
                                Dec = Convert.ToDecimal(worksheet.Cells[row, 22].Text),
                                Year = Convert.ToInt32($"20{worksheet.Cells[1, 12].Text.Split("-")[1]}"),
                            });
                            row++;
                        }
                        cis.AddForecasts(forecast).GetAwaiter();
                    }
                    
                    
                }

                catch

                {
                    // catch stuff
                    throw;
                }
            });
            // Task.Run(() => _forecastService.UploadForecast(stream, _lifetime));
            return Ok();
        }
    }
}
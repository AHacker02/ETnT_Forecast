using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Abstractions;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
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

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteForecast(Guid id)
        {
            return Ok(await _forecastService.DeleteForecast(id));
        }
    }
}
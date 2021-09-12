using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
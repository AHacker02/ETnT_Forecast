using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Common.Commands;
using Common.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/forecast")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get All Forecasts by FyYear
        /// </summary>
        /// <returns></returns>
        [HttpGet("{fyYear}")]
        public async Task<ApiResponse> GetAllForecastByFyYear(int fyYear)
        {
            var response = await _mediator.Send(new GetForecastsByFyYearQuery(fyYear));
            return response.Any()
                ? new ApiResponse(response)
                : new ApiResponse(statusCode: (int) HttpStatusCode.NoContent,
                    message: $"No Records Found for Year:{fyYear}");
        }

        /// <summary>
        ///     Add and update forecast
        /// </summary>
        /// <param name="forecasts"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> AddForecasts(AddUpdateForecastCommand forecasts)
        {
            var response = await _mediator.Send(forecasts);
            return response is int
                ? new ApiResponse(response)
                : new ApiResponse((int) HttpStatusCode.BadRequest, response);
        }

        /// <summary>
        ///     Delete forecast by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ApiResponse> DeleteForecast(Guid id)
        {
            return await _mediator.Send(new DeleteForecastCommand(id))
                ? new ApiResponse()
                : new ApiResponse((int) HttpStatusCode.NotFound, $"Forecast {id} not found");
        }

        [HttpPost("upload")]
        public async Task<ApiResponse> UploadFile([FromForm] IFormFile file)
        {
            var command = new FileUploadCommand
            {
                Id = Guid.NewGuid(),
                FileName = file.Name
            };
            await file.CopyToAsync(command.File);
            _mediator.Publish(command);

            return new ApiResponse(command.Id);
        }
        
        [HttpGet("task/{id:guid}")]
        public async Task<ApiResponse> GetTaskStatus(Guid id)
        {
            return new ApiResponse(await _mediator.Send(new GetTaskStatusQuery(id)));
        }

        [HttpGet("lookup")]
        public async Task<ApiResponse> GetAllLookupData()
        {
            return new ApiResponse(await _mediator.Send(new GetLookupQuery()));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Commands;
using Common.Models;

namespace Service.Abstractions
{
    public interface IForecastService
    {
        Task<Response<IEnumerable<ForecastViewModel>>> GetAllForecast();
        Task<Response> AddForecasts(IEnumerable<ForecastCommand> forecasts);
        Task<Response> DeleteForecast(Guid id);
    }
}
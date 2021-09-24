using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Common.Commands;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Abstractions
{
    public interface IForecastService
    {
        Task<Response<IEnumerable<ForecastViewModel>>> GetAllForecast();
        Task<Response> AddForecasts(IEnumerable<ForecastCommand> forecasts);
        Task<Response> DeleteForecast(Guid id);
        Task UploadForecast(Stream openReadStream, ILifetimeScope service);
    }
}
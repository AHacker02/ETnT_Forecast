using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Commands;
using Common.Models;
using DataAccess;
using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;

namespace Service
{
    public class ForecastService : IForecastService
    {
        private readonly ForecastRepository _repository;
        private readonly IMapper _mapper;

        public ForecastService(ForecastRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ForecastViewModel>>> GetAllForecast()
        {
            var temp = await _repository.GetAllForecasts();
            
            return new Response<IEnumerable<ForecastViewModel>>
            {
                Status = HttpStatusCode.OK,
                Data = _mapper.Map<IEnumerable<ForecastViewModel>>(
                    await _repository.GetAllForecasts()
                )
            };
        }

        public async Task<Response> AddForecasts(IEnumerable<ForecastCommand> forecasts)
        {
            foreach (var forecast in forecasts)
            {
                var forecastHeader = _repository.AddForecast(forecast.Org, forecast.Manager, forecast.USFocal,
                    forecast.Project, forecast.SkillGroup, forecast.Business, forecast.Capability, forecast.Chargeline,
                    forecast.ForecastConfidence, forecast.Comments);
                await _repository.AddForecastData(forecastHeader, forecast.Jan, forecast.Feb, forecast.Mar, forecast.Apr,
                    forecast.May, forecast.June, forecast.July, forecast.Aug, forecast.Sep, forecast.Oct, forecast.Nov,
                    forecast.Dec, forecast.Year);
            }

            return new Response()
                {Status = await _repository.SaveChanges() > 0 ? HttpStatusCode.Created : HttpStatusCode.BadRequest};
        }

        public async Task<Response> DeleteForecast(Guid id)
        {
            await _repository.DeleteForecast(id);
            return new Response()
                {Status = await _repository.SaveChanges() > 0 ? HttpStatusCode.Created : HttpStatusCode.BadRequest};
        }
    }
}
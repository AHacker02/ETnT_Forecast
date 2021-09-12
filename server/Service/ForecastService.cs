using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;

namespace Service
{
    public class ForecastService : IForecastService
    {
        private readonly ForecastContext _db;
        private readonly IMapper _mapper;

        public ForecastService(ForecastContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ForecastViewModel>>> GetAllForecast()
        {
            return new Response<IEnumerable<ForecastViewModel>>
            {
                Status = HttpStatusCode.OK,
                Data = _mapper.Map<IEnumerable<ForecastViewModel>>(
                    await _db.Forecasts.ToListAsync()
                )
            };
        }
    }
}
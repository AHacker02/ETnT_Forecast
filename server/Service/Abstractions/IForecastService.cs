using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.Models;

namespace Service.Abstractions
{
    public interface IForecastService
    {
        public Task<Response<IEnumerable<ForecastViewModel>>> GetAllForecast();
    }
}
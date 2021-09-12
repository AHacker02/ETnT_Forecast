using AutoMapper;
using Common.Models;
using DataAccess.Models;

namespace Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ForecastViewModel, Forecast>().ReverseMap();
        }
    }
}
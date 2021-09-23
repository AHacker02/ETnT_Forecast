using AutoMapper;
using Common.DbSets;
using Common.Models;

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
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Models;
using DataAccess.DbSets;

namespace Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<IEnumerable<Forecast>, IEnumerable<ForecastViewModel>>()
                .ConvertUsing<ForecastFlatMap>();

            CreateMap<TaskStatusViewModel, EventData>()
                .ReverseMap();
        }
    }

    public class ForecastFlatMap : ITypeConverter<IEnumerable<Forecast>, IEnumerable<ForecastViewModel>>
    {
        public IEnumerable<ForecastViewModel> Convert(IEnumerable<Forecast> source,
            IEnumerable<ForecastViewModel> destination, ResolutionContext context)
        {
            return source.SelectMany(s => s.ForecastData.Select(o => new ForecastViewModel
            {
                Id = s.Id,
                Org = s.Org.Value,
                Manager = s.Manager.FullName,
                USFocal = s.USFocal.FullName,
                Project = s.Project.Value,
                SkillGroup = s.SkillGroup.Value,
                Business = s.Business.Value,
                Capability = s.Capability.Value,
                ForecastConfidence = s.ForecastConfidence.Value,
                Chargeline = s.Chargeline,
                Comments = s.Comments,
                Jan = o.Jan,
                Feb = o.Feb,
                Mar = o.Mar,
                Apr = o.Apr,
                May = o.May,
                June = o.June,
                July = o.July,
                Aug = o.Aug,
                Oct = o.Oct,
                Sep = o.Sep,
                Nov = o.Nov,
                Dec = o.Dec
            })).ToList();
        }
    }
}
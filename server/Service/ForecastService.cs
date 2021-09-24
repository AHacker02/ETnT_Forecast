using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Lifetime;
using AutoMapper;
using Common.Commands;
using Common.Models;
using DataAccess;
using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
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
            try
            {
                foreach (var forecast in forecasts)
                {
                    var forecastHeader = _repository.AddForecast(forecast.Org, forecast.Manager, forecast.USFocal,
                        forecast.Project, forecast.SkillGroup, forecast.Business, forecast.Capability,
                        forecast.Chargeline,
                        forecast.ForecastConfidence, forecast.Comments);
                    await _repository.AddForecastData(forecastHeader, forecast.Jan, forecast.Feb, forecast.Mar,
                        forecast.Apr,
                        forecast.May, forecast.June, forecast.July, forecast.Aug, forecast.Sep, forecast.Oct,
                        forecast.Nov,
                        forecast.Dec, forecast.Year);
                }

                return new Response()
                    {Status = await _repository.SaveChanges() > 0 ? HttpStatusCode.Created : HttpStatusCode.BadRequest};
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Response> DeleteForecast(Guid id)
        {
            await _repository.DeleteForecast(id);
            return new Response()
                {Status = await _repository.SaveChanges() > 0 ? HttpStatusCode.Created : HttpStatusCode.BadRequest};
        }

        public async Task UploadForecast(Stream file, ILifetimeScope serviceScopeFactory)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int row = 2;
                    var forecast = new List<ForecastCommand>();
                    while (!String.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()))
                    {
                        forecast.Add(new ForecastCommand()
                        {
                            Org = worksheet.Cells[row, 2].Text.ToString(),
                            Manager = worksheet.Cells[row, 3].Text,
                            USFocal = worksheet.Cells[row, 4].Text,
                            Project = worksheet.Cells[row, 5].Text,
                            SkillGroup = worksheet.Cells[row, 6].Text,
                            Business = worksheet.Cells[row, 7].Text,
                            Capability = worksheet.Cells[row, 8].Text,
                            Chargeline = worksheet.Cells[row, 9].Text,
                            ForecastConfidence = worksheet.Cells[row, 10].Text,
                            Comments = worksheet.Cells[row, 11].Text,
                            Jan = Convert.ToDecimal(worksheet.Cells[row, 12].Text),
                            Feb = Convert.ToDecimal(worksheet.Cells[row, 13].Text),
                            Mar = Convert.ToDecimal(worksheet.Cells[row, 14].Text),
                            Apr = Convert.ToDecimal(worksheet.Cells[row, 15].Text),
                            May = Convert.ToDecimal(worksheet.Cells[row, 16].Text),
                            June = Convert.ToDecimal(worksheet.Cells[row, 17].Text),
                            July = Convert.ToDecimal(worksheet.Cells[row, 18].Text),
                            Sep = Convert.ToDecimal(worksheet.Cells[row, 19].Text),
                            Oct = Convert.ToDecimal(worksheet.Cells[row, 20].Text),
                            Nov = Convert.ToDecimal(worksheet.Cells[row, 21].Text),
                            Dec = Convert.ToDecimal(worksheet.Cells[row, 22].Text),
                            Year = Convert.ToInt32($"20{worksheet.Cells[1, 12].Text.Split("-")[1]}"),
                        });
                        row++;
                    }

                    using (var lifetimeScope = serviceScopeFactory.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
                    {
                        var service = lifetimeScope.Resolve<ForecastService>();

                        await service.AddForecasts(forecast);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
    
}
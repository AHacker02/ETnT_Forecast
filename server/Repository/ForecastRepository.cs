using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ForecastRepository
    {
        private readonly ForecastContext _db;

        public ForecastRepository(ForecastContext db)
        {
            _db = db;
        }

        public Forecast AddForecast(string orgName, string managerName, string usFocalName, string projectName,
            string skillName, string businessUnitName, string capabilityName, string chargeline,
            string forecastConfidenceValue, string comments)
        {
            var org = _db.Orgs.FirstOrDefault(x => x.Value == orgName);
            var manager = _db.Users.AsEnumerable().FirstOrDefault(x => $"{x.FirstName} {x.LastName}" == managerName);
            var usFocal = _db.Users.AsEnumerable().FirstOrDefault(x => x.FullName == usFocalName);
            var project = _db.Projects.FirstOrDefault(x => x.Value == projectName);
            var skill = _db.Skills.FirstOrDefault(x => x.Value == skillName);
            var businessUnit = _db.Businesses.FirstOrDefault(x => x.Value == businessUnitName);
            var capability = _db.Capabilities.FirstOrDefault(x => x.Value == capabilityName);
            var forecastConfidence = _db.Categories.FirstOrDefault(x => x.Value == forecastConfidenceValue);
            var forecast = _db.Forecasts.FirstOrDefault(x =>
                x.Org == org
                && x.Project == project
                && x.SkillGroup == skill
                && x.Business == businessUnit
                && x.Capability == capability
            );
            if (forecast == null)
            {
                forecast = new Forecast(org, manager, usFocal, project, skill, businessUnit, capability, chargeline,
                    forecastConfidence, comments);
                _db.Forecasts.Add(forecast);
            }
            else
            {
                forecast.Manager = manager;
                forecast.USFocal = usFocal;
                forecast.Chargeline = chargeline;
                forecast.ForecastConfidence = forecastConfidence;
                forecast.Comments = comments;
            }

            return forecast;
        }

        public async Task<IEnumerable<Forecast>> GetAllForecasts()
            => await _db.Forecasts
                .Include(m => m.Business)
                .Include(m=>m.Capability)
                .Include(m=>m.ForecastConfidence)
                .Include(m=>m.Manager)
                .Include(m=>m.USFocal)
                .Include(m=>m.ForecastData)
                .Include(m=>m.Project)
                .Include(m=>m.SkillGroup)
                .Include(m=>m.Org)
                .Include(m=>m.ForecastData)
                .ToListAsync();

        public async Task<ForecastData> AddForecastData(
            Forecast forecast
            , decimal jan
            , decimal feb
            , decimal mar
            , decimal apr
            , decimal may
            , decimal june
            , decimal july
            , decimal aug
            , decimal sep
            , decimal oct
            , decimal nov
            , decimal dec
            , int year)
        {
            var forecastData = _db.ForecastData.FirstOrDefault(x => x.Forecast == forecast && x.Year == year);
            if (forecastData != null)
            {
                _db.ForecastData.Remove(forecastData);
            }
            forecastData = new ForecastData(
                forecast,
                jan,
                feb,
                mar, apr, may, june, july, aug, sep, oct, nov, dec, year
            );
            await _db.ForecastData.AddAsync(forecastData);
            return forecastData;
        }

        public async Task<int> SaveChanges()
            => await _db.SaveChangesAsync();

        public async Task DeleteForecast(Guid id)
            => _db.Forecasts.Remove(await _db.Forecasts.FirstOrDefaultAsync(x => x.Id == id));
    }
}
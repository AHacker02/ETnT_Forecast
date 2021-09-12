using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ForecastContext:DbContext
    {
        public DbSet<Forecast> Forecasts { get; set; }
        
        public ForecastContext(DbContextOptions<ForecastContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
namespace DataAccess.DbSets
{
    public class ForecastData : BaseEntity
    {
        public ForecastData()
        {
        }

        public ForecastData(Forecast forecast, decimal jan, decimal feb, decimal mar, decimal apr, decimal may,
            decimal june, decimal july, decimal aug, decimal sep, decimal oct, decimal nov, decimal dec, int year)
        {
            Forecast = forecast;
            Jan = jan;
            Feb = feb;
            Mar = mar;
            Apr = apr;
            May = may;
            June = june;
            July = july;
            Aug = aug;
            Sep = sep;
            Oct = oct;
            Nov = nov;
            Dec = dec;
            Year = year;
        }

        public Forecast Forecast { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public int Year { get; set; }
    }
}
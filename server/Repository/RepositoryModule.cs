using Autofac;

namespace DataAccess
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForecastContext>();
            builder.RegisterType<Seeder>();
            builder.RegisterType<ForecastRepository>();
        }
    }
}
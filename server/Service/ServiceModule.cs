using Autofac;
using DataAccess;
using Service.Abstractions;

namespace Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType<ForecastService>().As<IForecastService>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using ParkingRight.Domain.SNS;

namespace ParkingRight.Domain
{
    public static class DomainModule
    {
        public static void RegisterDomainModule(this IServiceCollection services)
        {
            services.AddTransient<ISnsConnector, SnsConnector>();
            services.AddSingleton<IConfigurationProvider, ConfigurationProvider>();
            services.AddTransient<IParkingRightProcessor, ParkingRightProcessor>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace ParkingRight.Domain
{
    public static class DomainModule
    {
        public static void RegisterTo(IServiceCollection services)
        {
            services.AddTransient<IParkingRightProcessor, ParkingRightProcessor>();
        }
    }
}
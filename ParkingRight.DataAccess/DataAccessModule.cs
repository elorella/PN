using Microsoft.Extensions.DependencyInjection;
using ParkingRight.DataAccess.Repositories;

namespace ParkingRight.DataAccess
{
    public static class DataAccessModule
    {
        public static void RegisterTo(IServiceCollection services)
        {
            services.AddTransient<IParkingRightRepository, ParkingRightRepository>();
        }
    }
}
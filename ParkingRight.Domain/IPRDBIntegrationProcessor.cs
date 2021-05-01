using System.Threading.Tasks;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain
{
    public interface IPrdbIntegrationProcessor
    {
        Task<int?> Register(ParkingRegistration request);
    }
}
using System.Threading.Tasks;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain
{
    public interface IParkingRightProcessor
    {
        Task<ParkingRightModel> GetParkingRight(string parkingRightKey);
        Task<ParkingRightModel> SaveParkingRight(ParkingRightModel parkingRight);
    }
}
using System.Threading.Tasks;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain
{
    public interface IParkingRightProcessor
    {
        Task<ApiServiceResult<ParkingRightDto>> GetParkingRight(string parkingRightKey);
        Task<ApiServiceResult<string>> SaveParkingRight(ParkingRightInsertRequest request);

        Task<ApiServiceResult<bool>> DeleteParkingRight(string parkingRightKey);
    }
}
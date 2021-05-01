using System.Threading.Tasks;
using ParkingRight.DataAccess.Entities;

namespace ParkingRight.DataAccess.Repositories
{
    public interface IParkingRightRepository
    {
        Task<ParkingRightEntity> Get(string key);

        Task<bool> Add(ParkingRightEntity parkingRightEntity);
    }
}
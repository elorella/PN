using System.Threading.Tasks;
using ParkingRight.DataAccess.Entities;

namespace ParkingRight.DataAccess.Repositories
{
    public interface IParkingRightRepository
    {
        Task<ParkingRightEntity> Get(string key);

        Task Add(ParkingRightEntity parkingRightEntity);
        //Task<IEnumerable<ParkingRightEntity>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
//        Task UpdateMovie(ParkingRightEntity request);

        //      Task<IEnumerable<ParkingRightEntity>> GetMovieRank(string movieName);
    }
}
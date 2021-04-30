using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ParkingRight.DataAccess.Entities;

namespace ParkingRight.DataAccess.Repositories
{
    public class ParkingRightRepository : IParkingRightRepository
    {
        private readonly DynamoDBContext _context;

        public ParkingRightRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<ParkingRightEntity> Get(string parkingRightKey)
        {
            return await _context.LoadAsync<ParkingRightEntity>(parkingRightKey);
        }

        //public async Task<IEnumerable<ParkingRightEntity>> GetUsersRankedMoviesByMovieTitle(int userId,
        //    string movieName)
        //{
        //    var config = new DynamoDBOperationConfig
        //    {
        //        QueryFilter = new List<ScanCondition>
        //        {
        //            new ScanCondition("MovieName", ScanOperator.BeginsWith, movieName)
        //        }
        //    };

        //    return await _context.QueryAsync<ParkingRightEntity>(userId, config).GetRemainingAsync();
        //}

        public async Task Add(ParkingRightEntity entity)
        {
            await _context.SaveAsync(entity);
        }

        public async Task<IEnumerable<ParkingRightEntity>> GetAllItems()
        {
            return await _context.ScanAsync<ParkingRightEntity>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task UpdateMovie(ParkingRightEntity request)
        {
            await _context.SaveAsync(request);
        }

        public async Task<IEnumerable<ParkingRightEntity>> GetMovieRank(string movieName)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "MovieName-index"
            };

            return await _context.QueryAsync<ParkingRightEntity>(movieName, config).GetRemainingAsync();
        }
    }
}
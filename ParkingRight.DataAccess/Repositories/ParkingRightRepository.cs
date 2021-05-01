using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Logging;
using ParkingRight.DataAccess.Entities;

namespace ParkingRight.DataAccess.Repositories
{
    public class ParkingRightRepository : IParkingRightRepository
    {
        private readonly DynamoDBContext _context;
        private readonly ILogger _logger;

        public ParkingRightRepository(IAmazonDynamoDB dynamoDbClient, ILogger<ParkingRightRepository> logger)
        {
            _logger = logger;
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<ParkingRightEntity> Get(string parkingRightKey)
        {
            return await _context.LoadAsync<ParkingRightEntity>(parkingRightKey);
        }

        public async Task<bool> Add(ParkingRightEntity entity)
        {
            try
            {
                await _context.SaveAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, e, "ParkingRightEntity couldn't be saved.");
            }

            return false;
        }
    }
}
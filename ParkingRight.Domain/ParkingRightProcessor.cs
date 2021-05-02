using System;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ParkingRight.DataAccess.Entities;
using ParkingRight.DataAccess.Repositories;
using ParkingRight.Domain.Models;
using ParkingRight.Domain.SNS;

namespace ParkingRight.Domain
{
    public class ParkingRightProcessor : IParkingRightProcessor
    {
        private readonly IMapper _mapper;
        private readonly IParkingRightRepository _parkingRightRepository;
        private readonly ISnsConnector _snsConnector;
        private readonly IConfigurationProvider _configurationProvider;

        public ParkingRightProcessor(IParkingRightRepository parkingRightRepository,
            IMapper mapper,
            ISnsConnector snsConnector, IConfigurationProvider configurationProvider)
        {
            _parkingRightRepository = parkingRightRepository;
            _mapper = mapper;
            _snsConnector = snsConnector;
            _configurationProvider = configurationProvider;
        }

        public async Task<ParkingRightModel> GetParkingRight(string parkingRightKey)
        {
            var parkingRightEntity = await _parkingRightRepository.Get(parkingRightKey);
            return _mapper.Map<ParkingRightModel>(parkingRightEntity);
        }

         
        public async Task<ParkingRightModel> SaveParkingRight(ParkingRightModel parkingRight)
        {
            var entity = _mapper.Map<ParkingRightEntity>(parkingRight);
            var isAdded = await _parkingRightRepository.Add(entity);
            if (!isAdded)
                // retry to save the record. 
                throw new Exception("Failed to create a parking right.");

            parkingRight.ParkingRightKey = entity.ParkingRightKey;

            var payload = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(parkingRight));
            var isPublished = await _snsConnector.PublishMessage(parkingRight.ParkingRightKey,
                MessageType.ParkingRegistrationRequest,
                payload, _configurationProvider.RegistrationTopicArn);

            if (!isPublished)
            {
                throw new Exception("Failed to publish sns message!");
            }

            return parkingRight;
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using ParkingRight.DataAccess.Entities;
using ParkingRight.DataAccess.Repositories;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain
{
    public class ParkingRightProcessor : IParkingRightProcessor
    {
        private readonly IMapper _mapper;
        private readonly IParkingRightRepository _parkingRightRepository;
        private readonly IPrdbIntegrationProcessor _prdbIntegrationProcessor;

        public ParkingRightProcessor(IParkingRightRepository parkingRightRepository, IMapper mapper,
            IPrdbIntegrationProcessor prdbIntegrationProcessor)
        {
            _parkingRightRepository = parkingRightRepository;
            _mapper = mapper;
            _prdbIntegrationProcessor = prdbIntegrationProcessor;
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
            {
                // retry to save the record. 
                throw new Exception("Failed to create a parking right.");
            }

            // Instead of Http, communication can be done over SQS; see architectural diagram 
            var registrationRequest = _mapper.Map<ParkingRegistration>(parkingRight);
            var registrationId = await _prdbIntegrationProcessor.Register(registrationRequest);
            if (!registrationId.HasValue)
            {
                throw new Exception("Failed to register parking spot to PRDB.");
            }

           
            parkingRight.ParkingRightKey = entity.ParkingRightKey;
            return parkingRight;
        }
    }
}
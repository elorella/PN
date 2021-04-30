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

        public async Task<ApiServiceResult<ParkingRightDto>> GetParkingRight(string parkingRightKey)
        {
            var parkingRightEntity = await _parkingRightRepository.Get(parkingRightKey);
            return new ApiServiceResult<ParkingRightDto>(_mapper.Map<ParkingRightDto>(parkingRightEntity),
                ResponseCodes.Success);
        }

        public async Task<ApiServiceResult<string>> SaveParkingRight(ParkingRightInsertRequest request)
        {
            var registrationRequest = _mapper.Map<ParkingRegistrationRequest>(request);
            var registrationId = await _prdbIntegrationProcessor.Register(registrationRequest);
            if (!registrationId.HasValue)
                return new ApiServiceResult<string>(string.Empty, ResponseCodes.RegistrationFailed);

            var entity = _mapper.Map<ParkingRightEntity>(request);
            await _parkingRightRepository.Add(entity);

            // Publish notification 

            return ApiServiceResult<string>.Success(entity.ParkingRightKey);
        }

        public async Task<ApiServiceResult<bool>> DeleteParkingRight(string parkingRightKey)
        {
            throw new NotImplementedException();
        }
    }
}
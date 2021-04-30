using ParkingRight.DataAccess.Entities;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain.Profile
{
    public class ParkingRightProfile : AutoMapper.Profile
    {
        public ParkingRightProfile()
        {
            CreateMap<ParkingRightEntity, ParkingRightDto>().ReverseMap();

            CreateMap<ParkingRightInsertRequest, ParkingRegistrationRequest>();

            CreateMap<ParkingRightInsertRequest, ParkingRightEntity>();

            
        }
    }
}
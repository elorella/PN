using ParkingRight.DataAccess.Entities;
using ParkingRight.Domain.Models;

namespace ParkingRight.Domain.Profile
{
    public class ParkingRightProfile : AutoMapper.Profile
    {
        public ParkingRightProfile()
        {
            CreateMap<ParkingRightEntity, ParkingRightModel>();

            CreateMap<ParkingRightModel, ParkingRightEntity>()
                .ForMember(dest => dest.ParkingRightKey,
                    (opt) =>
                    {
                        opt.MapFrom(src => string.Concat(src.LicensePlate, src.OperatorId, src.CustomerProfile));
                    });

           
        }
    }
}
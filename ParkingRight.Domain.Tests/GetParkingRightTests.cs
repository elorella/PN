using System;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core;
using Moq;
using Newtonsoft.Json;
using ParkingRight.DataAccess.Entities;
using ParkingRight.DataAccess.Repositories;
using ParkingRight.Domain.Models;
using ParkingRight.Domain.Profile;
using Xunit;

namespace ParkingRight.Domain.Tests
{
    public class GetParkingRightTests
    {
        [Fact]
        public async Task GetParkingRighShouldBeReturned()
        {
            var key = "key";
            var entity = new ParkingRightEntity()
            {
                AmountPaid = 100,
                CustomerProfileType = (int) CustomerProfile.Resident,
                EndDate = DateTime.Now.AddDays(2),
                LicensePlate = "123",
                OperatorId = 3,
                ParkingRightKey = key,
                ParkingZoneId = 4,
                StartDate = DateTime.Now
            };
            var repo = new Mock<IParkingRightRepository>();
            repo.Setup(m=>m.Get(key))
                .Returns(() => Task.FromResult(entity));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ParkingRightProfile>());
            var mapper = config.CreateMapper();
            var responseExpected = mapper.Map<ParkingRightModel>(entity);

            var parkingRightProcessor = new ParkingRightProcessor(repo.Object, mapper, new Mock<IPrdbIntegrationProcessor>().Object);

            var response = await parkingRightProcessor.GetParkingRight(key);
            
            var responseExpectedJson = JsonConvert.SerializeObject(responseExpected);
            var responseJson =  JsonConvert.SerializeObject(response);

            Assert.Equal(responseExpectedJson, responseJson);


        }
    }
}
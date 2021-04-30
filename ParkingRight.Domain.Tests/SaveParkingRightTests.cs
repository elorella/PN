using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ParkingRight.DataAccess.Entities;
using ParkingRight.DataAccess.Repositories;
using ParkingRight.Domain.Models;
using ParkingRight.Domain.Profile;
using Xunit;

namespace ParkingRight.Domain.Tests
{
    public class SaveParkingRightTests
    {
        [Fact(DisplayName = "Save parking right should be triggered with the repo-save function once.")]
        public async Task SaveParkingRightShouldTriggerSaveRepo()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ParkingRightProfile>());
            var mapper = config.CreateMapper();

            var repo = new Mock<IParkingRightRepository>();
            var integrationProcess = new Mock<IPrdbIntegrationProcessor>();

            int? registrationResponse = 5;
            integrationProcess.Setup(m => m.Register(It.IsAny<ParkingRegistrationRequest>()))
                .Returns(() => Task.FromResult(registrationResponse));

            var parkingRightProcessor = new ParkingRightProcessor(repo.Object, mapper, integrationProcess.Object);

            var request = new Mock<ParkingRightInsertRequest>();
            await parkingRightProcessor.SaveParkingRight(request.Object);

            repo.Verify(r => r.Add(It.IsAny<ParkingRightEntity>()), Times.Once);
        }
    }
}
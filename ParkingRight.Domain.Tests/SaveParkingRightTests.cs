using System;
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

            repo.Setup(m => m.Add(It.IsAny<ParkingRightEntity>()))
                .Returns(() => Task.FromResult(true));

            int? registrationResponse = 5;
            integrationProcess.Setup(m => m.Register(It.IsAny<ParkingRegistration>()))
                .Returns(() => Task.FromResult(registrationResponse));

            var parkingRightProcessor = new ParkingRightProcessor(repo.Object, mapper, integrationProcess.Object);


            await parkingRightProcessor.SaveParkingRight(new ParkingRightModel());

            repo.Verify(r => r.Add(It.IsAny<ParkingRightEntity>()), Times.Once);
        }

        [Fact(DisplayName = "PRDBIntegration shouldn't be triggered if repository fails to save parking right.")]
        public async Task SaveParkingRightShouldntBeTriggered()
        {
            var repo = new Mock<IParkingRightRepository>();
            var integrationProcess = new Mock<IPrdbIntegrationProcessor>();

            repo.Setup(m => m.Add(It.IsAny<ParkingRightEntity>()))
                .Returns(() => Task.FromResult(false));

            var parkingRightProcessor =
                new ParkingRightProcessor(repo.Object, new Mock<IMapper>().Object, integrationProcess.Object);

            await Assert.ThrowsAsync<Exception>(async () =>
                await parkingRightProcessor.SaveParkingRight(new ParkingRightModel()));

            integrationProcess.Verify(r => r.Register(It.IsAny<ParkingRegistration>()), Times.Never);
        }
    }
}
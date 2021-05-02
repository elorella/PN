using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ParkingRight.DataAccess.Entities;
using ParkingRight.DataAccess.Repositories;
using ParkingRight.Domain.Models;
using ParkingRight.Domain.Profile;
using ParkingRight.Domain.SNS;
using Xunit;

namespace ParkingRight.Domain.Tests
{
    public class SaveParkingRightTests
    {
        private readonly IMapper _mapper;

        public SaveParkingRightTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ParkingRightProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact(DisplayName = "Save parking right should be triggered with the repo-save function once.")]
        public async Task SaveParkingRightShouldTriggerSaveRepo()
        {
            var repo = new Mock<IParkingRightRepository>();
            var sns = new Mock<ISnsConnector>();

            repo.Setup(m => m.Add(It.IsAny<ParkingRightEntity>()))
                .Returns(() => Task.FromResult(true));

            sns.Setup(m => m.PublishMessage(
                    It.IsAny<string>(), 
                    It.IsAny<MessageType>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(() => Task.FromResult(true));

            var parkingRightProcessor = new ParkingRightProcessor(repo.Object, _mapper, sns.Object,new Mock<IConfigurationProvider>().Object);
            await parkingRightProcessor.SaveParkingRight(new ParkingRightModel());

            repo.Verify(r => r.Add(It.IsAny<ParkingRightEntity>()), Times.Once);
        }

        [Fact(DisplayName = "Sns message shouldn't be published if repository fails to save parking right.")]
        public async Task SaveParkingRightShouldntBeTriggered()
        {
            var repo = new Mock<IParkingRightRepository>();
            var sns = new Mock<ISnsConnector>();

            repo.Setup(m => m.Add(It.IsAny<ParkingRightEntity>()))
                .Returns(() => Task.FromResult(false));

            var parkingRightProcessor = new ParkingRightProcessor(repo.Object, _mapper, sns.Object, new Mock<IConfigurationProvider>().Object);


            await Assert.ThrowsAsync<Exception>(async () =>
                await parkingRightProcessor.SaveParkingRight(new ParkingRightModel()));

            sns.Verify(r => r.PublishMessage(It.IsAny<string>(),
                It.IsAny<MessageType>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
        }
    }
}
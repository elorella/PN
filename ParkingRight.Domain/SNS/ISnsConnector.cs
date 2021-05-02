using System.Threading.Tasks;

namespace ParkingRight.Domain.SNS
{
    public interface ISnsConnector
    {
        Task<bool> PublishMessage(string messageId, MessageType messageType, string messagePayload, string topicArn);
    }
}
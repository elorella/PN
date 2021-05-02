using System.Threading.Tasks;

namespace ParkingRight.WebApi.SNS
{
    public interface ISnsConnector
    {
        Task PublishMessage(string messageId, MessageType messageType, string messagePayload, string topicArn);
    }
}
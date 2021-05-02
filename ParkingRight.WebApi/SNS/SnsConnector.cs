using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;

namespace ParkingRight.WebApi.SNS
{
    public class SnsConnector : ISnsConnector
    {
        private readonly ILogger _logger;
        private readonly IAmazonSimpleNotificationService _snsClient;

        public SnsConnector(IAmazonSimpleNotificationService snsClient, ILogger logger)
        {
            _snsClient = snsClient;
            _logger = logger;
        }

        public async Task PublishMessage(string messageId, MessageType messageType, string messagePayload,
            string topicArn)
        {
            _logger.LogInformation($"MessageId : {messageId} will be published.");

            var publishRequest = PublishRequest(messageId, messageType, messagePayload, topicArn);

            var attributesString = string.Join(",",
                publishRequest.MessageAttributes.Select(s => $"\"{s.Key}\":\"{s.Value?.StringValue}\""));
            _logger.LogInformation(
                $"Sending payload '{publishRequest.Message}' attributes '{{{attributesString}}}' to SNS for MQ.");
            await _snsClient.PublishAsync(publishRequest);
        }

        private static PublishRequest PublishRequest(string messageId, MessageType messageType, string messagePayload,
            string topicArn)
        {
            var messageTransferModel = new MessageTransferModel
            {
                MessageType = messageType,
                MessageId = messageId,
                Content = messagePayload
            };

            var messageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {MessageAttributes.MessageType, ToStringAttribute(ToAttributeValue(messageType))},
                {MessageAttributes.MessageModelVersion, ToStringAttribute(messageTransferModel.MessageModelVersion)}
            };

            var publishRequest = new PublishRequest
            {
                Message = JsonSerializer.Serialize(messageTransferModel),
                TopicArn = topicArn,
                MessageAttributes = messageAttributes
            };
            return publishRequest;
        }


        private static MessageAttributeValue ToStringAttribute(string attributeValue)
        {
            return new MessageAttributeValue
                {DataType = "String", StringValue = attributeValue};
        }

        private static string ToAttributeValue<T>(T value)
        {
            return value?.ToString().ToLower();
        }
    }
}
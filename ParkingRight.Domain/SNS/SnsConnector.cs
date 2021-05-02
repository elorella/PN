using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;

// Could have been moved to its own solution and be shared with other services.
namespace ParkingRight.Domain.SNS
{
    public class SnsConnector : ISnsConnector
    {
        private readonly ILogger _logger;
        private readonly IAmazonSimpleNotificationService _snsClient;

        public SnsConnector(IAmazonSimpleNotificationService snsClient, ILogger<SnsConnector> logger)
        {
            _snsClient = snsClient;
            _logger = logger;
        }

        public async Task<bool> PublishMessage(string messageId, MessageType messageType, string messagePayload,
            string topicArn)
        {
            try
            {
                _logger.LogInformation($"MessageId : {messageId} will be published.");
                var publishRequest = PublishRequest(messageId, messageType, messagePayload, topicArn);
                await _snsClient.PublishAsync(publishRequest);
                _logger.LogInformation($"MessageId : {messageId} is published.");
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Critical, e, $"MessageId : {messageId} couldn't be published.");
            }

            return false;
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
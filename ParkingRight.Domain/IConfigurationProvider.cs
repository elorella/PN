using System;

namespace ParkingRight.Domain
{
    public interface IConfigurationProvider
    {
        string RegistrationTopicArn { get; }
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public string RegistrationTopicArn { get; } = Environment.GetEnvironmentVariable("registration_topic") == null? "arn:aws:sns:eu-central-1:874134515578:ParkNow": Environment.GetEnvironmentVariable("registration_topic");

    }
}
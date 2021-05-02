using System;

namespace ParkingRight.Domain
{
    public interface IConfigurationProvider
    {
        string RegistrationTopicArn { get; }
    }

    public class ConfigurationProvider : IConfigurationProvider
    {
        public string RegistrationTopicArn { get; } = Environment.GetEnvironmentVariable("registration_topic");

    }
}
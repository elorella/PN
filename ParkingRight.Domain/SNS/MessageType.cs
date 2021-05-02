using System.ComponentModel;

namespace ParkingRight.Domain.SNS
{
    public enum MessageType
    {
        None,
        [Description("park_registration")] ParkingRegistrationRequest
    }
}
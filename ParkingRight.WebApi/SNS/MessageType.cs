using System.ComponentModel;

namespace ParkingRight.WebApi.SNS
{
    public enum MessageType
    {
        None,
        [Description("park_registration")] ParkingRegistrationRequest
    }
}
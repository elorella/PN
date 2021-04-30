using System;

namespace ParkingRight.Domain.Models
{
    public class ParkingRegistrationRequest
    {
        public string LicensePlate { get; set; }
        public int OperatorId { get; set; }
        public int ParkingZoneId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal AmountPaid { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }
}
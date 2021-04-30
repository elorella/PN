using System;

namespace ParkingRight.Contracts
{
    public class ParkingRightModel
    {
        public string LicensePlate { get; set; }
        public int OperatorId { get; set; }
        public int ParkingZoneId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal AmountPaid { get; set; }
        public CustomerProfileType CustomerProfileType { get; set; }
        public bool IsActive { get; set; }
    }
}
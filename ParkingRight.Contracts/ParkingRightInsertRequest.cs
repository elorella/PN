using System;

namespace ParkingRight.Contracts
{
    public class ParkingRightInsertRequest
    {
        public string LicensePlate { get; set; }
        public int OperatorId { get; set; }
        public int ParkingZoneId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal AmountPaid { get; set; }
        public int CustomerProfileType { get; set; }
    }
}

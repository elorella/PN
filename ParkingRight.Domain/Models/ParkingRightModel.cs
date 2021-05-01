using System;

namespace ParkingRight.Domain.Models
{
    public class ParkingRightModel
    {
        public string ParkingRightKey { get; set; }
        public string LicensePlate { get; set; }
        public int OperatorId { get; set; }
        public int ParkingZoneId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal AmountPaid { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }
}
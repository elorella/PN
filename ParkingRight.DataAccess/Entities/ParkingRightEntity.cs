using System;
using Amazon.DynamoDBv2.DataModel;

namespace ParkingRight.DataAccess.Entities
{
    [DynamoDBTable("ParkingRights")]
    public class ParkingRightEntity
    {
        [DynamoDBHashKey]
        public string ParkingRightKey =>
            string.Concat(LicensePlate, ParkingZoneId.ToString(), CustomerProfileType.ToString());

        [DynamoDBGlobalSecondaryIndexHashKey] public string LicensePlate { get; set; }

        public int OperatorId { get; set; }
        public int ParkingZoneId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal AmountPaid { get; set; }
        public int CustomerProfileType { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
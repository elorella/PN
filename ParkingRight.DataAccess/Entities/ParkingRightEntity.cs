using System;
using Amazon.DynamoDBv2.DataModel;

namespace ParkingRight.DataAccess.Entities
{
    [DynamoDBTable("ParkingRights")]
    public class ParkingRightEntity
    {
        [DynamoDBHashKey("ParkingRightKey")] public string ParkingRightKey { get; set; }
        [DynamoDBProperty("LicensePlate")] public string LicensePlate { get; set; }

        [DynamoDBProperty] public int OperatorId { get; set; }
        [DynamoDBProperty] public int ParkingZoneId { get; set; }
        [DynamoDBProperty] public DateTime StartDate { get; set; }
        [DynamoDBProperty] public DateTime EndDate { get; set; }
        [DynamoDBProperty] public decimal AmountPaid { get; set; }

        [DynamoDBProperty] public int CustomerProfileType { get; set; }
        //public bool IsActive { get; set; }
        //public DateTime CreateDate=> DateTime.Now;
    }
}
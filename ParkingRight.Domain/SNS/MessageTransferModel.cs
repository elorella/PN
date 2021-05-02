namespace ParkingRight.Domain.SNS
{
    public class MessageTransferModel
    {
        public string MessageId { get; set; }

        public MessageType MessageType { get; set; }

        public string MessageModelVersion => "1.0";
        public string Content { get; set; }
    }
}
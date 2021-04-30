namespace ParkingRight.Domain.Models
{
    public class ApiServiceResult<T>
    {
        public ApiServiceResult(T t, ResponseCodes response)
        {
            Value = t;
            ResponseCode = response;
        }

        public ResponseCodes ResponseCode { get; }

        public T Value { get; }

        public bool IsSuccess => ResponseCode == ResponseCodes.Success;

        public static ApiServiceResult<T> Success(T t)
        {
            return new ApiServiceResult<T>(t, ResponseCodes.Success);
        }
    }
}
namespace CourierServiceApp.Application.Exceptions
{
    public class DeliveryScheduleException : AppException
    {
        public DeliveryScheduleException(string message) : base(message) { }
        public DeliveryScheduleException(string message, Exception inner) : base(message, inner) { }
    }
}

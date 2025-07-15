namespace CourierServiceApp.Application.Exceptions
{
    public class InvalidPackageException : AppException
    {
        public InvalidPackageException(string message) : base(message) { }
    }
}

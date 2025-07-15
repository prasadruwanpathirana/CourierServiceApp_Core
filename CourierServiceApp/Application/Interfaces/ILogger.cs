using System;

namespace CourierServiceApp.Application.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void LogError(Exception ex);
    }
}

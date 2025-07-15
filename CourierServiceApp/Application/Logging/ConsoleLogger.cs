using CourierServiceApp.Application.Interfaces;
using System;

namespace CourierServiceApp.Application.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        public void LogError(Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}\n{ex.StackTrace}");
        }
    }
}

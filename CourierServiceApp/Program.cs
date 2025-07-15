using CourierServiceApp.Application;
using CourierServiceApp.Application.Interfaces;
using CourierServiceApp.Application.Logging;
using CourierServiceApp.Infrastructure;
using CourierServiceApp.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace CourierServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var app = serviceProvider.GetRequiredService<ConsoleUI>();
            app.Run();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddTransient<IDeliverySchedulerFactory, DeliverySchedulerFactory>();
            services.AddTransient<DeliveryServiceFactory>();
            services.AddTransient<ConsoleUI>();
            return services.BuildServiceProvider();
        }
    }

    public interface IDeliverySchedulerFactory
    {
        IDeliveryScheduler Create(int vehicleCount, double speed, double maxWeight);
    }

    public class DeliverySchedulerFactory : IDeliverySchedulerFactory
    {
        private readonly ILogger _logger;
        public DeliverySchedulerFactory(ILogger logger) => _logger = logger;
        public IDeliveryScheduler Create(int vehicleCount, double speed, double maxWeight)
            => new DeliveryScheduler(vehicleCount, speed, maxWeight, _logger);
    }

    public class DeliveryServiceFactory
    {
        private readonly IDeliverySchedulerFactory _schedulerFactory;
        private readonly ILogger _logger;
        public DeliveryServiceFactory(IDeliverySchedulerFactory schedulerFactory, ILogger logger)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public DeliveryService Create(double baseCost, int vehicleCount, double speed, double maxWeight)
        {
            var scheduler = _schedulerFactory.Create(vehicleCount, speed, maxWeight);
            return new DeliveryService(baseCost, scheduler, _logger);
        }
    }
}

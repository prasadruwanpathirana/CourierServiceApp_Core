using CourierServiceApp.Application;
using CourierServiceApp.Domain;

namespace CourierServiceApp.Presentation
{
    public class ConsoleUI
    {
        private readonly DeliveryServiceFactory _serviceFactory;
        public ConsoleUI(DeliveryServiceFactory serviceFactory) => _serviceFactory = serviceFactory;

        public void Run()
        {
            try
            {
                Console.WriteLine("Enter base delivery cost and number of packages:");
                var input = Console.ReadLine()?.Split();
                InputValidator.ValidateInput(input, 2, "base delivery line");

                double baseCost = InputValidator.TryParse<double>(input[0], "Base Cost");
                int numberOfPackages = InputValidator.TryParse<int>(input[1], "Number of Packages");

                var packages = new List<Package>();
                for (int i = 0; i < numberOfPackages; i++)
                {
                    var line = Console.ReadLine()?.Split();
                    InputValidator.ValidateInput(line, 4, $"package {i + 1}");
                    packages.Add(new Package(
                        line[0],
                        InputValidator.TryParse<double>(line[1], "Package Weight"),
                        InputValidator.TryParse<double>(line[2], "Package Distance"),
                        line[3]));
                }

                Console.WriteLine("Enter vehicle info (count speed max_weight):");
                var vehicleInput = Console.ReadLine()?.Split();
                InputValidator.ValidateInput(vehicleInput, 3, "vehicle line");

                int vehicleCount = InputValidator.TryParse<int>(vehicleInput[0], "Vehicle Count");
                double speed = InputValidator.TryParse<double>(vehicleInput[1], "Vehicle Speed");
                double maxWeight = InputValidator.TryParse<double>(vehicleInput[2], "Vehicle Max Weight");

                var deliveryService = _serviceFactory.Create(baseCost, vehicleCount, speed, maxWeight);
                var results = deliveryService.ProcessPackages(packages);

                foreach (var pkg in results)
                {
                    Console.WriteLine($"{pkg.Id} {pkg.Discount} {pkg.TotalCost} {pkg.DeliveryTime:F2}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal Error: {ex.Message}");
            }
        }
    }
}

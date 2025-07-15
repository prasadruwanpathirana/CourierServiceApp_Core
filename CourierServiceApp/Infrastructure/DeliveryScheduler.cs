// FINAL UPDATE: Assign delivery time per-package based on actual distance
// Infrastructure/DeliveryScheduler.cs
using CourierServiceApp.Application.Exceptions;
using CourierServiceApp.Application.Interfaces;
using CourierServiceApp.Domain;

namespace CourierServiceApp.Infrastructure
{
    public class DeliveryScheduler : IDeliveryScheduler
    {
        private readonly double speed;
        private readonly double maxWeight;
        private readonly List<Vehicle> vehicles;
        private readonly ILogger logger;

        public DeliveryScheduler(int count, double speed, double maxWeight, ILogger logger)
        {
            this.speed = speed;
            this.maxWeight = maxWeight;
            this.logger = logger;
            vehicles = Enumerable.Range(0, count).Select(i => new Vehicle(i)).ToList();
        }

        public void Schedule(List<Package> packages)
        {
            var pending = new List<Package>(packages);

            while (pending.Any())
            {
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
                double currentTime = vehicle.AvailableAt;

                var batch = FindBestFitBatch(pending, maxWeight);

                if (!batch.Any())
                    throw new DeliveryScheduleException("No valid batch found within vehicle capacity.");

                double maxDistance = batch.Max(p => p.Distance);
                double roundTripTime = (maxDistance / speed) * 2;

                foreach (var pkg in batch)
                {
                    double pkgDeliveryTime = currentTime + (pkg.Distance / speed);
                    pkg.DeliveryTime = pkgDeliveryTime;
                }

                vehicle.AvailableAt = currentTime + roundTripTime;

                foreach (var pkg in batch)
                    pending.Remove(pkg);
            }
        }

        private List<Package> FindBestFitBatch(List<Package> packages, double capacity)
        {
            var allCombos = GetAllCombinations(packages);

            var bestCombo = allCombos
                .Where(combo => combo.Sum(p => p.Weight) <= capacity)
                .Select(combo => new
                {
                    Packages = combo,
                    TotalWeight = combo.Sum(p => p.Weight),
                    MaxDistance = combo.Max(p => p.Distance),
                    Count = combo.Count
                })
                .OrderByDescending(c => c.TotalWeight)
                .ThenBy(c => c.MaxDistance)
                .ThenByDescending(c => c.Count)
                .FirstOrDefault();

            return bestCombo?.Packages ?? new List<Package>();
        }

        private List<List<Package>> GetAllCombinations(List<Package> packages)
        {
            var result = new List<List<Package>>();
            int n = packages.Count;

            for (int i = 1; i < (1 << n); i++)
            {
                var combo = new List<Package>();
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) != 0)
                        combo.Add(packages[j]);
                }
                result.Add(combo);
            }

            return result;
        }
    }
}

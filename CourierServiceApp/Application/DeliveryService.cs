using CourierServiceApp.Application.Exceptions;
using CourierServiceApp.Application.Interfaces;
using CourierServiceApp.Domain;

namespace CourierServiceApp.Application
{
    public class DeliveryService
    {
        private readonly double baseCost;
        private readonly IDeliveryScheduler scheduler;
        private readonly ILogger logger;

        public DeliveryService(double baseCost, IDeliveryScheduler scheduler, ILogger logger)
        {
            if (baseCost < 0) throw new AppException("Base cost must be non-negative.");
            this.baseCost = baseCost;
            this.scheduler = scheduler;
            this.logger = logger;
        }

        public List<Package> ProcessPackages(List<Package> packages)
        {
            if (packages == null || packages.Count == 0)
                throw new InvalidPackageException("Package list cannot be empty.");

            try
            {
                foreach (var pkg in packages)
                {
                    double cost = baseCost + (pkg.Weight * 10) + (pkg.Distance * 5);
                    double discount = cost * OfferManager.GetDiscountRate(pkg.OfferCode, pkg.Weight, pkg.Distance);
                    pkg.Discount = discount;
                    pkg.TotalCost = cost - discount;
                }

                scheduler.Schedule(packages);
                return packages;
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw new DeliveryScheduleException("Error occurred during delivery scheduling.", ex);
            }
        }
    }
}

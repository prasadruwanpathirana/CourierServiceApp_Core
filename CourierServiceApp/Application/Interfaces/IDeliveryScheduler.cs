using CourierServiceApp.Domain;
using System.Collections.Generic;

namespace CourierServiceApp.Application.Interfaces
{
    public interface IDeliveryScheduler
    {
        void Schedule(List<Package> packages);
    }
}

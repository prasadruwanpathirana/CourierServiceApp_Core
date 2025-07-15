namespace CourierServiceApp.Domain
{
    public class Vehicle
    {
        public int Id { get; }
        public double AvailableAt { get; set; }

        public Vehicle(int id)
        {
            Id = id;
            AvailableAt = 0;
        }
    }
}

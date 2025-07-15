namespace CourierServiceApp.Domain
{
    public class Package
    {
        public string Id { get; }
        public double Weight { get; }
        public double Distance { get; }
        public string OfferCode { get; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double DeliveryTime { get; set; }

        public Package(string id, double weight, double distance, string offerCode)
        {
            Id = id;
            Weight = weight;
            Distance = distance;
            OfferCode = offerCode;
        }
    }
}
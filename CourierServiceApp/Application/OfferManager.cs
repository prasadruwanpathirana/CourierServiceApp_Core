namespace CourierServiceApp.Application
{
    public static class OfferManager
    {
        public static double GetDiscountRate(string code, double weight, double distance)
        {
            return code switch
            {
                "OFR001" when weight >= 70 && weight <= 200 && distance <= 199 => 0.10,
                "OFR002" when weight >= 100 && weight <= 250 && distance >= 50 && distance <= 150 => 0.07,
                "OFR003" when weight >= 10 && weight <= 150 && distance >= 50 && distance <= 250 => 0.05,
                _ => 0.0
            };
        }
    }
}

namespace monopoly
{
    public enum TypeProperty
    {
        Residential,
        Commercial,
        Utility
    }

    public enum PropertySituation
    {
        Unowned,
        Owned,
        Mortgaged
    }

    public class Property : Square
    {
        private string owner;
        private int price;
        private int rent;
        private int housePrice;
        private int hotelPrice;
        private int numberOfHouses;
        private bool hasHotel;
        private TypeProperty propertyType;
        private PropertySituation propertySituation;

        public Property(int position, string name, string description, int price, int rent, int housePrice, int hotelPrice, TypeProperty propertyType)
            : base(position, name, description)
        {
            this.price = price;
            this.rent = rent;
            this.housePrice = housePrice;
            this.hotelPrice = hotelPrice;
            this.propertyType = propertyType;
            owner = null;
            propertySituation = PropertySituation.Unowned;
            numberOfHouses = 0;
            hasHotel = false;
        }

        public string GetOwner()
        {
            return owner;
        }

        public void SetOwner(string playerName)
        {
            owner = playerName;
            propertySituation = PropertySituation.Owned;
        }

        public int GetPrice()
        {
            return price;
        }

        public int GetRent()
        {
            int totalRent = rent;

            // Calculate rent based on the number of houses and hotel
            if (numberOfHouses > 0)
            {
                // Increase rent based on the number of houses
                totalRent *= (int)Math.Pow(2, numberOfHouses);
            }
            else if (hasHotel)
            {
                // If a hotel is present, charge a fixed rent
                totalRent = rent * 5;
            }

            return totalRent;
        }

        public TypeProperty GetPropertyType()
        {
            return propertyType;
        }

        public PropertySituation GetPropertySituation()
        {
            return propertySituation;
        }

        public void SetPropertySituation(PropertySituation situation)
        {
            propertySituation = situation;
        }

        public void AddHouse()
        {
            // Increase the number of houses
            numberOfHouses++;
        }

        public void RemoveHouse()
        {
            // Decrease the number of houses
            numberOfHouses--;
        }

        public void AddHotel()
        {
            // Add a hotel to the property
            hasHotel = true;
        }

        public void RemoveHotel()
        {
            // Remove the hotel from the property
            hasHotel = false;
        }

        public int GetNumberOfHouses()
        {
            return numberOfHouses;
        }

        public bool HasHotel()
        {
            return hasHotel;
        }

        public int GetHousePrice()
        {
            return housePrice;
        }

        public int GetHotelPrice()
        {
            return hotelPrice;
        }
    }
}

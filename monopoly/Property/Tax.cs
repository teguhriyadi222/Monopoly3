
namespace monopoly
{

    public class Tax : Square
    {
        private int taxAmount;

        public Tax(int position, string name, string description, int taxAmount)
            : base(position, name, description)
        {
            this.taxAmount = taxAmount;
        }

        public int GetTaxAmount()
        {
            return taxAmount;
        }
    }

}


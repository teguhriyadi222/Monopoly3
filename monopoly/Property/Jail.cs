namespace monopoly
{
    public class Jail : Square
    {
        private int bailAmount;

        public Jail(int position, string name, string description, int bailAmount)
            : base(position, name, description)
        {
            this.bailAmount = bailAmount;
        }

        public int GetBailAmount()
        {
            return bailAmount;
        }
    }
}

namespace monopoly
{
    public abstract class Square
    {
        private int position;
        private string name;
        private string description;

        public Square(int position, string name, string description)
        {
            this.position = position;
            this.name = name;
            this.description = description;
        }

        public int GetPosition()
        {
            return position;
        }

        public string GetName()
        {
            return name;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}

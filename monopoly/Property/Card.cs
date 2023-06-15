namespace monopoly
{
    public enum TypeCard
    {
        Chance,
        CommunityChest
    }

    public enum TypeCardCommand
    {
        Move,
        PayTax,
        ReceiveMoney
    }

    public class Card : Square
    {
        private string description;
        private TypeCard type;
        private Action<Player, int> Command { get; set; }
        private TypeCardCommand typeCommand;
        private int valueCard;


        public Card(int position, string name, string description, TypeCard type, int valueCard, TypeCardCommand typeComman)
            : base(position, name, description)
        {
            this.description = description;
            this.type = type;
            this.valueCard = valueCard;
            this.typeCommand = typeCommand;
        }

        public string GetDescription()
        {
            return description;
        }

        public TypeCard GetCardType()
        {
            return type;
        }

        public int GetValue()
        {
            return valueCard;
        }

        public void SetCommand(Action<Player, int> command, TypeCardCommand typeCommand)
        {
            Command = command;
            this.typeCommand = typeCommand;
        }

        

        public void ExecuteCommand(Player player)
        {
            Command?.Invoke(player, valueCard);
        }
    }
}

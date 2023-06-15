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
        private string _description;
        private TypeCard _type;
        private Action<Player, int> _Command { get; set; }
        private TypeCardCommand _typeCommand;
        private int _valueCard;


        public Card(int position, string name, string description, TypeCard type, int valueCard, TypeCardCommand typeComman)
            : base(position, name, description)
        {
            _description = description;
            _type = type;
            _valueCard = valueCard;
            _typeCommand = _typeCommand;
        }

        public string GetDescription()
        {
            return _description;
        }

        public TypeCard GetCardType()
        {
            return _type;
        }

        public TypeCardCommand GetTypeCommand()
        {
            return _typeCommand;
        }

        public int GetValue()
        {
            return _valueCard;
        }

        public void SetCommand(Action<Player, int> command, TypeCardCommand typeCommand)
        {
            _Command = command;
            _typeCommand = typeCommand;
        }

        public void ExecuteCommand(Player player)
        {
            _Command?.Invoke(player, _valueCard);
        }
    }
}

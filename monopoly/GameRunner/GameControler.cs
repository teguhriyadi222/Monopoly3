
namespace monopoly
{
    public class GameController
    {
        private List<Player> _players;
        private IBoard _board;
        private Jail _jail;
        private List<Card> _chanceCards;
        private List<Card> _communityChestCards;
        private IDice _dice;
        private int _currentPlayer;
        private Dictionary<Player, Square> _playerPositions;
        private Dictionary<Player, List<Property>> _playerProperties;
        private Dictionary<Player, int> _playerMoney;
        private Dictionary<Player, bool> _jailStatus;
        private Random _random;
        public event Action<Player> GoToJailEvent;


        public GameController(List<Player> players, IBoard board, Jail jail, List<Card> chanceCards, List<Card> communityChestCards, IDice dice)
        {
            _players = players;
            _board = board;
            _jail = jail;
            _random = new Random();
            _chanceCards = chanceCards;
            _communityChestCards = communityChestCards;
            _dice = dice;
            _playerPositions = new Dictionary<Player, Square>();
            _playerProperties = new Dictionary<Player, List<Property>>();
            _playerMoney = new Dictionary<Player, int>();
            _jailStatus = new Dictionary<Player, bool>();
        }

        public void AddPlayer(string name)
        {
            Player player = new Player(name);
            _players.Add(player);
            _playerProperties[player] = new List<Property>();
            _playerMoney[player] = 20000;
            _jailStatus[player] = false;
        }

        public void CreateBoard(IBoard board)
        {
            _board = board;
        }

        public void SetInitialPlayerPositions()
        {
            Square startSquare = new Start(0, "Start", "Starting point of the board");

            foreach (Player player in _players)
            {
                _playerPositions[player] = startSquare;
            }
        }

        public (int dice1Result, int dice2Result, int totalResult) RollDice()
        {
            int dice1Result = _dice.Roll();
            int dice2Result = _dice.Roll();
            int totalResult = dice1Result + dice2Result;
            return (dice1Result, dice2Result, totalResult);
        }

        public Player GetActivePlayer()
        {
            if (_players.Count > 0)
            {
                int currentPlayerIndex = GetCurrentPlayerIndex();
                Player activePlayer = _players[currentPlayerIndex];
                return activePlayer;
            }
            return null;
        }

        public int GetPlayerMoney(Player player)
        {
            if (_playerMoney.ContainsKey(player))
            {
                return _playerMoney[player];
            }
            return 0;
        }

        public int GetCurrentPlayerIndex()
        {
            if (_currentPlayer < 0 || _currentPlayer >= _players.Count)
            {
                _currentPlayer = 0;
            }

            return _currentPlayer;
        }

        public void NextTurn()
        {
            _currentPlayer = (_currentPlayer + 1) % _players.Count;
        }


        public int GetPlayerPosition(Player player)
        {
            if (_playerPositions.ContainsKey(player))
            {
                Square square = _playerPositions[player];
                return square.GetPosition();
            }

            return -1;
        }

        public void SetPlayerPosition(Player player, Square square)
        {
            if (_playerPositions.ContainsKey(player))
            {
                _playerPositions[player] = square;
            }
            else
            {
                _playerPositions.Add(player, square);
            }
        }

        public void Move(Player player, int steps)
        {
            int currentPosition = GetPlayerPosition(player);

            if (currentPosition >= 0)
            {
                int numSquares = _board.GetSquaresCount();
                int newPosition = (currentPosition + steps) % numSquares;

                Square newSquare = _board.GetSquare(newPosition);

                SetPlayerPosition(player, newSquare);

                HandleSquareAction(player, newSquare);
            }
        }

        public void HandleSquareAction(Player player, Square square)
        {
            if (square is Property property)
            {
                HandlePropertyAction(player, property);
            }
            else if (square is Tax tax)
            {
                HandleTaxAction(player, tax);
            }
            else if (square is Start start)
            {
                HandlePassGoAction(player);
            }
            else if (square is Card card)
            {
                ExecuteCommand(player, card);

            }
            else if (square is GoToJail jail)
            {
                GoToJail(player, jail);
            }
        }

        public void HandlePropertyAction(Player player, Property property)
        {
            string propertyOwnerName = property.GetOwner();
            string currentPlayerName = player.GetName();

            if (propertyOwnerName != null && propertyOwnerName != currentPlayerName)
            {

                int rentAmount = property.GetRent();

                if (_playerMoney.ContainsKey(player))
                {
                    _playerMoney[player] -= rentAmount;
                }

                Player propertyOwner = GetPlayerByName(propertyOwnerName);

                if (_playerMoney.ContainsKey(propertyOwner))
                {
                    _playerMoney[propertyOwner] += rentAmount;
                }
            }
        }

        public void HandleTaxAction(Player player, Tax tax)
        {
            PayTax(player, tax);
        }

        public void HandlePassGoAction(Player player)
        {
            int salaryAmount = 200;
            Player currentPlayer = GetPlayerByName(player.GetName());
            if (_playerMoney.ContainsKey(currentPlayer))
            {
                _playerMoney[currentPlayer] += salaryAmount;
            }

        }

        public Player GetPlayerByName(string playerName)
        {
            foreach (Player player in _players)
            {
                if (player.GetName() == playerName)
                {
                    return player;
                }
            }

            return null;
        }



        public List<Property> GetPlayerProperties(Player player)
        {
            if (_playerProperties.ContainsKey(player))
            {
                return _playerProperties[player];
            }

            return null;
        }

        public void AddPropertyToPlayer(Player player, Property property)
        {
            if (_playerProperties.ContainsKey(player))
            {
                List<Property> properties = _playerProperties[player];
                properties.Add(property);
            }
        }

        public void RemovePropertyFromPlayer(Player player, Property property)
        {
            if (_playerProperties.ContainsKey(player))
            {
                List<Property> properties = _playerProperties[player];
                properties.Remove(property);
            }
        }


        public void SellProperty(Player player, Property property)
        {
            if (_playerProperties.ContainsKey(player))
            {
                List<Property> properties = _playerProperties[player];
                if (properties.Contains(property) && property.GetOwner() == player.GetName())
                {
                    int propertyPrice = property.GetPrice();

                    if (_playerMoney.ContainsKey(player))
                    {
                        _playerMoney[player] += propertyPrice; // Tambahkan uang langsung ke dictionary playerMoney

                        // Hapus properti dari daftar properti pemain
                        properties.Remove(property);
                        property.SetOwner(null);
                    }
                }
            }
        }


        public void BuyProperty(Player player, Property property)
        {
            if (_playerProperties.ContainsKey(player))
            {
                List<Property> properties = _playerProperties[player];
                if (!properties.Contains(property) && property.GetOwner() == null)
                {
                    int propertyPrice = property.GetPrice();

                    if (_playerMoney.ContainsKey(player) && _playerMoney[player] >= propertyPrice)
                    {
                        _playerMoney[player] -= propertyPrice;
                        properties.Add(property);
                        property.SetOwner(player.GetName());
                    }
                }
            }
        }

        public void PayTax(Player player, Tax tax)
        {
            int taxAmount = tax.GetTaxAmount();

            if (_playerMoney.ContainsKey(player))
            {
                _playerMoney[player] -= taxAmount;
            }
        }


        public void GoToJail(Player player, GoToJail goToJail)
        {
            Jail jail = GetJail();
            SetPlayerPosition(player, jail);
            _jailStatus[player] = true;
            OnGoToJail(player);
        }

        public void OnGoToJail(Player player)
        {
            GoToJailEvent?.Invoke(player);
        }

        public Jail GetJail()
        {
            for (int i = 0; i < _board.GetSquaresCount(); i++)
            {
                Square square = _board.GetSquare(i);
                if (square is Jail jail)
                {
                    return jail;
                }
            }

            return null;
        }

        public bool IsPlayerInJail(Player player)
        {
            if (_jailStatus.ContainsKey(player))
            {
                return _jailStatus[player];
            }
            return false;
        }

        public void ReleaseFromJail(Player player)
        {
            Jail jail = GetJail();
            if (_playerPositions[player] == jail)
            {
                int escapeAttempts = 0;

                while (escapeAttempts < 3)
                {
                    (int dice1Result, int dice2Result, int totalResult) = RollDice();

                    if (dice1Result == dice2Result)
                    {
                        Move(player, totalResult);
                        _jailStatus[player] = false;
                        break;
                    }

                    escapeAttempts++;
                }

                if (escapeAttempts == 3)
                {

                }
            }
        }


        public bool IsPlayerBankrupt(Player player)
        {
            if (_playerMoney.ContainsKey(player))
            {
                int playerMoneyAmount = _playerMoney[player];
                List<Property> playerProperties = GetPlayerProperties(player);
                if (playerMoneyAmount <= 0 && (playerProperties == null || playerProperties.Count == 0))
                {
                    return true;
                }
            }

            return false;
        }

        public bool BuyHouse(Player player, Property property)
        {
            if (property.GetPropertySituation() != PropertySituation.Owned || property.GetOwner() != player.GetName())
            {
                return false;
            }

            if (property.GetPropertyType() != TypeProperty.Residential)
            {
                return false;
            }

            int housePrice = property.GetHousePrice();
            int maxHouses = 4;

            if (property.GetNumberOfHouses() >= maxHouses)
            {
                return false;
            }

            if (_playerMoney.ContainsKey(player) && _playerMoney[player] >= housePrice)
            {
                _playerMoney[player] -= housePrice;
                property.AddHouse();
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool BuyHotel(Player player, Property property)
        {
            if (property.GetPropertySituation() != PropertySituation.Owned || property.GetOwner() != player.GetName())
            {
                return false;
            }

            if (property.GetPropertyType() != TypeProperty.Residential)
            {
                return false;
            }

            int hotelPrice = property.GetHotelPrice();

            if (_playerMoney.ContainsKey(player) && _playerMoney[player] >= hotelPrice)
            {
                _playerMoney[player] -= hotelPrice;

                property.AddHotel();
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ExecuteCommand(Player player, Card card)
        {
            TypeCardCommand typeCardCommand = card.GetTypeCommand();
            int valueCard = card.GetValue();
            switch (typeCardCommand)
            {
                case TypeCardCommand.Move:
                    Move(player, valueCard);
                    break;
                case TypeCardCommand.PayTax:
                    _playerMoney[player] -= valueCard;
                    break;
                case TypeCardCommand.ReceiveMoney:
                    _playerMoney[player] += valueCard;
                    break;
            }

        }

        public void AddCard(Card card)
        {
            if (card.GetCardType() == TypeCard.Chance)
            {
                if (_chanceCards == null)
                    _chanceCards = new List<Card>();

                _chanceCards.Add(card);
            }
            else if (card.GetCardType() == TypeCard.CommunityChest)
            {
                if (_communityChestCards == null)
                    _communityChestCards = new List<Card>();

                _communityChestCards.Add(card);
            }
        }

        public void DrawRandomCard(Player player, TypeCard cardType)
        {
            List<Card> cardList;

            if (cardType == TypeCard.Chance)
            {
                cardList = _chanceCards;
            }
            else if (cardType == TypeCard.CommunityChest)
            {
                cardList = _communityChestCards;
            }
            else
            {
                return;
            }

            Random random = new Random();
            int index = random.Next(0, cardList.Count);
            Card card = cardList[index];

            ExecuteCommand(player, card);
        }



        public void StartGame()
        {
            SetInitialPlayerPositions();
            _currentPlayer = 0;
        }

    }
}
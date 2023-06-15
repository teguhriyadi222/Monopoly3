
namespace monopoly
{
    public class GameController
    {
        private List<Player> players;
        private IBoard board;
        private Jail jail;
        private List<Card> chanceCards;
        private List<Card> communityChestCards;
        private IDice dice;
        private int currentPlayer;
        private Dictionary<Player, Square> playerPositions;
        private Dictionary<Player, List<Property>> playerProperties;
        private Dictionary<Player, int> playerMoney;
        private Dictionary<Player, bool> jailStatus;
        private Random random;


        public GameController(List<Player> players, IBoard board, Jail jail, List<Card> chanceCards, List<Card> communityChestCards, IDice dice)
        {
            this.players = players;
            this.board = board;
            this.jail = jail;
            this.random = new Random();
            this.chanceCards = chanceCards;
            this.communityChestCards = communityChestCards;
            this.dice = dice;
            this.playerPositions = new Dictionary<Player, Square>();
            this.playerProperties = new Dictionary<Player, List<Property>>();
            this.playerMoney = new Dictionary<Player, int>();
            this.jailStatus = new Dictionary<Player, bool>();
        }

        public void AddPlayer(string name)
        {
            Player player = new Player(name);
            players.Add(player);
            playerProperties[player] = new List<Property>();
            playerMoney[player] = 20000;
            jailStatus[player] = false;
        }

        public void CreateBoard(IBoard board)
        {
            this.board = board;
        }

        public (int dice1Result, int dice2Result, int totalResult) RollDice()
        {
            int dice1Result = dice.Roll();
            int dice2Result = dice.Roll();
            int totalResult = dice1Result + dice2Result;
            return (dice1Result, dice2Result, totalResult);
        }

        public Player GetActivePlayer()
        {
            if (players.Count > 0)
            {
                int currentPlayerIndex = GetCurrentPlayerIndex();
                Player activePlayer = players[currentPlayerIndex];
                return activePlayer;
            }

            return null;
        }

        public int GetPlayerMoney(Player player)
        {
            if (playerMoney.ContainsKey(player))
            {
                return playerMoney[player];
            }
            return 0; // Atau nilai default yang sesuai jika pemain tidak ditemukan dalam dictionary
        }

        private int GetCurrentPlayerIndex()
        {
            if (currentPlayer < 0 || currentPlayer >= players.Count)
            {
                currentPlayer = 0;
            }

            return currentPlayer;
        }

        public void NextTurn()
        {
            currentPlayer = (currentPlayer + 1) % players.Count;
        }

        public void SetInitialPlayerPositions()
        {
            Square startSquare = new Start(0, "Start", "Starting point of the board");

            foreach (Player player in players)
            {
                playerPositions[player] = startSquare;
            }
        }

        public int GetPlayerPosition(Player player)
        {
            if (playerPositions.ContainsKey(player))
            {
                Square square = playerPositions[player];
                return square.GetPosition();
            }

            return -1;
        }

        public void SetPlayerPosition(Player player, Square square)
        {
            if (playerPositions.ContainsKey(player))
            {
                playerPositions[player] = square;
            }
            else
            {
                playerPositions.Add(player, square);
            }
        }

        public void Move(Player player, int steps)
        {
            int currentPosition = GetPlayerPosition(player);

            if (currentPosition >= 0)
            {
                int numSquares = board.GetSquaresCount();
                int newPosition = (currentPosition + steps) % numSquares;

                Square newSquare = board.GetSquare(newPosition);

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
        }

        public void HandlePropertyAction(Player player, Property property)
        {
            string propertyOwnerName = property.GetOwner();
            string currentPlayerName = player.GetName();

            // Periksa apakah properti dimiliki oleh pemain lain
            if (propertyOwnerName != null && propertyOwnerName != currentPlayerName)
            {
                // Dapatkan jumlah sewa properti
                int rentAmount = property.GetRent();

                // Kurangi uang pemain saat ini dengan jumlah sewa
                if (playerMoney.ContainsKey(player))
                {
                    playerMoney[player] -= rentAmount;
                }

                // Dapatkan pemilik properti
                Player propertyOwner = GetPlayerByName(propertyOwnerName);

                // Tambahkan jumlah sewa ke uang pemilik properti
                if (playerMoney.ContainsKey(propertyOwner))
                {
                    playerMoney[propertyOwner] += rentAmount;
                }
            }
        }

        public void HandleTaxAction(Player player, Tax tax)
        {
            PayTax(player, tax);
        }

        public void HandlePassGoAction(Player player)
        {
            // Jika pemain melewati awal papan, tambahkan uang sejumlah gaji yang diterima
            int salaryAmount = 200;
            Player currentPlayer = GetPlayerByName(player.GetName()); // Dapatkan objek Player berdasarkan nama
            if (playerMoney.ContainsKey(currentPlayer))
            {
                playerMoney[currentPlayer] += salaryAmount; // Tambahkan uang langsung ke dictionary playerMoney
            }

        }

        public Player GetPlayerByName(string playerName)
        {
            foreach (Player player in players)
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
            if (playerProperties.ContainsKey(player))
            {
                return playerProperties[player];
            }

            return null;
        }

        public void AddPropertyToPlayer(Player player, Property property)
        {
            if (playerProperties.ContainsKey(player))
            {
                List<Property> properties = playerProperties[player];
                properties.Add(property);
            }
        }

        public void RemovePropertyFromPlayer(Player player, Property property)
        {
            if (playerProperties.ContainsKey(player))
            {
                List<Property> properties = playerProperties[player];
                properties.Remove(property);
            }
        }


        public void SellProperty(Player player, Property property)
        {
            if (playerProperties.ContainsKey(player))
            {
                List<Property> properties = playerProperties[player];
                if (properties.Contains(property) && property.GetOwner() == player.GetName())
                {
                    int propertyPrice = property.GetPrice();

                    if (playerMoney.ContainsKey(player))
                    {
                        playerMoney[player] += propertyPrice; // Tambahkan uang langsung ke dictionary playerMoney

                        // Hapus properti dari daftar properti pemain
                        properties.Remove(property);
                        property.SetOwner(null);
                    }
                }
            }
        }


        public void BuyProperty(Player player, Property property)
        {
            if (playerProperties.ContainsKey(player))
            {
                List<Property> properties = playerProperties[player];
                if (!properties.Contains(property) && property.GetOwner() == null)
                {
                    int propertyPrice = property.GetPrice();

                    if (playerMoney.ContainsKey(player) && playerMoney[player] >= propertyPrice)
                    {
                        playerMoney[player] -= propertyPrice; // Kurangi uang langsung dari dictionary playerMoney

                        // Tambahkan properti ke daftar properti pemain
                        properties.Add(property);

                        // Atur pemilik properti menjadi pemain
                        property.SetOwner(player.GetName());
                    }
                }
            }
        }

        public void PayTax(Player player, Tax tax)
        {
            int taxAmount = tax.GetTaxAmount();

            if (playerMoney.ContainsKey(player))
            {
                playerMoney[player] -= taxAmount; // Kurangi uang langsung dari dictionary playerMoney
            }
        }


        public void GoToJail(Player player, GoToJail goToJail)
        {
            // Dapatkan objek Jail dari GameController
            Jail jail = GetJail();

            // Pindahkan pemain ke petak penjara
            SetPlayerPosition(player, jail);
            jailStatus[player] = true;
        }

        public Jail GetJail()
        {
            for (int i = 0; i < board.GetSquaresCount(); i++)
            {
                Square square = board.GetSquare(i);
                if (square is Jail jail)
                {
                    return jail;
                }
            }

            return null;
        }

        public bool IsPlayerInJail(Player player)
        {
            if (jailStatus.ContainsKey(player))
            {
                return jailStatus[player];
            }
            return false; // Jika pemain tidak ditemukan dalam kamus, anggap mereka tidak berada di penjara
        }

        public void ReleaseFromJail(Player player)
        {
            // Get the Jail object from GameController
            Jail jail = GetJail();

            // Check if the current player is in jail
            if (playerPositions[player] == jail)
            {
                // Process the release from jail

                // Roll the dice
                (int dice1Result, int dice2Result, int totalResult) = RollDice();

                // Check if it's a double roll
                if (dice1Result == dice2Result)
                {
                    // Move the player by the dice result
                    Move(player, totalResult);

                    // Set the player's jail status to false
                    jailStatus[player] = false;
                }
                else
                {
                    // If it's not a double roll, the player remains in jail

                    // Decrease the number of jail attempts
                    int remainingJailAttempts = GetRemainingJailAttempts(player);

                    // If the player has attempted to leave jail three times,
                    // they must pay a fine specified by the bail amount
                    if (remainingJailAttempts == 0)
                    {
                        int fineAmount = jail.GetBailAmount();

                        if (playerMoney.ContainsKey(player))
                        {
                            playerMoney[player] -= fineAmount; // Kurangi uang langsung dari dictionary playerMoney
                        }

                        // Set the player's jail status to false
                        jailStatus[player] = false;
                    }
                }
            }
        }


        private int GetRemainingJailAttempts(Player player)
        {
            int maxJailAttempts = 3;
            int usedJailAttempts = maxJailAttempts - playerMoney[player] / jail.GetBailAmount();
            int remainingJailAttempts = maxJailAttempts - usedJailAttempts;

            return remainingJailAttempts;
        }

        public bool IsPlayerBankrupt(Player player)
        {
            if (playerMoney.ContainsKey(player))
            {
                int playerMoneyAmount = playerMoney[player];
                List<Property> playerProperties = GetPlayerProperties(player);

                // Cek jika uang pemain kurang dari atau sama dengan 0
                // dan pemain tidak memiliki properti
                if (playerMoneyAmount <= 0 && (playerProperties == null || playerProperties.Count == 0))
                {
                    return true;  // Pemain bangkrut
                }
            }

            return false;  // Pemain belum bangkrut
        }

        public bool BuyHouse(Player player, Property property)
        {
            if (property.GetPropertySituation() != PropertySituation.Owned || property.GetOwner() != player.GetName())
            {
                // Properti belum dimiliki oleh pemain atau pemain bukan pemilik properti
                return false;
            }

            if (property.GetPropertyType() != TypeProperty.Residential)
            {
                // Properti bukan tipe Residential, tidak dapat membeli rumah
                return false;
            }

            int housePrice = property.GetHousePrice();
            int maxHouses = 4; // Jumlah maksimal rumah yang dapat dibeli

            if (property.GetNumberOfHouses() >= maxHouses)
            {
                // Sudah mencapai jumlah maksimal rumah, tidak dapat membeli lagi
                return false;
            }

            // Periksa apakah pemain memiliki cukup uang untuk membeli rumah
            if (playerMoney.ContainsKey(player) && playerMoney[player] >= housePrice)
            {
                // Kurangi uang pemain dengan harga rumah
                playerMoney[player] -= housePrice;

                // Tambahkan jumlah rumah pada properti
                property.AddHouse();
                return true;

            }
            else
            {
                return false;
            }
        }

        public void BuyHotel(Player player, Property property)
        {
            if (property.GetPropertySituation() != PropertySituation.Owned || property.GetOwner() != player.GetName())
            {
                // Properti belum dimiliki oleh pemain atau pemain bukan pemilik properti
                return;
            }

            if (property.GetPropertyType() != TypeProperty.Residential)
            {
                // Properti bukan tipe Residential, tidak dapat membeli hotel
                return;
            }

            int hotelPrice = property.GetHotelPrice();

            if (playerMoney.ContainsKey(player) && playerMoney[player] >= hotelPrice)
            {
                playerMoney[player] -= hotelPrice;

                property.AddHotel();

            }
            else
            {

            }
        }

        public void AddChanceCard(Card card)
        {
            int randomIndex = new Random().Next(0, chanceCards.Count + 1);
            chanceCards.Add(card);
        }

        public void AddCommunityChestCard(Card card)
        {
            int randomIndex = new Random().Next(0, communityChestCards.Count + 1);
            communityChestCards.Add(card);
        }

        // public void ExecuteCommand(Player player, Card card)
        // {
        //     TypeCardCommand typeCommand = card.GetCardType();
        //     switch (typeCommand)
        //     {
        //         case TypeCardCommand.Move:
        //             int valueCard = card.GetValue();
        //             player.Move(valueCard);
        //             break;
        //         case TypeCardCommand.PayTax:
        //             valueCard = card.GetValue();
        //             player.PayTax(valueCard);
        //             break;
        //         case TypeCardCommand.ReceiveMoney:
        //             valueCard = card.GetValue();
        //             player.ReceiveMoney(valueCard);
        //             break;
        //         default:
        //             // Handle unrecognized command type
        //             break;
        //     }
        // }

        public void StartGame()
        {
            SetInitialPlayerPositions();
            currentPlayer = 0;
        }

    }
}
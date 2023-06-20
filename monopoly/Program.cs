using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace monopoly
{
    public class Program
    {

        static async Task Main()
        {
            IBoard board = new Board();

            Jail jail = new Jail(10, "Jail", "Jail square", 70);
            GameController gameController = new GameController(
                new List<Player>(),
                board,
                jail,
                new List<Card>(),
                new List<Card>(),
                new Dice(6)
            );
            gameController.GoToJailEvent += HandleGoToJailEvent;

            board.AddSquare(new Start(0, "Start", "Starting point of the board"));
            board.AddSquare(new Property(1, "Salatiga", "Description 1", 100, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(2, "Bandung", "Description 2", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(3, "Semarang", "Description 3", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(4, "jakarta", "Description 4", 300, 50, 30, 20, TypeProperty.Train));
            board.AddSquare(new Property(5, "Malang", "Description 5", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(6, "Purworejo", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Tax(7, "Tax 1", "kamu harus membayar: Rp.100", 100));
            board.AddSquare(new Property(8, "Yogyakarta", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(9, "Bekasi", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(jail);
            board.AddSquare(new Property(12, "Jambi", "Description 6", 300, 50, 30, 20, TypeProperty.Utility));
            board.AddSquare(new Tax(13, "Tax 2", "kamu harus membayar: Rp.200", 200));
            board.AddSquare(new Property(14, "Medan", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(15, "Surabaya", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(16, "Denpasar", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(17, "Ketapang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(18, "Cilacap", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(19, "Cikarang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(20, "Banten", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new FreeParking(21, "Free Parking", "Description 6"));
            board.AddSquare(new Tax(23, "Tax 3", "kamu harus membayar: Rp.300", 300));
            board.AddSquare(new Property(24, "JayaPura", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(25, "Palu", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(26, "Kutai", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(27, "Magelang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(28, "Ambarawa", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new Property(29, "Ungaran", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
            board.AddSquare(new GoToJail(30, "GoToJail", "Description"));
            Console.Clear();
            Console.WriteLine("=================WELCOME TO MOOPOLY GAME=======================");
            Console.Write("Enter the number of players: ");
            int numberOfPlayers = 0;
            bool inputValid = false;
            while (!inputValid)
            {
                if (int.TryParse(Console.ReadLine(), out numberOfPlayers) && numberOfPlayers > 0)
                {
                    inputValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of players.");
                    Console.Write("Enter the number of players: ");
                }
            }

            // Adding players
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Write("Enter the name of player {0}: ", i + 1);
                string playerName = Console.ReadLine();

                if (!string.IsNullOrEmpty(playerName))
                {
                    gameController.AddPlayer(playerName);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    i--;
                }
            }

            Console.WriteLine("Press enter to start the game");

            Console.ReadKey();
            gameController.StartGame();

            // Perulangan untuk giliran pemain
            bool gameEnd = false;
            while (!gameEnd)
            {
                // Mendapatkan pemain yang sedang bermain saat ini
                Player activePlayer = gameController.GetActivePlayer();

                Console.Clear();
                onsole.WriteLine("Player's Turn: " + activePlayer.GetName());
                Console.WriteLine("Press enter to Roll the Dice");
                Console.ReadKey();
                // Melempar dadu
                (int dice1Result, int dice2Result, int totalResult) = gameController.RollDice();
                Console.WriteLine("Dice 1: " + dice1Result);
                await Task.Delay(2000);
                Console.WriteLine("Dice 2: " + dice2Result);
                await Task.Delay(2000);
                Console.WriteLine("Total: " + totalResult);
                await Task.Delay(2000);
                Console.Clear();
                // Memindahkan pemain
                gameController.Move(activePlayer, totalResult);
                // Mendapatkan posisi pemain setelah pergerakan
                int playerPosition = gameController.GetPlayerPosition(activePlayer);

                // Menampilkan posisi pemain
                // Mendapatkan square yang sedang ditempati oleh pemain9
                Square currentSquare = board.GetSquare(playerPosition);
                List<Property> properties = new List<Property>();
                Console.WriteLine("Your Position :");
                Console.WriteLine("Player Position: " + playerPosition);
                Console.WriteLine("name : " + currentSquare.GetName());
                if (currentSquare is Property property)
                {
                    if (property.GetOwner() != null)
                    {
                        Console.WriteLine("This property is owned by: " + property.GetOwner());
                        Console.WriteLine("Rent amount: " + property.GetRent());
                    }
                    else
                    {
                        Console.WriteLine("Property Price: " + property.GetPrice());

                    }


                }
                else if (currentSquare is Tax tax)
                {
                    Console.WriteLine("You must pay: " + tax.GetTaxAmount());
                }

                Console.WriteLine(currentSquare.GetDescription());

                bool turnEnd = false;

                while (!turnEnd)
                {
                    Console.ReadKey();
                    // Menampilkan menu
                    Console.WriteLine("\nPlease Make a Selection :");
                    Console.WriteLine("1 : Finish Turn");
                    Console.WriteLine("2 : Your DashBoard");
                    Console.WriteLine("3 : Purchase the property");
                    Console.WriteLine("4 : Quit Game");

                    List<Property> playerProperties = gameController.GetPlayerProperties(activePlayer);

                    if (playerProperties != null && playerProperties.Count > 0)
                    {
                        Console.WriteLine("5 : Buy House");
                        Console.WriteLine("6 : Buy Hotel");
                        Console.WriteLine("7 : Sell Property");
                    }
                    Console.Write("Choose Your Options : ");

                    int menuSelection = int.Parse(Console.ReadLine());

                    switch (menuSelection)
                    {
                        case 1:
                            turnEnd = true;
                            gameController.NextTurn();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("==== Dashboard ====");
                            Console.WriteLine("Your position is: " + gameController.GetPlayerPosition(activePlayer));
                            Console.WriteLine("Your money is: Rp." + gameController.GetPlayerMoney(activePlayer));
                            Console.WriteLine("Your properties are: ");
                            if (playerProperties != null && playerProperties.Count > 0)
                            {
                                foreach (Property prop in playerProperties)
                                {
                                    Console.WriteLine(prop.GetName());
                                    Console.WriteLine("Total Houses: " + prop.GetNumberOfHouses());
                                }
                            }
                            else
                            {
                                Console.WriteLine("You don't have any properties.");
                            }

                            break;
                        case 3:

                            if (currentSquare is Property py)
                            {
                                gameController.BuyProperty(activePlayer, py);
                                Console.WriteLine("Property successfully purchased!");
                            }
                            else
                            {
                                Console.WriteLine("Failed to purchase a Property.");
                            }
                            break;
                        case 4:
                            if (currentSquare is Property prope)
                            {
                                if (gameController.BuyHouse(activePlayer, prope))
                                {
                                    Console.WriteLine("House successfully purchased!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to purchase a house.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("You are not on a property where you can buy a house.");
                            }
                            break;
                        case 5:
                            if (currentSquare is Property pro)
                            {
                                if (gameController.BuyHotel(activePlayer, pro))
                                {
                                    Console.WriteLine("Hotel successfully purchased!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to purchase a hotel.");
                                }
                            }

                            break;
                        case 6:
                            if (currentSquare is Property p)
                            {
                                gameController.SellProperty(activePlayer, p);
                                Console.WriteLine("successful sale of property");
                            }
                            else
                            {
                                Console.WriteLine("You are not on a property to sell.");
                            }

                            break;
                        case 7:

                            gameEnd = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Selection");
                            break;
                    }

                }
            }

        }
        static void HandleGoToJailEvent(Player player)
        {
            Console.WriteLine($"{player.GetName()} has been sent to jail.");
        }
    }
}


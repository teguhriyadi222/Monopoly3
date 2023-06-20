// namespace monopoly
// {
//     public partial class Program
//     {

//         static async Task Game()
//         {
//             IBoard board = new Board();
            
//             Jail jail = new Jail(10, "Jail", "Jail square", 70);
//             GameController gameController = new GameController(
//                 new List<Player>(),
//                 board,
//                 jail,
//                 new List<Card>(),
//                 new List<Card>(),
//                 new Dice(6)
//             );
//             Console.WriteLine("tekan enter untuk memulai permain");
//             Console.ReadKey();
//             gameController.StartGame();

//             // Perulangan untuk giliran pemain
//             bool gameEnd = false;
//             while (!gameEnd)
//             {
//                 // Mendapatkan pemain yang sedang bermain saat ini
//                 Player activePlayer = gameController.GetActivePlayer();

//                 Console.Clear();
//                 Console.WriteLine("Giliran pemain: " + activePlayer.GetName());
//                 Console.WriteLine("tekan enter untuk Roll Dadu");
//                 Console.ReadKey();
//                 // Melempar dadu
//                 (int dice1Result, int dice2Result, int totalResult) = gameController.RollDice();
//                 Console.WriteLine("Dice 1: " + dice1Result);
//                 await Task.Delay(2000);
//                 Console.WriteLine("Dice 2: " + dice2Result);
//                 await Task.Delay(2000);
//                 Console.WriteLine("Total: " + totalResult);
//                 await Task.Delay(2000);
//                 Console.Clear();
//                 // Memindahkan pemain
//                 gameController.Move(activePlayer, totalResult);
//                 // Mendapatkan posisi pemain setelah pergerakan
//                 int playerPosition = gameController.GetPlayerPosition(activePlayer);

//                 // Menampilkan posisi pemain
//                 // Mendapatkan square yang sedang ditempati oleh pemain9
//                 Square currentSquare = board.GetSquare(playerPosition);
//                 List<Property> properties = new List<Property>();
//                 Console.WriteLine("Your Position :");
//                 Console.WriteLine("Player Position: " + playerPosition);
//                 Console.WriteLine("name : " + currentSquare.GetName());
//                 if (currentSquare is Property property)
//                 {
//                     if (property.GetOwner() != null)
//                     {
//                         Console.WriteLine("This property is owned by: " + property.GetOwner());
//                         Console.WriteLine("Rent amount: " + property.GetRent());
//                     }
//                     else
//                     {
//                         Console.WriteLine("Property Price: " + property.GetPrice());

//                     }


//                 }
//                 else if (currentSquare is Tax tax)
//                 {
//                     Console.WriteLine("You must pay: " + tax.GetTaxAmount());
//                 }

//                 Console.WriteLine(currentSquare.GetDescription());

//                 bool turnEnd = false;

//                 while (!turnEnd)
//                 {
//                     Console.ReadKey();
//                     // Menampilkan menu
//                     Console.WriteLine("\nPlease Make a Selection :");
//                     Console.WriteLine("1 : Finish Turn");
//                     Console.WriteLine("2 : Your DashBoard");
//                     Console.WriteLine("3 : Purchase the property");
//                     Console.WriteLine("4 : Quit Game");

//                     List<Property> playerProperties = gameController.GetPlayerProperties(activePlayer);

//                     if (playerProperties != null && playerProperties.Count > 0)
//                     {
//                         Console.WriteLine("5 : Buy House");
//                         Console.WriteLine("6 : Buy Hotel");
//                         Console.WriteLine("7 : Sell Property");
//                     }
//                     Console.Write("Choose Your Options : ");

//                     int menuSelection = int.Parse(Console.ReadLine());

//                     switch (menuSelection)
//                     {
//                         case 1:
//                             turnEnd = true;
//                             gameController.NextTurn();
//                             break;
//                         case 2:
//                             Console.Clear();
//                             Console.WriteLine("==== Dashboard ====");
//                             Console.WriteLine("Your position is: " + gameController.GetPlayerPosition(activePlayer));
//                             Console.WriteLine("Your money is: Rp." + gameController.GetPlayerMoney(activePlayer));
//                             Console.WriteLine("Your properties are: ");
//                             if (playerProperties != null && playerProperties.Count > 0)
//                             {
//                                 foreach (Property prop in playerProperties)
//                                 {
//                                     Console.WriteLine(prop.GetName());
//                                     Console.WriteLine("Total Houses: " + prop.GetNumberOfHouses());
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You don't have any properties.");
//                             }

//                             break;
//                         case 3:

//                             if (currentSquare is Property py)
//                             {
//                                 gameController.BuyProperty(activePlayer, py);
//                                 Console.WriteLine("Property successfully purchased!");
//                             }
//                             else
//                             {
//                                 Console.WriteLine("Failed to purchase a Property.");
//                             }
//                             break;
//                         case 4:
//                             if (currentSquare is Property prope)
//                             {
//                                 if (gameController.BuyHouse(activePlayer, prope))
//                                 {
//                                     Console.WriteLine("House successfully purchased!");
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("Failed to purchase a house.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You are not on a property where you can buy a house.");
//                             }
//                             break;
//                         case 5:
//                             if (currentSquare is Property pro)
//                             {
//                                 if (gameController.BuyHotel(activePlayer, pro))
//                                 {
//                                     Console.WriteLine("Hotel successfully purchased!");
//                                 }
//                                 else
//                                 {
//                                     Console.WriteLine("Failed to purchase a hotel.");
//                                 }
//                             }

//                             break;
//                         case 6:
//                             if (currentSquare is Property p)
//                             {
//                                 gameController.SellProperty(activePlayer, p);
//                                 Console.WriteLine("successful sale of property");
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You are not on a property to sell.");
//                             }

//                             break;
//                         case 7:

//                             gameEnd = true;
//                             break;
//                         default:
//                             Console.WriteLine("Invalid Selection");
//                             break;
//                     }

//                 }
//             }

//         }
//         static void HandleGoToJailEvent(Player player)
//         {
//             Console.WriteLine($"{player.GetName()} has been sent to jail.");
//         }
//     }
// }




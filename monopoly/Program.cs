// using System;
// using System.Collections.Generic;

// namespace monopoly
// {
//     public class Program
//     {
//         static void Main(string[] args)
//         {
//             // Membuat objek Board
//             IBoard board = new Board();

//             Jail jail = new Jail(10, "Jail", "Jail square", 70);

//             // Membuat objek GameController
//             GameController gameController = new GameController(
//                 new List<Player>(), // Tambahkan daftar pemain sesuai kebutuhan
//                 board,
//                 jail, // Tambahkan objek Jail sesuai kebutuhan
//                 new List<Card>(), // Tambahkan daftar kartu Chance sesuai kebutuhan
//                 new List<Card>(), // Tambahkan daftar kartu Community Chest sesuai kebutuhan
//                 new Dice() // Tambahkan objek Dice sesuai kebutuhan
//             );

//             // Membuat papan permainan (Board)
//             // Tambahkan pengaturan posisi-posisi square di board sesuai kebutuhan
//             board.AddSquare(new Start(0, "Start", "Starting point of the board"));
//             board.AddSquare(new Property(1, "Property 1", "Description 1", 100, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(2, "Property 2", "Description 2", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(3, "Property 3", "Description 3", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(4, "Property 4", "Description 4", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(5, "Property 5", "Description 5", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(6, "Property 6", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Tax(7, "Tax 1", "kamu harus membayar: Rp.100", 100));
//             board.AddSquare(new Property(8, "Property 7", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(9, "Property 8", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(12, "Property 9", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Tax(13, "Tax 2", "kamu harus membayar: Rp.200", 200));
//             board.AddSquare(new Property(14, "Property 10", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(15, "Property 11", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(16, "Property 12", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(17, "Property 13", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(18, "Property 14", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(19, "Property 15", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(20, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new FreeParking(21, "Free Parking", "Description 6"));
//             board.AddSquare(new Tax(23, "Tax 3", "kamu harus membayar: Rp.300", 300));
//             board.AddSquare(new Property(24, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(25, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(26, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(27, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(28, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(29, "Property 16", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new GoToJail(30, "GoToJail", "Description"));

//             Console.WriteLine("=================SELMAT DATANG DI GAME MOOPOLY=======================");


//             // Input jumlah pemain
//             Console.Write("Masukkan jumlah pemain: ");
//             int jumlahPemain = int.Parse(Console.ReadLine());

//             // Menambahkan pemain
//             for (int i = 0; i < jumlahPemain; i++)
//             {
//                 Console.Write("Masukkan nama pemain ke-{0}: ", i + 1);
//                 string namaPemain = Console.ReadLine();
//                 gameController.AddPlayer(namaPemain);
//             }
//             Console.WriteLine("tekan enter untuk memulai permain");
//             Console.ReadKey();
//             // Memulai permainan
//             gameController.StartGame();

//             // Perulangan untuk giliran pemain
//             bool gameEnd = false;
//             bool diceRolled = false;
//             while (!gameEnd)
//             {

//                 // Mendapatkan pemain yang sedang bermain saat ini
//                 Player activePlayer = gameController.GetActivePlayer();
//                 // Menampilkan giliran pemain
//                 Console.WriteLine("Giliran pemain: " + activePlayer.GetName());

//                 // Melempar dadu
//                 (int dice1Result, int dice2Result, int totalResult) = gameController.RollDice();
//                 Console.WriteLine("Dice 1: " + dice1Result);
//                 Console.WriteLine("Dice 2: " + dice2Result);
//                 Console.WriteLine("Total: " + totalResult);

//                 // Memindahkan pemain
//                 gameController.Move(activePlayer, totalResult);

//                 // Mendapatkan posisi pemain setelah pergerakan
//                 int playerPosition = gameController.GetPlayerPosition(activePlayer);

//                 // Menampilkan posisi pemain
//                 Console.WriteLine("Posisi Pemain: " + playerPosition);

//                 // Mendapatkan square yang sedang ditempati oleh pemain
//                 Square currentSquare = board.GetSquare(playerPosition);

//                 Console.WriteLine("Posisi anda berada di :");
//                 Console.WriteLine(currentSquare.GetName());
//                 Console.WriteLine(currentSquare.GetDescription());

//                 bool turnEnd = false;

//                 while (!turnEnd)
//                 {
//                     // Menampilkan menu
//                     Console.WriteLine("\nPlease Make a Selection :");
//                     Console.WriteLine("1 : Finish Turn");
//                     Console.WriteLine("2 : Your DashBoard");
//                     Console.WriteLine("3 : Purchase the property");
//                     Console.WriteLine("4 : Buy House");
//                     Console.WriteLine("5 : Buy Hotel");
//                     Console.WriteLine("6 : Declare Bankrupt");
//                     Console.WriteLine("7 : Quit Game");
//                     Console.Write("(0-7)>");

//                     int menuSelection = int.Parse(Console.ReadLine());

//                     switch (menuSelection)
//                     {
//                         case 1:
//                             turnEnd = true;
//                             gameController.NextTurn();
//                             break;
//                         case 2:
//                             Console.WriteLine("==== Dashboard ====");
//                             Console.WriteLine("Your position is: " + gameController.GetPlayerPosition(activePlayer));
//                             Console.WriteLine("Your money is: " + gameController.GetPlayerMoney(activePlayer));
//                             Console.WriteLine("Your properties are: ");
//                             List<Property> playerProperties = gameController.GetPlayerProperties(activePlayer);
//                             if (playerProperties != null && playerProperties.Count > 0)
//                             {
//                                 foreach (Property prop in playerProperties)
//                                 {
//                                     Console.WriteLine(prop.GetName());
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You don't have any properties.");
//                             }

//                             break;
//                         case 3:

//                             if (currentSquare is Property property)
//                             {
//                                 gameController.BuyProperty(activePlayer, property);
//                                 Console.WriteLine("Properti berhasil dibeli!");
//                             }
//                             else
//                             {
//                                 Console.WriteLine("Anda tidak berada di properti yang dapat dibeli.");
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
//                                     Console.WriteLine("Failed to purchase a house. Make sure you meet the requirements.");
//                                 }
//                             }
//                             else
//                             {
//                                 Console.WriteLine("You are not on a property where you can buy a house.");
//                             }
//                             break;
//                         case 5:

//                             break;
//                         case 6:

//                             break;
//                         case 7:

//                             gameEnd = true;
//                             break;
//                         default:
//                             Console.WriteLine("Pilihan tidak valid.");
//                             break;
//                     }

//                 }
//             }

//         }
//     }
// }

using System;
using System.Collections.Generic;

namespace monopoly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Membuat objek GameController dan objek Board
            List<Player> players = new List<Player>();  // Inisialisasi daftar pemain
            GameController gameController = new GameController(players, null, null, null, null, null);
            Board board = new Board();
            
            // Membuat objek Player
            Player player = new Player("John");

            // Menambahkan Player dan Board ke GameController
            gameController.AddPlayer(player.GetName());
            gameController.CreateBoard(board);
            Player activePlayer = gameController.GetActivePlayer();

            // Membuat objek Tax didalam board
            Tax tax = new Tax(1, "Tax 3", "kamu harus membayar: Rp.300", 300);
            board.AddSquare(tax);

            // Menentukan posisi player di properti tax
            gameController.SetPlayerPosition(activePlayer, tax);

            // Mencetak jumlah uang player sebelum membayar tax
            int moneyBeforeTax = gameController.GetPlayerMoney(activePlayer);
            Console.WriteLine("Player's money before tax: $" + moneyBeforeTax);

            // Menghandle aksi saat berada di properti tax
            gameController.HandleSquareAction(activePlayer, tax);

            // Mencetak jumlah uang player setelah membayar tax
            int moneyAfterTax = gameController.GetPlayerMoney(activePlayer);
            Console.WriteLine("Player's money after tax: $" + moneyAfterTax);
        }
}
}




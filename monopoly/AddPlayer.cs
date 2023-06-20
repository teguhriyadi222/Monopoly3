// using System;

// namespace monopoly
// {
//     public partial class Program
//     {
//        static async Task AddPlayers()
//         {
//              IBoard board = new Board();
            
//             Jail jail = new Jail(10, "Jail", "Jail square", 70);
//             GameController gameController = new GameController(
//                 new List<Player>(),
//                 board,
//                 jail,
//                 new List<Card>(),
//                 new List<Card>(),
//                 new Dice(6)
//             );
//             Console.Clear();
//             Console.WriteLine("================= SELAMAT DATANG DI GAME MONOPOLY =======================");
//             Console.Write("Masukkan jumlah pemain: ");
//             int jumlahPemain = 0;
//             bool inputValid = false;
//             while (!inputValid)
//             {
//                 if (int.TryParse(Console.ReadLine(), out jumlahPemain) && jumlahPemain > 0)
//                 {
//                     inputValid = true;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Input tidak valid. Masukkan jumlah pemain yang benar!");
//                     Console.Write("Masukkan jumlah pemain: ");
//                 }
//             }

//             for (int i = 0; i < jumlahPemain; i++)
//             {
//                 Console.Write($"Masukkan nama pemain ke-{i + 1}: ");
//                 string namaPemain = Console.ReadLine();

//                 if (!string.IsNullOrEmpty(namaPemain))
//                 {
//                     gameController.AddPlayer(namaPemain);
//                 }
//                 else
//                 {
//                     Console.WriteLine("Input tidak valid. Masukkan nama pemain yang benar!");
//                     i--;
//                 }
//             }
//         }
//     }
// }

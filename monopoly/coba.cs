// using System;
// using System.Collections.Generic;

// namespace monopoly
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             Jail jail = new Jail(10, "Jail", "Jail square", 70);
//             List<Player> players = new List<Player>();  
//             GameController gameController = new GameController(players, new Board(), jail, new List<Card>(), new List<Card>(), new Dice(6));
//             gameController.GoToJailEvent += HandleGoToJailEvent;
//             Board board = new Board();

//             Player player = new Player("John");
//             string player1 = "teguh";
//             string player2 = "yuli";

//             gameController.AddPlayer(player1);
//             gameController.CreateBoard(board);
//             gameController.StartGame();
//             Player activePlayer = gameController.GetActivePlayer();

//             Card card1 = new Card(1, "Salatiga, Description 1", "Salatiga square", TypeCard.Chance, 5, TypeCardCommand.Move);
//             Card card2 = new Card(2, "Sembir, Description 1", "Sembir square", TypeCard.Chance, 3, TypeCardCommand.Move);
//             Card card3 = new Card(3, "Bandung, Description 1", "Bandung square", TypeCard.Chance, 2, TypeCardCommand.Move);

//             gameController.AddCard(card1);
//             gameController.AddCard(card2);
//             gameController.AddCard(card3);

//             int playerPosition = gameController.GetPlayerPosition(activePlayer);
//             Card drawnCard = gameController.DrawRandomCard(activePlayer, TypeCard.Chance);
//             Console.WriteLine("Pemain " + activePlayer.GetName() + " mendapatkan kartu: " + drawnCard.GetDescription());
//             Console.WriteLine("Posisi Pemain: " + playerPosition);

//             gameController.HandleSquareAction(activePlayer, board.GetSquare(card1.GetPosition()));

//             playerPosition = gameController.GetPlayerPosition(activePlayer);
//             Console.WriteLine("Player Position: " + playerPosition);
//         }

//         static void HandleGoToJailEvent(Player player)
//         {
//             Console.WriteLine($"{player.GetName()} has been sent to jail.");
//         }
//     }
// }

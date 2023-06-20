// namespace monopoly
// {
//     public partial class Program
//     {
//         static async Task Main()
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
//             gameController.GoToJailEvent += HandleGoToJailEvent;

//             await Board();
//             await AddPlayers();
//             await Game();

//         }
//     }
// }
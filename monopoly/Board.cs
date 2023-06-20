// namespace monopoly
// {
//     public partial class Program
//     {
//         static async Task Board()
//         {
//             Jail jail = new Jail(10, "Jail", "Jail square", 70);
//             IBoard board = new Board();
//             board.AddSquare(new Start(0, "Start", "Starting point of the board"));
//             board.AddSquare(new Property(1, "Salatiga", "Description 1", 100, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(2, "Bandung", "Description 2", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(3, "Semarang", "Description 3", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(4, "jakarta", "Description 4", 300, 50, 30, 20, TypeProperty.Train));
//             board.AddSquare(new Property(5, "Malang", "Description 5", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(6, "Purworejo", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Tax(7, "Tax 1", "kamu harus membayar: Rp.100", 100));
//             board.AddSquare(new Property(8, "Yogyakarta", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(9, "Bekasi", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(jail);
//             board.AddSquare(new Property(12, "Jambi", "Description 6", 300, 50, 30, 20, TypeProperty.Utility));
//             board.AddSquare(new Tax(13, "Tax 2", "kamu harus membayar: Rp.200", 200));
//             board.AddSquare(new Property(14, "Medan", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(15, "Surabaya", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(16, "Denpasar", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(17, "Ketapang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(18, "Cilacap", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(19, "Cikarang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(20, "Banten", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new FreeParking(21, "Free Parking", "Description 6"));
//             board.AddSquare(new Tax(23, "Tax 3", "kamu harus membayar: Rp.300", 300));
//             board.AddSquare(new Property(24, "JayaPura", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(25, "Palu", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(26, "Kutai", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(27, "Magelang", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(28, "Ambarawa", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new Property(29, "Ungaran", "Description 6", 300, 50, 30, 20, TypeProperty.Residential));
//             board.AddSquare(new GoToJail(30, "GoToJail", "Description"));
//         }
//     }
// }

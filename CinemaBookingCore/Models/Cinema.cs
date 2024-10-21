namespace CinemaBookingCore.Models
{
    public class Cinema
    {
        public string MovieName { get; set; }
        public int Rows { get; set; }
        public int SeatsPerRow { get; set; }
        public int[,] SeatingMap { get; set; }
        public virtual int Available { get; set; }
    }
}

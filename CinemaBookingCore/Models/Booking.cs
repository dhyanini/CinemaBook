namespace CinemaBookingCore.Models
{
    public class Booking
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var bookingObj = (Booking)obj;
            return Row == bookingObj.Row && Col == bookingObj.Col;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using CinemaBookingCore.Validations;
using System;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Services
{
    public class SeatingMapService
    {

        //I am using Singleton pattern here to avoid lot of object allocation although its not a solution requirement
        private static SeatingMapService instance = null;
        private SeatingMapService()
        {
        }

        public static SeatingMapService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SeatingMapService();
                }
                return instance;
            }
        }

        public int[,] BuildSeatingMap(int row, int seatsPerRow)
        {
            int[,] result = new int[row, seatsPerRow];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < seatsPerRow; j++)
                {
                    result[i, j] = AVAILABLE;
                }
            }
            return result;
        }
    }
}
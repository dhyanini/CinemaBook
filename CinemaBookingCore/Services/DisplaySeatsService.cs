using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Services
{
    public class DisplaySeatsService
    {
        //I am using Singleton pattern here to avoid lot of object allocation although its not a solution requirement
        private static DisplaySeatsService instance = null;

        private DisplaySeatsService()
        {
        }

        public static DisplaySeatsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DisplaySeatsService();
                }
                return instance;
            }
        }

        public void Display(int[,] seats)
        {
            int rows = seats.GetLength(0);
            int cols = seats.GetLength(1);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("SCREEN");
            Console.WriteLine("--------------------");
            Console.WriteLine("");

            for (int i = 0; i < rows; i++)
            {
                char alphabetChar = (char)('A' + (rows - i) - 1);
                Console.Write(alphabetChar);

                for (int j = 0; j < cols; j++)
                {
                    if (j < 9)
                        Console.Write("  " + Helper.SeatStateToDisplayMap[seats[i, j]]);
                    else
                        Console.Write("   " + Helper.SeatStateToDisplayMap[seats[i, j]]);
                }
                Console.WriteLine();
            }

            // Print the numbers at the bottom
            Console.Write("  " + " ");

            for (int i = 1; i <= cols; i++)
            {
                Console.Write(i + "  ");
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}

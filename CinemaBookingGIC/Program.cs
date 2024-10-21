using CinemaBookingCore.Utilities;
using System;
using CinemaBookingCore.States;
using CinemaBookingCore.Factories;

namespace CinemaBookingGIC
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            CinemaContext cc = new CinemaContext(StateFactoryCache.Instance.GetState(StateType.SeatingMap, null));
            while (keepRunning)
            {
                var input = Console.ReadLine();
                Console.WriteLine();
                keepRunning = cc.Handle(input);
            }
        }
    }
}

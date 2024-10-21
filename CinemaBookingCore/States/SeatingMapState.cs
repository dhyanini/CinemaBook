

using System;
using static CinemaBookingCore.Utilities.Constants;
using CinemaBookingCore.Utilities;
using CinemaBookingCore.Models;
using CinemaBookingCore.Services;
using CinemaBookingCore.Validations.RuleEngines;
using CinemaBookingCore.Factories;

namespace CinemaBookingCore.States
{
    public class SeatingMapState : State
    {
        public SeatingMapState(RuleEngine ruleEngine) : base(ruleEngine)
        {
        }

        public override bool HandleSpecificToState(string input)
        {
            //Since All validations have passed, we can convert string to int successfully
            string[] splitString = Helper.ProcessString(input);
            int rows = int.Parse(splitString[1]);
            int seatsPerRow = int.Parse(splitString[2]);

            var gicCinema = new Cinema() { MovieName = splitString[0], Rows = rows, SeatsPerRow = seatsPerRow, Available = rows * seatsPerRow };

            gicCinema.SeatingMap = SeatingMapService.Instance.BuildSeatingMap(gicCinema.Rows, gicCinema.SeatsPerRow);

            Context.RowNameToRowNoMap = Helper.BuildRowNameToRowNoMap(rows);
            Context.SetCinema(gicCinema);

            Context.TransitionTo(StateFactoryCache.Instance.GetState(StateType.Options, this));
            return true;
        }
        public override void OnEntry()
        {
            Console.WriteLine(APP_START_MESSAGE);
        }
    }
}

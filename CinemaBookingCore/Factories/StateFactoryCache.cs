using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaBookingCore.Services;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;

namespace CinemaBookingCore.Factories
{
    public class StateFactoryCache
    {
        //I am using Singleton pattern here to avoid lot of object allocation although its not a solution requirement
        private static StateFactoryCache instance = null;
        private readonly Dictionary<StateType, StateFactory> _statesFactoryCache  = new Dictionary<StateType, StateFactory>();
        private readonly Dictionary<(StateType, string), StateType> _stateTransitionTable = new Dictionary<(StateType, string), StateType>();

        private StateFactoryCache()
        {
            //Mapping from StateType to State Specific Factory
            _statesFactoryCache.Add(StateType.SeatingMap, new SeatingMapStateFactory());
            _statesFactoryCache.Add(StateType.Options, new OptionsStateFactory());
            _statesFactoryCache.Add(StateType.StartBooking, new StartBookingStateFactory());
            _statesFactoryCache.Add(StateType.ReserveBooking, new ReserveBookingStateFactory());
            _statesFactoryCache.Add(StateType.CheckBooking, new CheckBookingStateFactory());
            _statesFactoryCache.Add(StateType.Exit, new ExitStateFactory());


            //Initialize State Transition Table
            //State Transition allowed from null to Seating Map
            _stateTransitionTable.Add((StateType.SeatingMap, null), StateType.SeatingMap);

            //State Transition allowed from Seating Map to Options State
            _stateTransitionTable.Add((StateType.Options, typeof(SeatingMapState).Name), StateType.Options);

            //State Transition allowed from Options State to StartBooking/CheckBooking/Exit State
            _stateTransitionTable.Add((StateType.StartBooking, typeof(OptionsState).Name), StateType.StartBooking);
            _stateTransitionTable.Add((StateType.CheckBooking, typeof(OptionsState).Name), StateType.CheckBooking);
            _stateTransitionTable.Add((StateType.Exit, typeof(OptionsState).Name), StateType.Exit);

            //State Transition allowed from StartBooking State to ReserveBooking/Options State
            _stateTransitionTable.Add((StateType.ReserveBooking, typeof(StartBookingState).Name), StateType.ReserveBooking);
            _stateTransitionTable.Add((StateType.Options, typeof(StartBookingState).Name), StateType.Options);

            //State Transition allowed from CheckBooking State to CheckBooking/Options State
            _stateTransitionTable.Add((StateType.CheckBooking, typeof(CheckBookingState).Name), StateType.CheckBooking);
            _stateTransitionTable.Add((StateType.Options, typeof(CheckBookingState).Name), StateType.Options);

            //State Transition allowed from ReserveBooking State to ReserveBooking/Options State
            _stateTransitionTable.Add((StateType.ReserveBooking, typeof(ReserveBookingState).Name), StateType.ReserveBooking);
            _stateTransitionTable.Add((StateType.Options, typeof(ReserveBookingState).Name), StateType.Options);
        }

        public static StateFactoryCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateFactoryCache();
                }
                return instance;
            }
        }

        public State GetState(StateType nextStateType, State currentState)
        {
            var nextState = GetNextState(nextStateType, currentState);
            if ( nextState is StateType.NoState)
                return currentState;

            return _statesFactoryCache[nextStateType].GetState();
        }

        public StateType GetNextState(StateType nextStateType, State currentState)
        {
            var key = (nextStateType, currentState is not null ? currentState.GetType().Name : null);
            if (_stateTransitionTable.ContainsKey(key))
                return _stateTransitionTable[key];

            return StateType.NoState;
        }
    }
}

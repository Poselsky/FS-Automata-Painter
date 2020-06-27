using System;
using System.Collections.Generic;
using FSMachine.Representation;
using FSMachine.State;
using FSMachine.TransitionTable;

namespace FSMachine
{

    public struct CountingState : IState
    {
        int ValState;

        public CountingState(int Val)
        {
            this.ValState = Val;
        }

        public override string ToString()
        {
            return ValState.ToString();
        }
    }

    public class CountingMachine : DeterministicStateMachine<int, CountingState>
    {
        public CountingMachine(CountingState StartWith, HashSet<int> Alphabet, HashSet<CountingState> AllStates, HashSet<CountingState> FiniteStates) : base(StartWith, Alphabet, AllStates, FiniteStates)
        {
        }

        protected override StateChanger<int, CountingState>[] BuildTable()
        {
            return new StateChanger<int, CountingState>[]
            {
                new StateChanger<int, CountingState>(0, new CountingState(0), new CountingState(1)),
                new StateChanger<int, CountingState>(0, new CountingState(1), new CountingState(1)),

                new StateChanger<int, CountingState>(1, new CountingState(0), new CountingState(0)),
                new StateChanger<int, CountingState>(1, new CountingState(1), new CountingState(0)),

                new StateChanger<int, CountingState>(-1, new CountingState(0), new CountingState(-1)),
            };
        }
    }

}

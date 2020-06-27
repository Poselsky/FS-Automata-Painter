using FSMachine.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSMachine.TransitionTable
{
    public struct StateChanger<In, State> where State : struct, IState
    {
        public In Input;
        public State From;
        public State To;

        public StateChanger(In Input, State From, State To)
        {
            this.Input = Input;
            this.From = From;
            this.To = To;
        }
    }
}

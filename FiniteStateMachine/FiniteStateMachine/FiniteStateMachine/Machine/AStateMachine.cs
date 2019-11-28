using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract public class AStateMachine<T>
    {
        //definition says : ordered 5 objects - but in programming makes life harder
        //protected List<object> orderedList = new List<object>();

        List<AState<T>> allStates;
        List<T> finiteInputSymbols;
        Sigma<T> changeStateFunction;
        AState<T> startState;
        List<FinalState<T>> finalStates;
        protected AState<T> currentState { get; set; }

        public AStateMachine(List<AState<T>> allStates, List<T> finiteInputSymbols, Sigma<T> changeStateFunction, AState<T> startState, List<FinalState<T>> finalStates)
        {
            this.allStates = allStates;
            this.finiteInputSymbols = finiteInputSymbols;
            this.changeStateFunction = changeStateFunction;
            this.startState = startState;
            this.finalStates = finalStates;

            currentState = startState;
        }

    }
}

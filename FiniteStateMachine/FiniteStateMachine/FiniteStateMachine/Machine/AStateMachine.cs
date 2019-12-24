using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract public class AStateMachine<T,V>
    {
        //definition says : ordered 5 objects - but in programming makes life harder
        //protected List<object> orderedList = new List<object>();

        protected List<AState<T>> allStates;
        protected List<T> finiteInputSymbols;
        protected Sigma<T,V> changeStateTable;
        protected Sigma<V, T> changeStateTableReversed;
        protected AState<T> startState;
        protected List<AFinalState<T>> finalStates;
        protected AState<T> currentState { get; set; }

        public AStateMachine(List<AState<T>> allStates, List<T> finiteInputSymbols, Sigma<T,V> changeStateTable, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.changeStateTable = changeStateTable;

            SetInConst(allStates, finiteInputSymbols, startState, finalStates);
        }
        public AStateMachine(List<AState<T>> allStates, List<T> finiteInputSymbols, Sigma<V, T> changeStateTable, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.changeStateTableReversed = changeStateTable;

            SetInConst(allStates, finiteInputSymbols, startState, finalStates);
        }


        public void SetInConst(List<AState<T>> allStates, List<T> finiteInputSymbols, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.allStates = allStates;
            this.finiteInputSymbols = finiteInputSymbols;
            this.startState = startState;
            this.finalStates = finalStates;

            currentState = startState;
        }
    }
}

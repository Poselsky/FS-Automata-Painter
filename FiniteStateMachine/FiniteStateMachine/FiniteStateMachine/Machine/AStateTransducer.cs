using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract public class AStateTransducer<T,V>
    {
        //definition says : ordered 6 objects -- again, makes life harder
        //protected List<object> orderedList { get; set; } = new List<object>();

        protected List<AState<T>> allStates;
        protected List<T> finiteInputSymbols;
        public IList<T> outputAlphabet { get; private set; }
        protected Sigma<T,V> changeStateTable;
        protected Sigma<V, T> changeStateTableReversed;
        protected AState<T> startState;
        protected List<AFinalState<T>> finalStates;

        protected AState<T> currentState { get; set; }
        public T this[int i] => outputAlphabet[i];

        public AStateTransducer(List<AState<T>> allStates, List<T> finiteInputSymbols ,IList<T> outputAlphabet, Sigma<T,V> changeStateTable, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.changeStateTable = changeStateTable;

            SetInConst(allStates, finiteInputSymbols, outputAlphabet, startState, finalStates);
        }

        public AStateTransducer(List<AState<T>> allStates, List<T> finiteInputSymbols, IList<T> outputAlphabet, Sigma<V, T> changeStateTable, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.changeStateTableReversed = changeStateTable;

            SetInConst(allStates, finiteInputSymbols, outputAlphabet,  startState, finalStates);
        }

        public void SetInConst(List<AState<T>> allStates, List<T> finiteInputSymbols, IList<T> outputAlphabet, AState<T> startState, List<AFinalState<T>> finalStates)
        {
            this.allStates = allStates;
            this.finiteInputSymbols = finiteInputSymbols;
            this.startState = startState;
            this.finalStates = finalStates;
            this.outputAlphabet = outputAlphabet;

            currentState = startState;
        }

    }
}

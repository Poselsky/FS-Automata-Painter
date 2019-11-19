using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract public class AStateTransducer<T>
    {
        //definition says : ordered 6 objects
        protected List<object> orderedList { get; set; } = new List<object>();

        public IList<T> OutputAlphabet { get => (IList<T>)orderedList[2]; }

        public T this[int i] => OutputAlphabet[i];

        public AStateTransducer(List<AState<T>> allStates, List<T> finiteInputSymbols ,IList<T> outputAlphabet, Sigma<T> changeStateFunction, AState<T> startState, List<FinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);

            orderedList.Add(outputAlphabet);
            
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }
    }
}

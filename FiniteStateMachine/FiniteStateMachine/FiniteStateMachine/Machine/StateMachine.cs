using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract class AStateMachine<T>
    {
        //definition says : ordered 5 objects
        protected List<object> orderedList = new List<object>();

        public AStateMachine(List<AState<T>> allStates, List<T> finiteInputSymbols, Sigma<T> changeStateFunction, AState<T> startState, List<FinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }

    }
}

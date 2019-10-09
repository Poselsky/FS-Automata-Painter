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
        private List<object> orderedList;

        public AStateMachine(List<IState<T>> allStates, T finiteInputSymbols, Sigma<T> changeStateFunction, IState<T> startState, List<IFinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }
        public AStateMachine(List<IState<T>> allStates, List<T> finiteInputSymbols, Sigma<T> changeStateFunction, IState<T> startState, List<IFinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }
        public AStateMachine(List<IState<T>> allStates, T[] finiteInputSymbols, Sigma<T> changeStateFunction, IState<T> startState, List<IFinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }




    }
}

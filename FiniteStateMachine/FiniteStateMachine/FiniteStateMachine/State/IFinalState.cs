using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    interface IFinalState<T>:IState<T>
    {
        bool ReturnIf(Predicate<T> predicate, T predicateInput);
    }
}

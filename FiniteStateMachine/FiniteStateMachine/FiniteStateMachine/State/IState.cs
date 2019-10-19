using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    interface IState<T>
    {
        T Reaction(Func<T, T> action, T funcParameter);
    }
}

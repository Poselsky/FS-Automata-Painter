using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    interface IFunctionStateMachine<T,V>
    {
        T ReturnStateOfMachine(Func<T[], T> functions);
        T ReturnStateOfMachine(Func<T, T> functions);
        T ReturnStateOfMachine(Func<T, V> functions);
        T ReturnStateOfMachine(Func<List<T>, T> functions);
    }
}

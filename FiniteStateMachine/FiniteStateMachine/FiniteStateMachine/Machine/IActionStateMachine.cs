using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    interface IActionStateMachine<T>
    {
        void DoAction(Action<T[]> functions);
        void DoAction(Action<T> functions);
        void DoAction(Action<List<T>> functions);
    }
}

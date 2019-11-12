using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public abstract class AState<T> : IState<T>, IComparable<AState<T>>
    {
        public int ID { get; private set; }

        private static Random ran = new Random();

        public AState()
        {
            int numbersInName = 5;
            string stringifiedID = "";
            for (int i = 0; i < numbersInName; i++)
            {
                stringifiedID += ran.Next(10);
            }
            this.ID = int.Parse(stringifiedID);
        }

        public abstract T Reaction(Func<T, T> action, T funcParameter);

        public override string ToString()
        {
            return this.ID.ToString();
        }

        public int CompareTo(AState<T> other)
        {
            if (this.ID == other.ID)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachine;


namespace FiniteStateMachine.GameOfLife
{
    //Basicaly game logic
    class World : AStateMachine<Cell>, IActionStateMachine<Cell>
    {
        GridOfCells cells;

        public World(List<AState<Cell>> allStates, List<Cell> finiteInputSymbols, Sigma<Cell> changeStateFunction, AState<Cell> startState, List<FinalState<Cell>> finalStates) 
            : base(allStates, finiteInputSymbols, changeStateFunction, startState, finalStates)
        {
            cells = new GridOfCells(4, 4);

        }

        public void DoAction(Action<Cell[]> functions)
        {
            throw new NotImplementedException();
        }

        public void DoAction(Action<Cell> functions)
        {
            throw new NotImplementedException();
        }

        public void DoAction(Action<List<Cell>> functions)
        {
            throw new NotImplementedException();
        }
    }
}

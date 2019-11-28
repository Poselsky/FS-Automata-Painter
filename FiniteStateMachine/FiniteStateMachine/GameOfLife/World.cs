using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachine;


namespace FiniteStateMachine.GameOfLife
{
    //Basicaly game logic
    class World : AStateTransducer<Cell,Cell>
    {
        GridOfCells pastState;
        GridOfCells nowState;
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;

        //FinalStates - > null , what's the final state in this context anyways?
        public World(int x, int y,List<AState<Cell>> allStates, List<Cell> finiteInputSymbols, List<Cell> outputAlphabet, Sigma<Cell> changeStateFunction, AState<Cell> startState) 
            : base(allStates, finiteInputSymbols,outputAlphabet, changeStateFunction, startState, null)
        {
            nowState = new GridOfCells(x, y);
            pastState = new GridOfCells(x, y);
        }


        public Bitmap NextFrame(string stayAliveRule, string reviveRule)
        {
            int[] stayAlive = Array.ConvertAll(stayAliveRule.Split(), int.Parse);
            int[] revive = Array.ConvertAll(reviveRule.Split(), int.Parse);

            Array.Sort(stayAlive);
            Array.Sort(revive);

            if (width > 0 && height > 0)
            {
                Bitmap cellsToMap = new Bitmap(width, height);
                
                nowState.cellsInRow.ForEach((cell) =>
                {
                    AState<Cell> someCell = startState;

                    cell = (Cell)changeStateFunction.ChangeStateFunction(someCell, cell);
                });
                

                //Console.WriteLine(((Cell)startState).alive);

                return null;

            }
            else
            {
                throw new InvalidOperationException("Width and height is not SET!");
            }

        }
    }
}

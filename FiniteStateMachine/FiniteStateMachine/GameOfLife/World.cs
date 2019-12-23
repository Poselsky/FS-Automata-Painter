using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiniteStateMachine;


namespace FiniteStateMachine.GameOfLife
{
    //Basically game logic
    class World : AStateMachine<Cell,bool>
    {
        GridOfCells gridOne;
        GridOfCells gridTwo;
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;

        int x;
        int y;

        //FinalStates - > null , what's the final state in this context anyways?
        public World(List<AState<Cell>> allStates,GridOfCells inputGrid,  Sigma<bool, Cell> changeStateFunction) 
            : base(allStates, inputGrid.cellsInRow, changeStateFunction, null, null)
        {
            x = inputGrid.width + 1;
            y = inputGrid.height + 1;
            gridOne = inputGrid;
            gridTwo = new GridOfCells(x, y, (Cell)allStates.First(c => ((Cell)c).alive), (Cell)(allStates).First(c => !((Cell)c).alive), true);
            

            /*
            //Test Out pulsar
            gridOne.cells[0, 0] = (Cell)allStates[1];
            gridOne.cells[1, 0] = (Cell)allStates[0];
            gridOne.cells[2, 0] = (Cell)allStates[0];
            gridOne.cells[3, 0] = (Cell)allStates[1];
            gridOne.cells[0, 1] = (Cell)allStates[1];
            gridOne.cells[1, 1] = (Cell)allStates[1];
            gridOne.cells[2, 1] = (Cell)allStates[1];
            gridOne.cells[3, 1] = (Cell)allStates[0];
            gridOne.cells[0, 2] = (Cell)allStates[0];
            gridOne.cells[1, 2] = (Cell)allStates[1];
            gridOne.cells[2, 2] = (Cell)allStates[1];
            gridOne.cells[3, 2] = (Cell)allStates[1];
            gridOne.cells[0, 3] = (Cell)allStates[1];
            gridOne.cells[1, 3] = (Cell)allStates[0];
            gridOne.cells[2, 3] = (Cell)allStates[0];
            gridOne.cells[3, 3] = (Cell)allStates[1];
            */

        }


        public Bitmap NextFrame(string stayAliveRule, string reviveRule)
        {
            int[] stayAlive = Array.ConvertAll(stayAliveRule.ToCharArray(), c => (int)Char.GetNumericValue(c));
            int[] revive = Array.ConvertAll(reviveRule.ToCharArray(), c => (int)Char.GetNumericValue(c));

            Array.Sort(stayAlive);
            Array.Sort(revive);

            if (width > 0 && height > 0)
            {
                
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        Tuple<int, int> neighboursInfo = gridOne[i, j];
                        Cell currentCell = gridOne.cells[i, j];

                        if (currentCell.alive)
                        {
                            if(!stayAlive.Any(num => num == neighboursInfo.Item1))
                            {
                                //Dies
                                gridTwo.cells[i, j] = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, false);
                            }
                            else
                            {
                                //stays
                                gridTwo.cells[i, j] = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, true);
                            }

                        }else if (!currentCell.alive)
                        {
                            if(revive.Any(num => num == neighboursInfo.Item1))
                            {
                                //Revives
                                gridTwo.cells[i, j] = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, true);

                            }else
                            {
                                //stays
                                gridTwo.cells[i, j] = (Cell)changeStateTableReversed.ChangeStateFunction(currentCell, false);
                            }
                        }
                    }


                Bitmap res = MapGridToBitmap(gridOne);

                GenerationSwap();
                
                return res; 
            }
            else
            {
                throw new InvalidOperationException("Width and height is not SET!");
            }

        }


        //To use for each syntax easily
        public IEnumerable<Bitmap> NextFrames(string stayAliveRule, string reviveRule, int numberOfIterations)
        {
            for (int i = 0; i < numberOfIterations; i++)
                yield return NextFrame(stayAliveRule, reviveRule);
        }

        private void GenerationSwap()
        {
            //swapping "Generations"
            GridOfCells temp = gridTwo;
            gridTwo = gridOne;
            gridOne = temp;
        }

        private Bitmap MapGridToBitmap(GridOfCells grid)
        {
            Bitmap cellsToMap = new Bitmap(width, height);


            using (Graphics img = Graphics.FromImage(cellsToMap))
            {
                int first = -1;
                int second = -1;
                for (int i = 0; i < width; i += width / x)
                {
                    first++;
                    for (int j = 0; j < height; j+=height/y)
                    {
                        second++;
                        if (grid.cells[first,second].alive)
                        {
                            img.FillRectangle(new SolidBrush(Color.Black), i, j, i+width, j+height);
                        }
                        else
                        {
                            img.FillRectangle(new SolidBrush(Color.White), i, j, i + width, j + height);
                        }
                        img.DrawRectangle(new Pen(Color.Blue), i, j, i + width, j + height);
                        
                    }
                    second = -1;
                }
            }
            return cellsToMap;
        }
    }
}

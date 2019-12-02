using System;
using System.IO;
using System.Collections.Generic;
using FiniteStateMachine;
using FiniteStateMachine.GameOfLife;


namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            Cell alive = new Cell(true);
            Cell dead = new Cell(false);

            List<AState<Cell>> states = new List<AState<Cell>>() { alive, dead };

            //Changing state table
            Sigma<bool, Cell> function = new Sigma<bool, Cell>();

            function.AddFunctionToTable(alive, false, dead)
                    .AddFunctionToTable(alive, true, alive)
                    .AddFunctionToTable(dead, true, alive)
                    .AddFunctionToTable(dead, false, dead);


            GridOfCells gridOne = new GridOfCells(100, 100, alive, dead);

            World world = new World(states, gridOne, function);
            world.width = 600;
            world.height = 600;

            
            int i = 0;


            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }

            foreach (var img in world.NextFrames("23", "3", 100))
            {
                img.Save(new FileStream("images/image"+i+".jpg", FileMode.Create,FileAccess.ReadWrite),System.Drawing.Imaging.ImageFormat.Jpeg);
                i++;
                Console.WriteLine(i);
                img.Dispose();
            }
            
        }
    }

}

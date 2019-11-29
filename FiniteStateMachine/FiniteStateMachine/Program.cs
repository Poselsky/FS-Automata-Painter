using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Sigma<bool, Cell> function = new Sigma<bool, Cell>();

            function.AddFunctionToTable(alive, false, dead)
                .AddFunctionToTable(alive, true, alive)
                .AddFunctionToTable(dead, true, alive)
                .AddFunctionToTable(dead, false, dead);

            World world = new World(30, 30,states,function);
            world.width = 600;
            world.height = 600;

            
            int i = 0;
            foreach (var img in world.NextFrames("", "", 5))
            {
                img.Save(new FileStream("image"+i+".bmp", FileMode.Create,FileAccess.ReadWrite),System.Drawing.Imaging.ImageFormat.Bmp);
                img.Dispose();
                Console.WriteLine(i);
                i++;
            }

        }
    }

}

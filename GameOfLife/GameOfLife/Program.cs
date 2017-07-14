using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game life = new Game();

            GameApp app = new GameApp(life);
            app.StartGame(speed: 1500);



            /// other usage of Game:

            //Game life = new Game(15, 20); // row and column

            //GameApp app = new GameApp(life);
            //app.CursorDown = 10;
            //app.WindowHeight = 40;
            //app.WindowWidth = 50;

            //for (int i = 0; i < life.Grid.GetLength(1); i++)
            //{
            //    life.Grid[5, i] = Cell.Alive;
            //}

            //app.StartGame();
         
        }

    }
}

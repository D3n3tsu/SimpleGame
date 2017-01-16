using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TarasGame2
{
    class Taras
    {

        public int X { get; set; }
        public int Y { get; set; }
        public bool Hitting { get; private set; }

        public Taras(int x, int y)
        {
            X = x;
            Y = y;
            Hitting = false;
        }

        public void Move(Keys key)
        {
            if (key == Keys.Up)
            {
                if (Y > 5)
                    Y -= 10;
            }
            else if (key == Keys.Down)
            {
                if (Y < 280)
                    Y += 10;
            }
            else if (key == Keys.Left)
            {
                if (X > 20)
                    X -= 7;
            }
            else if (key == Keys.Right)
            {
                if (X < 1000)
                    X += 7;
            }
            else if (key == Keys.Space)
            {
                if (!Hitting)
                {
                    Hitting = true;
                    Task.Factory.StartNew(async () => { await Task.Delay(250); Hitting = false; });
                }
            }
        }
        
        
    }
}

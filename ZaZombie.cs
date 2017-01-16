using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarasGame2
{
    class ZaZombie : IComparable<ZaZombie>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Fallen { get; private set; }
        bool stance;
        public int Speed { get; set; }
        Random random;
        public bool Disapears { get; private set; }
        public bool IsGetle { get; private set; }
        int counter;

        public ZaZombie(int x, int y, Random r, int speed)
        {
            this.X = x;
            this.Y = y;
            Fallen = false;
            random = r;
            Speed = speed;
            stance = false;
            Disapears = false;
            if (r.Next(2) == 1) IsGetle = true;
            counter = 0;
        }

        public void GotHit()
        {
            Fallen = true;
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(500);
                Disapears = true;
            });
        }

        public bool Move()
        {
            counter++;
            if (!Fallen)
            {
                X -= Speed;
                int upOrDown = random.Next(2);
                if (upOrDown == 0 && Y > 5)
                {
                    Y -= Speed;
                }
                else if (upOrDown == 1 && Y < 280)
                {
                    Y += Speed;
                }
            }
            if (counter > 5)
            {
                counter = 0;
                if (stance) { stance = false; }
                else { stance = true; }
            }
            return stance;
        }

        public int CompareTo(ZaZombie other)
        {
            if (this.Y > other.Y) return 1;
            else if(this.Y < other.Y) return -1;
            else
            {
                if (this.X > other.X) return 1;
                else if(this.X < other.X) return -1;
                else return 0;
            }
        }
    }
}

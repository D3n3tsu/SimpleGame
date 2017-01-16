using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TarasGame2
{
    delegate void ZombieEventHandler(ZaZombie zomb);
    class Scene
    {
        public event ZombieEventHandler AddZombieEvent;
        public event ZombieEventHandler DeleteZombieEvent;
        
        Random random;
        public Taras taras { get; set; }
        public List<ZaZombie> ZaZombies;
        public bool GameOver { get; private set; }
        int hardness;
        public int ZaZombiesDefeated { get; private set; }

        public Scene()
        {
            ZaZombiesDefeated = 0;
            GameOver = false;
            hardness = 0;
            random = new Random();
            taras = new Taras(5, 100);
            ZaZombies = new List<ZaZombie>();
            ZaZombies.Add(new ZaZombie(1100, random.Next(50, 350), random, 1));
            ZaZombies.AsParallel();
        }

        public bool Go()
        {
            hardness++;
            ZaZombies.Sort();
            if (hardness > 100)
            {
                AddZombie(random.Next(50), 2);
            }
            if (hardness > 500)
            {
                AddZombie(random.Next(25), 4);
            }
            if (hardness > 1000)
            {
                AddZombie(random.Next(10), 6);
            }
            if (hardness > 2000)
            {
                AddZombie(random.Next(5), 8);
            }

            if (ZaZombies.Count > 0)
            {
                for (int i = ZaZombies.Count - 1; i >= 0; i--)
                {
                    if (ZaZombies[i].Disapears || ZaZombies[i].X < -70)
                    {
                        ZaZombies.RemoveAt(i);
                        ZaZombiesDefeated++;
                    }
                }
            }

            foreach (ZaZombie zomb in ZaZombies)
            {
                if (taras.Hitting && HadBeenHit(zomb))
                {
                    if (DeleteZombieEvent != null) DeleteZombieEvent(zomb);
                    zomb.GotHit();
                    
                }
                else if (!taras.Hitting && HadBeenBitten(zomb))
                {
                    GameOver = true;
                }
            }
            return GameOver;
        }

        public void TekilaAttak()
        {
            foreach (var item in ZaZombies)
            {
                item.GotHit();
            }
        }

        void AddZombie(int posibility, int speed)
        {
            if (posibility == 1)
            {
                ZaZombie zomb = new ZaZombie(1100, random.Next(20, 280), random, speed);
                ZaZombies.Add(zomb);
                if(AddZombieEvent!=null) AddZombieEvent(zomb);
            }
        }

        bool HadBeenBitten(ZaZombie zomb)
        {
            if (zomb.Fallen) return false;
            Rectangle tarasa = new Rectangle(taras.X, taras.Y, 50, 160);
            Rectangle zomba = new Rectangle(zomb.X, zomb.Y, 70, 190);
            return tarasa.IntersectsWith(zomba);
        }

        bool HadBeenHit(ZaZombie zomb)
        {
            Rectangle tarasa = new Rectangle(taras.X, taras.Y, 80, 190);
            Rectangle zomba = new Rectangle(zomb.X, zomb.Y, 70, 190);
            return tarasa.IntersectsWith(zomba);
        }
    }
}

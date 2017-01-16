using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace TarasGame2
{
    class Renderer
    {
        Bitmap tekilaOne;
        Bitmap tekilaTwo;
        MainForm form;
        Bitmap track;
        Bitmap[] tarasRunning;
        Bitmap tarasAttacs;
        Bitmap zaZombieOne;
        Bitmap zaZombieTwo;
        Bitmap zaZombieDefeated;
        Bitmap zaZombieGentOne;
        Bitmap zaZombieGentTwo;
        Bitmap zaZombieGentDefeated;
        int frame;
        Scene scene;
        bool tekilaOneOrTwo;
        public int deathZombies { get { return scene.ZaZombiesDefeated; } }
         

        public Renderer(MainForm formToDraw,Scene scn)
        {
            tarasRunning = new Bitmap[4];
            form = formToDraw;
            scene = scn;
            InitImages();
            frame = 0;
            tekilaOneOrTwo = true;
        }

        public void DrawTekila(Graphics g)
        {
            if (tekilaOneOrTwo)
            {
                g.DrawImageUnscaled(tekilaOne, 359, 150);
                tekilaOneOrTwo = false;
            }
            else
            {
                g.DrawImageUnscaled(tekilaTwo, 359, 150);
                tekilaOneOrTwo = true;
            }
        }

        public void DrawTrack(Graphics g)
        {
            frame++;
            g.DrawImage(track, -frame, 0, form.ClientSize.Width * 2, (int)(form.ClientSize.Height * .8));
            
            if(frame >= form.ClientSize.Width)
            {
                frame = 0;
            }
        }

        public void DrawTaras(Graphics g)
        {
            int tarasMoveFrame = 0;

            frame++;

            switch ((frame % 40) / 10)
            {
                case 0: tarasMoveFrame = 0; break;
                case 1: tarasMoveFrame = 1; break;
                case 2: tarasMoveFrame = 2; break;
                case 3: tarasMoveFrame = 3; break;
            }
            if (!scene.taras.Hitting)
            {
                g.DrawImageUnscaled(tarasRunning[tarasMoveFrame], scene.taras.X,scene.taras.Y);
            }
            else
            {
                g.DrawImageUnscaled(tarasAttacs, scene.taras.X, scene.taras.Y);
            }
        }

        public void DrawZaZombies(Graphics g)
        {
            foreach (var zomb in scene.ZaZombies)
            {
                if (zomb.Fallen)
                {
                    if (zomb.IsGetle)
                    {
                        g.DrawImageUnscaled(zaZombieGentDefeated, zomb.X, zomb.Y + 120);
                    }
                    else {
                        g.DrawImageUnscaled(zaZombieDefeated, zomb.X, zomb.Y + 120);
                    }
                }
                else
                {
                    if (zomb.Move())
                    {
                        if (zomb.IsGetle)
                        {
                            g.DrawImageUnscaled(zaZombieGentOne, zomb.X, zomb.Y);
                        }
                        else {
                            g.DrawImageUnscaled(zaZombieOne, zomb.X, zomb.Y);
                        }
                    }
                    else {
                        if (zomb.IsGetle)
                        {
                            g.DrawImageUnscaled(zaZombieGentTwo, zomb.X, zomb.Y);
                        }
                        else
                        {
                            g.DrawImageUnscaled(zaZombieTwo, zomb.X, zomb.Y);
                        }
                    }

                }
            }
        }
         

        void InitImages()
        {
            track = ResizeTrack(Properties.Resources.Trek_Zerkalka, 1089, 638);
            tarasRunning[0] = ResizeImage(Properties.Resources._1111, 80, 190);
            tarasRunning[1] = ResizeImage(Properties.Resources._2222, 80, 190);
            tarasRunning[2] = ResizeImage(Properties.Resources._3333, 80, 190);
            tarasRunning[3] = ResizeImage(Properties.Resources._4444, 80, 190);
            tarasAttacs = ResizeImage(Properties.Resources.Ataka, 90, 190);
            zaZombieOne = ResizeImage(Properties.Resources.Zz1, 70, 190);
            zaZombieTwo = ResizeImage(Properties.Resources.Zz2, 70, 190);
            zaZombieDefeated = ResizeImage(Properties.Resources.Zz2, 70, 190);
            zaZombieDefeated.RotateFlip(RotateFlipType.Rotate90FlipNone);
            zaZombieGentOne= ResizeImage(Properties.Resources.Zz1__1_, 70, 190);
            zaZombieGentTwo = ResizeImage(Properties.Resources.Zz2__1_, 70, 190);
            zaZombieGentDefeated = ResizeImage(Properties.Resources.Zz2__1_, 70, 190);
            zaZombieGentDefeated.RotateFlip(RotateFlipType.Rotate90FlipNone);
            tekilaOne = Properties.Resources._111;
            tekilaTwo = Properties.Resources._222;
        }




        static Bitmap ResizeTrack(Bitmap image, int width, int height)
        {
            Bitmap newImg = new Bitmap(width*2, height);
                using(Graphics g = Graphics.FromImage(newImg))
            {
                g.DrawImage(image, 0, 0, width, height);
                g.DrawImage(image, width-1, 0, width, height);
            }
            return newImg;
        }

        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap newImg = new Bitmap(width , height);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return newImg;
        }
    }
}

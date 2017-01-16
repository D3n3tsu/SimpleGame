using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TarasGame2
{
    public partial class MainForm : Form
    {
        bool gameStarted;
        Renderer renderer;
        Timer operatingTimer ;
        SoundProducer sounder;
        Scene scene;
        bool tekilaRaging;
        int tekilaShots;

        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            gameStarted = false;
            tekilaRaging = false;

            sounder = new SoundProducer();
            sounder.PlayTheme();
        }

        private void startButton_Click_1(object sender, EventArgs e)
        {
            StartGame();
            startButton.Visible = false;
            Controls.Remove(startButton);
            startButton.Dispose();
            sounder.PlayZombies();
        }

        void StartGame()
        {
            scene = new Scene(); 
            operatingTimer = new Timer();
            operatingTimer.Tick += OperatingTimer_Tick;
            operatingTimer.Interval = 20;
            renderer = new Renderer(this, scene);
            operatingTimer.Start();
            gameStarted = true;
            tekilaShots = 3;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (gameStarted)
            {
                renderer.DrawTrack(e.Graphics);
                renderer.DrawZaZombies(e.Graphics);
                renderer.DrawTaras(e.Graphics);
                if (tekilaRaging)
                {
                    renderer.DrawTekila(e.Graphics);
                }
            }
        }

        

        private void OperatingTimer_Tick(object sender, EventArgs e)
        {

            if (scene.Go())
            {
                Graphics grx = CreateGraphics();

                operatingTimer.Stop();
                
                Label defeatLabel = new Label();

                Label yesLabel = new Label();
                yesLabel.Width = 100;
                yesLabel.Height = 30;
                yesLabel.Location = new Point(444, 389);
                yesLabel.Text = "ДА!";
                yesLabel.TextAlign = ContentAlignment.MiddleCenter;
                yesLabel.BackColor = Color.White;
                yesLabel.ForeColor = Color.Black;
                Controls.Add(yesLabel);
                Label noLabel = new Label();
                noLabel.Width = 100;
                noLabel.Height = 30;
                noLabel.Location = new Point(544, 389);
                noLabel.Text = "Я ИСПУГАЛСЯ," + Environment.NewLine + "ПРЕКРАТИ!";
                noLabel.TextAlign = ContentAlignment.MiddleCenter;
                noLabel.BackColor = Color.Red;
                noLabel.ForeColor = Color.Black;
                Controls.Add(noLabel);
                defeatLabel.Width = 200;
                defeatLabel.Height = 150;
                defeatLabel.Location = new Point(444, 269);
                defeatLabel.Text = "ИШО ОДНУ?";
                defeatLabel.ForeColor = Color.White;
                defeatLabel.BackColor = Color.Black;
                defeatLabel.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(defeatLabel);
                defeatLabel.BringToFront();
                yesLabel.BringToFront();
                noLabel.BringToFront();
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(100);
                    grx.DrawString("ТВОЯ ПЕСЕНКА СПЕТА, ТАРАШ!", new Font(new FontFamily("Impact"), 55, FontStyle.Bold), Brushes.Crimson, 50, 100);
                });
                
                yesLabel.Click += new EventHandler((o, ea) =>
                {
                    Controls.Remove(yesLabel);
                    Controls.Remove(noLabel);
                    Controls.Remove(defeatLabel);
                    defeatLabel.Dispose();
                    yesLabel.Dispose();
                    noLabel.Dispose();
                    MessageBox.Show("ТОЧНА?");
                    MessageBox.Show("НУ ТОЧНА?");
                    MessageBox.Show("ТОЧНА-ТОЧНА?");
                    MessageBox.Show("НУ ЛАДНА...");
                    sounder.Dispose(); StartGame();
                });
                noLabel.Click += new EventHandler((o, ea) => { this.Close(); });


            }
            deathLabel.Text = renderer.deathZombies.ToString();
            tekilaShotsLabel.Text = tekilaShots.ToString();
            Invalidate();
        }

        void TekilaBoom()
        {
            if (tekilaShots <= 0) return;
            tekilaRaging = true;
            sounder.PlayTekila();
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(500);
                tekilaRaging = false;
                
                scene.TekilaAttak();
                sounder.PlayZombies();
            });
            tekilaShots--;
        }

        private void TekilaTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            scene.taras.Move( e.KeyCode);

            if (e.KeyCode == Keys.Up)
            {
                upLabel.ForeColor = Color.Red;
            }
            if (e.KeyCode == Keys.Down)
            {
                downLabel.ForeColor = Color.Red;
            }
            if (e.KeyCode == Keys.Right)
            {
                rightLabel.ForeColor = Color.Red;
            }
            if (e.KeyCode == Keys.Left)
            {
                leftLabel.ForeColor = Color.Red;
            }
            if (e.KeyCode == Keys.Space)
            {
                spaceLabel.ForeColor = Color.Red;
            }
            if (e.KeyCode == Keys.T)
            {
                tekilaLabel.ForeColor = Color.Red;
                TekilaBoom();
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                upLabel.ForeColor = Color.Black;
            }
            if (e.KeyCode == Keys.Down)
            {
                downLabel.ForeColor = Color.Black;
            }
            if (e.KeyCode == Keys.Right)
            {
                rightLabel.ForeColor = Color.Black;
            }
            if (e.KeyCode == Keys.Left)
            {
                leftLabel.ForeColor = Color.Black;
            }
            if (e.KeyCode == Keys.Space)
            {
                spaceLabel.ForeColor = Color.Black;
            }
            if (e.KeyCode == Keys.T)
            {
                tekilaLabel.ForeColor = Color.Black;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(sounder!=null) sounder.Dispose();
        }
    }
}

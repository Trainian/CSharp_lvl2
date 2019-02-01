using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace HomeWork
{
    static class Game
    {
        public static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static BaseObject[] objs;
        private static List<Bullet> bullet = new List<Bullet>();
        private static List<Asteroid> asteroids = new List<Asteroid>();
        private static HealingBox[] healingBoxs;
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        static Timer timer = new Timer();
        public static int GamePoints = 0;
        static Ship ship = new Ship(new Point(10,400),new Point(5,5),new Size(10,10));
        public static bool NewGame = true;
        private static int _maxAsteroids = 3;

        static public void Init(Form form)
        {
            if (form.Height >= 1000|| form.Width >= 1000)
            {
                throw new ArgumentOutOfRangeException();
            }
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Ship.MessageEnd += Finish;
            Ship.JournalMessager += Journals.ConsoleJournal;
            Ship.JournalMessager += Journals.FileJournal;
            BaseObject.JournalMessager += Journals.ConsoleJournal;
            BaseObject.JournalMessager += Journals.FileJournal;
            form.KeyDown += Form_KeyDown;

            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                if (bullet.Count <= 10)
                {
                    bullet.Add(new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                ship.Up();
            }
            if (e.KeyCode == Keys.Down)
            {
                ship.Down();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
            {
                obj.Draw();
            }
            foreach (Asteroid ast in asteroids)
            {
                if (ast != null) ast.Draw();
            }

            foreach (Bullet b in bullet)
            {
                b.Draw();
            }

            foreach (HealingBox box in healingBoxs)
            {
                if (box != null)
                {
                    box.Draw();
                }
            }
            ship.Draw();
            buffer.Graphics.DrawString("Energy: " + ship.Energy, SystemFonts.DefaultFont,Brushes.White,0,0);
            buffer.Graphics.DrawString("Points: " + GamePoints, SystemFonts.DefaultFont, Brushes.White, 0, 12);
            buffer.Render();
        }

        static public void Update()
        {
            if (GamePoints > 20)
            {
                ship.Win();
            }
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }

            for (int i = 0; i < bullet.Count; i++)
            {
                bullet[i].Update();
                if (bullet[i].afterGameLockation)
                {
                    bullet.RemoveAt(i);
                    i--;
                }
            }

            if (asteroids.Count <= 0)
            {
                _maxAsteroids++;
                for (int j = 0; j < _maxAsteroids; j++)
                {
                    asteroids.Add(new Asteroid());
                }
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Update();
                    for (int j = 0; j < bullet.Count; j++)
                    {
                        if (i >= 0 && bullet[j].Collision(asteroids[i]))
                        {
                            SystemSounds.Hand.Play();
                            asteroids.RemoveAt(i);
                            i--;
                            bullet.RemoveAt(j);
                            j--;
                            GamePoints++;
                            continue;
                        }
                    }

                    if (i >= 0 && asteroids.Count > 0 && ship.Collision(asteroids[i]))
                    {
                        ship.EnergyLow(asteroids[i].Power);
                        SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0)
                        {
                            ship.Die();
                        }
                        asteroids.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < healingBoxs.Length; i++)
            {
                if (healingBoxs[i] != null)
                {
                    healingBoxs[i].Update();
                    if (healingBoxs[i].Collision(ship))
                    {
                        ship.EnergyUp(rnd.Next(1,3));
                        healingBoxs[i] = null;
                    }
                }
                else
                {
                    healingBoxs[i] = new HealingBox();
                }
            }
        }

        static public void Load()
        {
            objs = new BaseObject[30];
            for (int i = 0; i < objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                objs[i] = new Star();
            }

            for (int i = 0; i < _maxAsteroids; i++)
            {
                asteroids.Add(new Asteroid());
            }

            healingBoxs = new HealingBox[1];
            for (int i = 0; i < healingBoxs.Length; i++)
            {
                healingBoxs[i] = new HealingBox();
            }
        }

        public static void Finish(EnumEnds e)
        {
            timer.Stop();
            switch (e)
            {
                case EnumEnds.Loss:
                    buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                    break;
                case EnumEnds.Win:
                    buffer.Graphics.DrawString("You Win!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                    break;
            }
            buffer.Render();
        }

    }
}

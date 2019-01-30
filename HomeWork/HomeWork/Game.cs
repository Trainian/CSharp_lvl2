using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
        private static Bullet bullet;
        private static Asteroid[] asteroids;
        private static HealingBox[] healingBoxs;
        public static Random rnd = new Random();
        static Timer timer = new Timer();
        public static int GamePoints = 0;
        static Ship ship = new Ship(new Point(10,400),new Point(5,5),new Size(10,10));

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

            Ship.MessageDie += Finish;
            Ship.JournalMessager += ConsoleJournal;
            Ship.JournalMessager += FileJournal;
            BaseObject.JournalMessager += ConsoleJournal;
            form.KeyDown += Form_KeyDown;

            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                bullet = new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
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

            if (bullet != null)
            {
                bullet.Draw();
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
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }

            if (bullet != null)
            {
                bullet.Update();
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Update();
                    if (bullet != null && bullet.Collision(asteroids[i]))
                    {
                        SystemSounds.Hand.Play();
                        asteroids[i] = null;
                        bullet = null;
                        GamePoints++;
                        continue;
                    }

                    if (ship.Collision(asteroids[i]))
                    {
                        ship.EnergyLow(rnd.Next(1,10));
                        SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0)
                        {
                            ship.Die();
                        }
                        asteroids[i] = null;
                    }
                }
                else
                {
                    int r = rnd.Next(15, 50);
                    asteroids[i] = new Asteroid(new Point(1000, Game.rnd.Next(0, Game.Height - 50)), new Point(-r / 3, r), new Size(r, r));

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
                    int r = rnd.Next(10, 15);
                    healingBoxs[i] = new HealingBox(new Point(2500, rnd.Next(0, Game.Height - r)), new Point(-10, 25), new Size(r, r));
                }
            }
        }

        static public void Load()
        {
            objs = new BaseObject[30];
            for (int i = 0; i < objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                objs[i] = new Star(new Point(1000, Game.rnd.Next(0 + 3,Game.Height - 3)), new Point(-r,r),new Size(3,3));
            }

            asteroids = new Asteroid[3];
            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(15, 50);
                asteroids[i] = new Asteroid(new Point(1000, Game.rnd.Next(0 ,Game.Height - 50)), new Point(-r/3, r),  new Size(r,r));
            }

            healingBoxs = new HealingBox[1];
            for (int i = 0; i < healingBoxs.Length; i++)
            {
                int r = rnd.Next(10, 15);
                healingBoxs[i] = new HealingBox(new Point(2500, rnd.Next(0,Game.Height - r)), new Point(-10,25), new Size(r,r));
            }
        }

        public static void Finish()
        {
            timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,FontStyle.Underline), Brushes.White,200,100);
            buffer.Render();
        }

        public static void ConsoleJournal(string str, object obj)
        {
            if (obj is ConsoleColor)
            {
                Console.ForegroundColor = (ConsoleColor) obj;
            }
            Console.WriteLine($"{DateTime.Now} - {str}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void FileJournal(string str, object obj)
        {
            //Запись в файл.
        }
    }
}

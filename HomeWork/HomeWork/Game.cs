using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace HomeWork
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; private set; }
        public static int Height { get; private set; }

        private static BaseObject[] _objs;
        private static List<Bullet> bullet = new List<Bullet>();
        private static List<Asteroid> asteroids = new List<Asteroid>();
        private static HealingBox[] _healingBoxs;
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        private static Timer _timer = new Timer();
        public static int GamePoints;
        private static Ship _ship = new Ship(new Point(10,400),new Point(5,5),new Size(10,10));
        public static bool NewGame = true;
        private static int _maxAsteroids = 3;

        public static void Init(Form form)
        {
            if (form.Height >= 1000|| form.Width >= 1000)
            {
                throw new ArgumentOutOfRangeException();
            }
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Ship.MessageEnd += Finish;
            Ship.JournalMessager += Journals.ConsoleJournal;
            Ship.JournalMessager += Journals.FileJournal;
            BaseObject.JournalMessager += Journals.ConsoleJournal;
            BaseObject.JournalMessager += Journals.FileJournal;
            form.KeyDown += Form_KeyDown;

            _timer.Interval = 50;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                if (bullet.Count <= 10)
                {
                    bullet.Add(new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                _ship.Up();
            }
            if (e.KeyCode == Keys.Down)
            {
                _ship.Down();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            foreach (Asteroid ast in asteroids)
            {
                ast?.Draw();
            }

            foreach (Bullet b in bullet)
            {
                b.Draw();
            }

            foreach (HealingBox box in _healingBoxs)
            {
                box?.Draw();
            }
            _ship.Draw();
            Buffer.Graphics.DrawString("Energy: " + _ship.Energy, SystemFonts.DefaultFont,Brushes.White,0,0);
            Buffer.Graphics.DrawString("Points: " + GamePoints, SystemFonts.DefaultFont, Brushes.White, 0, 12);
            Buffer.Render();
        }

        private static void Update()
        {
            if (GamePoints > 20)
            {
                _ship.Win();
            }
            foreach (BaseObject obj in _objs)
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
                        }
                    }

                    if (i >= 0 && asteroids.Count > 0 && _ship.Collision(asteroids[i]))
                    {
                        _ship.EnergyLow(asteroids[i].Power);
                        SystemSounds.Asterisk.Play();
                        if (_ship.Energy <= 0)
                        {
                            _ship.Die();
                        }
                        asteroids.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (int i = 0; i < _healingBoxs.Length; i++)
            {
                if (_healingBoxs[i] != null)
                {
                    _healingBoxs[i].Update();
                    if (_healingBoxs[i].Collision(_ship))
                    {
                        _ship.EnergyUp(rnd.Next(1,3));
                        _healingBoxs[i] = null;
                    }
                }
                else
                {
                    _healingBoxs[i] = new HealingBox();
                }
            }
        }

        public static void Load()
        {
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length; i++)
            {
                _objs[i] = new Star();
            }

            for (int i = 0; i < _maxAsteroids; i++)
            {
                asteroids.Add(new Asteroid());
            }

            _healingBoxs = new HealingBox[1];
            for (int i = 0; i < _healingBoxs.Length; i++)
            {
                _healingBoxs[i] = new HealingBox();
            }
        }

        private static void Finish(EnumEnds e)
        {
            _timer.Stop();
            switch (e)
            {
                case EnumEnds.Loss:
                    Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                    break;
                case EnumEnds.Win:
                    Buffer.Graphics.DrawString("You Win!", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
                    break;
            }
            Buffer.Render();
        }

    }
}

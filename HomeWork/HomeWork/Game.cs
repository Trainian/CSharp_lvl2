using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace HomeWork
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        static public int Width { get; set; }
        static public int Height { get; private set; }

        static public BaseObject[] objs;

        static public Bullet bullet;

        static public Asteroid[] asteroids;

        static public Random rnd = new Random();

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
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;
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
                ast.Draw();
            }
            bullet.Draw();
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }

            foreach (Asteroid ast in asteroids)
            {
                ast.Update();
                if (ast.Collision(bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                }
            }
            bullet.Update();
        }

        static public void Load()
        {
            objs = new BaseObject[30];
            bullet = new Bullet(new Point(0,200), new Point(20,0), new Size(4,1));
            asteroids = new Asteroid[3];
            for (int i = 0; i < objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                objs[i] = new Star(new Point(1000, Game.rnd.Next(0,Game.Height)), new Point(-r,r),new Size(3,3));
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(15, 50);
                asteroids[i] = new Asteroid(new Point(1000, Game.rnd.Next(0,Game.Height)), new Point(-r/3, r),  new Size(r,r));
            }
            //int x = 0;
            //for (int i = 0; i < objs.Length/3; i++)
            //{
            //    //objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i,-i), new Size(10,10));
            //}
            //for (int i = objs.Length/3; i < objs.Length/3*2; i++)
            //{
            //    objs[i] = new Star(new Point(600, i * 20), new Point(- i, 0), new Size(5, 5));
            //}
            //for (int i = objs.Length/3*2; i < objs.Length; i++)
            //{
            //    objs[i] = new Planet(new Point(600, i+x), new Point(-i,0), new Size(i-15,i-15));
            //    x += 80;
            //}
        }
    }
}

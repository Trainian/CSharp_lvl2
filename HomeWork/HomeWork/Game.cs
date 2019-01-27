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

        static Game()
        { }

        static public void Init(Form form)
        {
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
            buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj.Update();
            }
        }

        static public void Load()
        {
            objs = new BaseObject[30];
            int x = 0;
            for (int i = 0; i < objs.Length/3; i++)
            {
                objs[i] = new BaseObject(new Point(600, i * 20), new Point(-i,-i), new Size(10,10));
            }

            for (int i = objs.Length/3; i < objs.Length/3*2; i++)
            {
                objs[i] = new Star(new Point(600, i * 20), new Point(- i, 0), new Size(5, 5));
            }
            for (int i = objs.Length/3*2; i < objs.Length; i++)
            {
                objs[i] = new Planet(new Point(600, i+x), new Point(-i,0), new Size(i,i));
                x += 80;
            }
        }
    }
}

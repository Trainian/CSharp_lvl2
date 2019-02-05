using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public Star() : base()
        {
            int r = Game.rnd.Next(5, 50);
            pos = new Point(1000, Game.rnd.Next(0 + 3, Game.Height - 3));
            dir = new Point(-r, r);
            size = new Size(3, 3);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, pos.X,pos.Y,pos.X + size.Width, pos.Y+ size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
            }
        }
    }
}

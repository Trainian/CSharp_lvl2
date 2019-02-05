using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    class HealingBox : BaseObject
    {
        public HealingBox(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public HealingBox() :base()
        {
            int r = Game.rnd.Next(10, 15);
            pos = new Point(2500, Game.rnd.Next(0, Game.Height - r));
            dir = new Point(-10, 25);
            size = new Size(r, r);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.Green,pos.X,pos.Y,size.Width,size.Height);
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

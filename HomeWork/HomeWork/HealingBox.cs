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

        public override void Draw()
        {
            Game.buffer.Graphics.FillRectangle(Brushes.Green,pos.X,pos.Y,size.Width,size.Height);
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

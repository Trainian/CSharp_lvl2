using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    class Bullet : BaseObject
    {
        public bool afterGameLockation = false;
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed,pos.X,pos.Y,size.Width,size.Height);
        }

        public override void Update()
        {
            if (pos.X > Game.Width)
            {
                afterGameLockation = true;
            }
            pos.X += dir.X;
        }
    }
}

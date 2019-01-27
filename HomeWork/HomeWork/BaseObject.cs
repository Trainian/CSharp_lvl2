using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    abstract class BaseObject : ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public Rectangle Rect
        {
            get{return new Rectangle(pos,size);}

        }

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        abstract public void Draw();
        //Image img = (Bitmap)Properties.Resources.ResourceManager.GetObject("star");
        //Image rImg = new Bitmap(img ?? throw new InvalidOperationException(), size.Width*3, size.Height*3);
        //Game.buffer.Graphics.DrawImage(rImg, pos.X, pos.Y);
        //Game.buffer.Graphics.DrawEllipse(Pens.White,pos.X,pos.Y,size.Width,size.Height);

        public abstract void Update();
        //{
        //    pos.X = pos.X + dir.X;
        //    pos.Y = pos.Y + dir.Y;
        //    if (pos.X < 0) dir.X = -dir.X;
        //    if (pos.X > Game.Width) dir.X = -dir.X;
        //    if (pos.Y < 0) dir.Y = -dir.Y;
        //    if (pos.Y > Game.Height) dir.Y = -dir.Y;
        //}

        public bool Collision(ICollision obj)
        {
            if (obj.Rect.IntersectsWith(this.Rect))
            {
                this.pos = new Point(1000, Game.rnd.Next(0,Game.Height));
                (obj as Bullet).pos = new Point(0, Game.rnd.Next(0, Game.Height));
                return true;
            }
            else return false;
        }

    }
}

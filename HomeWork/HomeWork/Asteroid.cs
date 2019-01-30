using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    class Asteroid : BaseObject,IComparable<Asteroid>
    {
        private int Power { get; set; } = 3;
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = Game.rnd.Next(1, 4);
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawEllipse(Pens.White,pos.X,pos.Y,size.Width,size.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0)
            {
                pos.X = Game.Width + size.Width;
            }
        }

        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (this.Power > obj.Power)
            {
                return 1;
            }

            if (this.Power < obj.Power)
            {
                return -1;
            }
            else return 0;
        }
    }
}

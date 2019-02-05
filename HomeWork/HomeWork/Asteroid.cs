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
        public int Power { get; set; } = 3;
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = Game.rnd.Next(1, 4);
        }

        public Asteroid() : base()
        {
            int r = Game.rnd.Next(15, 50);
            Power = Game.rnd.Next(1, 4);
            pos.X = 1000;
            pos.Y = Game.rnd.Next(0, Game.Height - 50);
            dir.X = -r / 3;
            dir.Y = r;
            size.Width = r;
            size.Height = r;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White,pos.X,pos.Y,size.Width,size.Height);
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

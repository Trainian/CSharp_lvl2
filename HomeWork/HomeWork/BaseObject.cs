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
        public delegate void Message(EnumEnds e);
        public delegate void Journal(string str, object obj);

        internal Point pos;
        protected Point dir;
        protected Size size;
        public static event Journal JournalMessager;


        public Rectangle Rect => new Rectangle(pos,size);

        protected BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        protected BaseObject()
        {
        }

        public abstract void Draw();

        public abstract void Update();

        public virtual bool Collision(ICollision obj)
        {
            if (obj.Rect.IntersectsWith(this.Rect))
            {
                if (JournalMessager != null)
                {
                    if (this is Bullet)
                    {
                        JournalMessager($"Попадание по астеройду !", new object());
                    }
                    if (this is Ship)
                    {
                        JournalMessager($"Cтолкнвение корабля !", ConsoleColor.Yellow);
                    }
                    if (this is HealingBox)
                    {
                        JournalMessager($"Ура, аптечка !", ConsoleColor.Green);
                    }
                }
                return true;
            }
            else return false;
        }

    }
}

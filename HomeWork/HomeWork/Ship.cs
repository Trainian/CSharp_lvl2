using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork
{
    class Ship : BaseObject
    {
        private int _energy = 10;
        public int Energy => _energy;

        public static event Message MessageEnd;
        public new static event Journal JournalMessager;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public void EnergyLow(int n)
        {
            _energy -= n;
            if (JournalMessager != null) JournalMessager($"Корабль получил повреждения : {n}", ConsoleColor.Yellow);
        }

        public void EnergyUp(int n)
        {
            _energy += n;
            if (JournalMessager != null) JournalMessager($"Корабль получил здоровье : {n}", ConsoleColor.Green);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat,pos.X,pos.Y,size.Width,size.Height);
        }

        public override void Update()
        {
            Draw();
        }

        public void Up()
        {
            if (pos.Y > 0)
            {
                pos.Y = pos.Y - dir.Y;
            }
        }

        public void Down()
        {
            if (pos.Y < Game.Height)
            {
                pos.Y = pos.Y + dir.Y;
            }
        }

        public void Die()
        {
            if (MessageEnd != null)
            {
                MessageEnd(EnumEnds.Loss);
                if (JournalMessager != null) JournalMessager("Корабль погиб", ConsoleColor.DarkRed);
            }
        }

        public void Win()
        {
            if (MessageEnd != null)
            {
                MessageEnd(EnumEnds.Win);
                if (JournalMessager != null) JournalMessager("Вы победили", ConsoleColor.DarkGreen);
            }
        }
    }
}

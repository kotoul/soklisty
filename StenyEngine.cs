using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace wpf
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum Direction
    {
        UP, RIGHT, DOWN, LEFT
    }

    public class Stena
    {
        public Point StartPoint { get; set; }
        public Direction Direction { get; set; }
        public int Length { get; set; }
        public bool IsSplit { get; set; }

        public Lista Lista { get; set; }

        private Stena() { }

        public List<Stena> split(int listaLen)
        {
            int splits = (int) Math.Ceiling((double) Length / listaLen);
            
            List<Stena> steny = Enumerable.Range(1, splits).Select(i => new Stena() { StartPoint = StartPoint, Direction = Direction, Length = listaLen, IsSplit = true }).ToList();
            steny.Aggregate((s1, s2) => { s2.StartPoint = s1.GetEndPoint(); return s2; });
            steny.Last().Length = Length - (splits - 1) * listaLen;

            IsSplit = true;
            return steny;
        }

        public List<Stena> reset()
        {
            IsSplit = false;
            return new List<Stena>() { this };
        }

        public Point GetEndPoint()
        {
            //nekontroluje zaporny hodnoty!
            switch (Direction)
            {
                case Direction.UP:
                    return new Point(StartPoint.X, StartPoint.Y - Length);
                case Direction.RIGHT:
                    return new Point(StartPoint.X + Length, StartPoint.Y);
                case Direction.DOWN:
                    return new Point(StartPoint.X, StartPoint.Y + Length);
                case Direction.LEFT:
                    return new Point(StartPoint.X - Length, StartPoint.Y);
                default:
                    throw new InvalidOperationException("Direction not set");
            }
        }

        public static Stena create(int len, Direction dir)
        {
            return new Stena() { Length = len, Direction = dir };
        }

        public Stena startAt(Point start)
        {
            StartPoint = start;
            return this;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", Length, IsSplit ? "*" : "");
        }
    }

    public class StenyEngine
    {
        public List<Stena> Steny { get; set; }

        public StenyEngine()
        {
            Steny = new List<Stena>();
            Steny.AddRange(fillStartingPoints(MojePokoje.pokoj()));
            Steny.AddRange(fillStartingPoints(MojePokoje.obyvak()));
            Steny.AddRange(fillStartingPoints(MojePokoje.loznice()));
            Steny.AddRange(fillStartingPoints(MojePokoje.kuchyne()));
        }

        private List<Stena> fillStartingPoints(List<Stena> steny) 
        {
            steny.Aggregate((s1, s2) => {
                if (s1.StartPoint != null)
                {
                    if (s2.StartPoint != null)
                    {
                        return s2;
                    }
                    s2.StartPoint = s1.GetEndPoint();
                }
                else
                {
                    throw new InvalidOperationException("Nesedi starty sten");
                }
                return s2;
            });
            return steny;
        }
    }
}

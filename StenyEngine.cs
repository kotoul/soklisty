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

        public List<Stena> Children { get; set; }
        private Lista lista;

        private Stena() { }

        public void setLista(Lista lista)
        {
            this.lista = lista;
        }

        public List<Lista> getListy()
        {
            return Children == null ? new List<Lista>() { lista } : Children.Select(s => s.lista).ToList();
        }

        public List<Stena> split(int listaLen)
        {
            List<Stena> children = new List<Stena>() {
                new Stena() { StartPoint = StartPoint, Direction = Direction, Length = listaLen },
                new Stena() { StartPoint = StartPoint, Direction = Direction, Length = Length - listaLen }
            };
            Children = children;
            return children;
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
            return Length.ToString();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf
{
    public class MojePokoje
    {
        private static Point startP = new Point(10, 10);
        private static Point startK = new Point(startP.X + 244 + 20, startP.Y);
        private static Point startO = new Point(startK.X + 281 + 20, startK.Y);
        private static Point startL = new Point(startO.X + 332 + 18 + 10, startO.Y);

        public static List<Stena> pokoj()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(40, Direction.RIGHT).startAt(startP),
                Stena.create(8, Direction.UP),
                Stena.create(147, Direction.RIGHT),
                Stena.create(8, Direction.DOWN),
                Stena.create(57, Direction.RIGHT),
                Stena.create(389, Direction.DOWN),
                Stena.create(15, Direction.LEFT), //u zarubne
                Stena.create(382, Direction.DOWN).startAt(startP),
                Stena.create(131, Direction.RIGHT),
                Stena.create(7, Direction.DOWN),
                Stena.create(15, Direction.RIGHT) //u zarubne
            });
        }

        public static List<Stena> obyvak()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(56, Direction.RIGHT).startAt(startO),
                Stena.create(8, Direction.UP),
                Stena.create(221, Direction.RIGHT),
                Stena.create(8, Direction.DOWN),
                Stena.create(55, Direction.RIGHT),
                Stena.create(130, Direction.DOWN), //u zarubne loznice
                Stena.create(320, Direction.DOWN).startAt(new Point(startO.X+332, startO.Y+130+80)), //u zarubne loznice
                Stena.create(332, Direction.LEFT),
                Stena.create(40, Direction.UP), //u zarubne chodba
                Stena.create(81, Direction.DOWN).startAt(startO),
                Stena.create(15, Direction.LEFT),
                Stena.create(15, Direction.RIGHT).startAt(new Point(startO.X-15, startO.Y+81+80)),
                Stena.create(250, Direction.DOWN) //u zarubne chodba
            });
        }

        public static List<Stena> loznice()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(330, Direction.RIGHT).startAt(startL),
                Stena.create(135, Direction.DOWN),
                Stena.create(10, Direction.RIGHT),
                Stena.create(149, Direction.DOWN),
                Stena.create(10, Direction.LEFT),
                Stena.create(98, Direction.DOWN),
                Stena.create(319, Direction.LEFT),
                Stena.create(167, Direction.UP),
                Stena.create(32, Direction.LEFT), //u zarubne
                Stena.create(78, Direction.DOWN).startAt(startL),
                Stena.create(18, Direction.LEFT),
                Stena.create(52, Direction.DOWN) //u zarubne
            });
        }

        public static List<Stena> kuchyne()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(24, Direction.RIGHT).startAt(startK),
                Stena.create(10, Direction.UP),
                Stena.create(147, Direction.RIGHT),
                Stena.create(10, Direction.DOWN),
                Stena.create(96, Direction.RIGHT),
                Stena.create(81, Direction.DOWN),
                Stena.create(14, Direction.RIGHT),
                Stena.create(75, Direction.UP).startAt(new Point(startK.X + 24 + 147 + 96, startK.Y + 81 + 75 + 80)),
                Stena.create(14, Direction.RIGHT)
            });
        }

        private static List<Stena> fillStartingPoints(List<Stena> steny)
        {
            steny.Aggregate((s1, s2) =>
            {
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

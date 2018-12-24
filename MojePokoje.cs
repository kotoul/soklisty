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
                Stena.create(39, Direction.RIGHT).startAt(startP),
                Stena.create(7, Direction.UP),
                Stena.create(142, Direction.RIGHT),
                Stena.create(6, Direction.DOWN),
                Stena.create(53, Direction.RIGHT),
                Stena.create(384, Direction.DOWN),
                Stena.create(13, Direction.LEFT), //u zarubne
                Stena.create(375, Direction.DOWN).startAt(startP),
                Stena.create(128, Direction.RIGHT),
                Stena.create(7, Direction.DOWN),
                Stena.create(17, Direction.RIGHT) //u zarubne
            });
        }

        public static List<Stena> obyvak()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(52, Direction.RIGHT).startAt(startO),
                Stena.create(6, Direction.UP),
                Stena.create(217, Direction.RIGHT),
                Stena.create(7, Direction.DOWN),
                Stena.create(53, Direction.RIGHT),
                Stena.create(116, Direction.DOWN), //u zarubne loznice
                Stena.create(317, Direction.DOWN).startAt(new Point(startO.X+332, startO.Y+130+80)), //u zarubne loznice
                Stena.create(323, Direction.LEFT),
                Stena.create(41, Direction.UP), //u zarubne chodba
                Stena.create(78, Direction.DOWN).startAt(startO),
                Stena.create(16, Direction.LEFT),
                Stena.create(16, Direction.RIGHT).startAt(new Point(startO.X-15, startO.Y+81+80)),
                Stena.create(242, Direction.DOWN) //u zarubne chodba
            });
        }

        public static List<Stena> loznice()
        {
            return fillStartingPoints(new List<Stena>() {
                Stena.create(325, Direction.RIGHT).startAt(startL),
                Stena.create(134, Direction.DOWN),
                Stena.create(8, Direction.RIGHT),
                Stena.create(144, Direction.DOWN),
                Stena.create(8, Direction.LEFT),
                Stena.create(94, Direction.DOWN),
                Stena.create(312, Direction.LEFT),
                Stena.create(160, Direction.UP),
                Stena.create(28, Direction.LEFT),
                Stena.create(14, Direction.UP), //u zarubne
                Stena.create(70, Direction.DOWN).startAt(startL),
                Stena.create(16, Direction.LEFT),
                Stena.create(44, Direction.DOWN) //u zarubne
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

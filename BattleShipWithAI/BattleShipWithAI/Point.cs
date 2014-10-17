using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    public enum PointStatus
    {
        Empty,
        Ship,
        Hit,
        Miss
    }

    class Point
    {
        //porperties
        public int x { get; set; }
        public int y { get; set; }
        public PointStatus status { get; set; }

        //constructor
        public Point()
        {
            this.x = 0;
            this.y = 0;
            this.status = PointStatus.Empty;
        }

        public Point(int x, int y, PointStatus status)
        {
            this.x = x;
            this.y = y;
            this.status = status;
        }

        //no methods
    }
}

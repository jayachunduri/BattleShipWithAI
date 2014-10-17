using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    public enum ShipType
    {
        Carrier = 5,
        Battleship = 4,
        Cruiser = 3,
        Submarine = 3,
        Minesweeper = 2
    }

    class Ship
    {
        //properties
        public ShipType type { get; set; }
        public List<Point> occupiedPoint { get; set; }
        public int length { get; set; }
        public bool IsDistroyed
        {
            get
            {
                return occupiedPoint.All(x => x.status == PointStatus.Hit);
            }
        }

        //consturctor
        public Ship(ShipType typeOfShip)
        {
            this.occupiedPoint = new List<Point>();
            this.type = typeOfShip;
            this.length = (int)type;
        }



    }
}

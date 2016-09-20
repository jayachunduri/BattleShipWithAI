using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    abstract class BattleShipPlayer
    {
        //properties
        public string name;
        public int score { get; set; }
        public Point point;
        public List<Point> pointsHit;
        public Grid ocean;
        //public List<Ship> ships;

        //Constructors
        public BattleShipPlayer()
        {
            
            pointsHit = new List<Point>();
            this.point = new Point();
            this.ocean = new Grid();
            this.ocean.PlaceShip(new Ship(ShipType.Submarine));
            this.ocean.PlaceShip(new Ship(ShipType.Minesweeper));
            this.ocean.PlaceShip(new Ship(ShipType.Cruiser));
            this.ocean.PlaceShip(new Ship(ShipType.Carrier));
            this.ocean.PlaceShip(new Ship(ShipType.Battleship));
            //this.ocean.ListOfShips.Add(new Ship(ShipType.Submarine));
             //foreach (var ship in this.ocean.ListOfShips)
             //{
             //    this.ocean.PlaceShip(ship);
             //}
	        
		 
	
                             
        }

        public BattleShipPlayer(string name)
        {
            score = 100;
            pointsHit = new List<Point>();
            this.name = name;
            this.point = new Point();
            this.ocean = new Grid();
            
        }

        //memebers
        public abstract void GenerateCoordinates();
        public abstract bool GoodCoordinates();
    }


}

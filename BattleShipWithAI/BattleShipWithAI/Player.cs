using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    class Player: BattleShipPlayer
    {
        //As Player inherits all the pulic properties
        //not declaring them

        //as a child class don't inherit constructor of parent class
        //declaring the constructors
        public Player(string name)
        {
            this.name = name;
           
        }

        //overriding the methods from parent class
        public override void GenerateCoordinates()
        {
            
            int x = -1, y = 10;
            //ask user to enter x and y coordinates
            while (x < 0 || x > 9)
            {
                string xException = "";
                while (xException == "" || !("0123456789".Contains(xException)))
                {
                    Console.WriteLine("Enter X coordinate");
                    xException = Console.ReadLine();
                }
                //after xException gets a good value from user. Then pracing it to int and putting in x
                x = int.Parse(xException);
            }

            while (y < 0 || y > 9)
            {
                string yException = "";
                while (yException == "" || !("0123456789".Contains(yException)))
                {
                    Console.WriteLine("Enter Y coordinate");
                    yException = Console.ReadLine();
                }
                //after yException gets a good value from user. Then pracing it to int and putting in y
                y = int.Parse(yException);
            }

            //while (!GoodCoordinates())
            //{
            //    Console.WriteLine("You already hit that point");
            //    GenerateCoordinates();
            //}
            point.x = x;
            point.y = y;
            pointsHit.Add(point);


        }

        //verifies if they are good coordinates
        public override bool GoodCoordinates()
        {
            return !(pointsHit.Any(x => x.x == this.point.x && x.y == this.point.y));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    class Program
    {
        public static bool won = false;

        static void Main(string[] args)
        {
            Player player;
            Computer comp;
           
            Greet();
            comp = new Computer();

            Console.Write("Enter Your Name: ");
            player = new Player(Console.ReadLine());
            PlayGame(player, comp);
        }

        static void Greet()
        {
            Console.WriteLine("Welcome to Battle Ship");
           
        }

        //logic for playing the game
        public static void PlayGame(Player player, Computer comp)
        {
            while (!won)
            {
                    //1st player's turn
                    //verify if playe's ships are still alive
                    //if (!player.ocean.AllShipsDestroyed)
                    //{
                    //    Console.WriteLine("Player's ocean");
                    //    player.ocean.DisplayOcean();
                    //    player.GenerateCoordinates();
                    //    player.ocean.Target(player.point, player.score);
                    //    player.ocean.DisplayOcean();
                    //    Console.WriteLine("comp turn");
                    //    Console.ReadKey();
                    //    Console.Clear();

                    //}
                    //computer's turn
                    //verify if all ships are alive

                //(player.ocean.AllShipsDestroyed) ||

                if (!comp.ocean.AllShipsDestroyed)
                {
                    Console.WriteLine("Computer's ocean");
                    //comp.ocean.DisplayOcean();
                    comp.GenerateCoordinates();
                    comp.pointsHit.Add(comp.point);
                    var x = comp.score;
                    comp.ocean.Target(comp.point, ref x);
                    comp.score = x;
                    comp.ocean.DisplayOcean();
                    Console.WriteLine("Your turn");
                    Console.ReadKey();
                    
                    Console.Clear();
                }
                else if ( (comp.ocean.AllShipsDestroyed))
                {
                    won = true;
                }
                }

                //we are out of loop means sobody one
            //if (player.ocean.AllShipsDestroyed)
            //{
            //    Console.WriteLine("You lost");
            //}
            //else
            {
                Console.WriteLine("Congratulations you WON!");
            }
            Console.WriteLine("Your Score: " + player.score);
            Console.WriteLine("Computer's Score: " + comp.score);
            }

        //public void Target(BattleShipPlayer player)
        //{
        //    int x = player.point.x;
        //    int y = player.point.y;

        //    if (player.ocean[x, y].status == PointStatus.Ship)
        //    {
        //        player.ocean[x, y].status = PointStatus.Hit;
        //        player.point.status = PointStatus.Hit;

        //    }
        //    else if (player.ocean[x, y].status == PointStatus.Empty)
        //    {
        //        player.ocean[x, y].status = PointStatus.Miss;
        //        player.point.status = PointStatus.Miss;
        //        player.score -= 1;
        //    }
        //}
        
    }
}

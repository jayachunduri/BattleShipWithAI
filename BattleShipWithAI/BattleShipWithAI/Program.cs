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
            Console.WriteLine();
        }

        //logic for playing the game
        public static void PlayGame(Player player, Computer comp)
        {
            while (!won)
            {
                while ((!player.ocean.AllShipsDestroyed) || (!comp.ocean.AllShipsDestroyed))
                {
                    //1st player's turn
                    Console.Clear();
                    //Console.WriteLine("Press Enter to proceed with your turn");
                    //Console.ReadKey();
                    Console.WriteLine("Ocean with Computer's ships in, as of now");
                    player.ocean.DisplayOcean(false);
                    
                    player.GenerateCoordinates();
                    player.ocean.Target(player.point);
                    
                    //Console.Clear();
                    Console.WriteLine("Ocean with enemy's ships in, after your current hit");
                    player.ocean.DisplayOcean(false);

                    //verify if player won
                    if (player.ocean.AllShipsDestroyed)
                    {
                        won = true;
                        EndGame(player, comp);
                        break;
                    }
                    else
                    {
                        //Now computers turn
                        Console.WriteLine("Press Enter to proceed for computer's turn");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Ocean with your ships in. You can see your ships, but not the computer");
                        Console.WriteLine("Ocean before computers hit");
                        comp.ocean.DisplayOcean(true);
                        comp.GenerateCoordinates();
                        comp.pointsHit.Add(comp.point);
                        
                        comp.ocean.Target(comp.point);
                        
                        Console.WriteLine("Ocean after computer's hit");
                        comp.ocean.DisplayOcean(true);
                        Console.WriteLine("press enter to proceed for your turn");
                        Console.ReadKey();
                        //verify if computer won
                        if (comp.ocean.AllShipsDestroyed)
                        {
                            won = true;
                            EndGame(player, comp);
                            break;
                        }
                    }


                }
                
                

            }
        }

        public static void EndGame(Player pl, Computer comp)
        {
            Console.Clear();
            if (pl.ocean.AllShipsDestroyed)
            {
                Console.WriteLine("Congratulations you WON!");
            }
            else 
            {
                Console.WriteLine("You lost! Better luck next time!");
            }
                Console.WriteLine("Your Score: " + pl.score);
                Console.WriteLine("Ocean with Computer's ships");
                pl.ocean.DisplayOcean(true);

                Console.WriteLine("Computer's Score: " + comp.score);
                Console.WriteLine("Ocen with your ships in");
                comp.ocean.DisplayOcean(true);
                Console.ReadKey();

                return;
        }
    }
}

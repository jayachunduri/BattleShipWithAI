using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    public enum PlaceShipDirection
    {
        Horizontal = 1,
        Vertical = 2
    }

    class Grid
    {
        //properties
        public Point[,] Ocean { get; set; }
        public List<Ship> ListOfShips { get; set; }
        public bool AllShipsDestroyed
        {
            get
            {
                return ListOfShips.All(x => x.IsDistroyed);
            }
        }
        //public int Score { get; set; }
        
        public Grid()
        {
            //initialize ocean
            Ocean = new Point[10, 10];
            ListOfShips = new List<Ship>();

            //loop to initialize all points
            for (int x = 0; x < 10; x++) //x coordinate
            {
                for (int y = 0; y < 10; y++) //y coordinate
                {
                    Ocean[x, y] = new Point(x, y, PointStatus.Empty);
                }
            }
            

        }

        //method to place ships
        public void PlaceShip(Ship shipToPlace)
        {
            bool yesShip = false;
            int startx = 0, starty = 0;
            int direction = 0;

            //need to generate a set of x-coordinate and y-coordinate, so that they are valid
            while (!yesShip)
            {
                Random rng = new Random();

                //random coordinates
                startx = rng.Next(0, 10);
                starty = rng.Next(0, 10);

                //random direction
                direction = rng.Next(1, 3);
                yesShip = CanPlaceShip(shipToPlace, (PlaceShipDirection)direction, startx, starty);
            }

            //make sure there is no ship in that part of ocean
            for (int i = 0; i < shipToPlace.length; i++)
            {
                //change the status of that point in ocean (from empty) to ship
                Ocean[startx, starty].status = PointStatus.Ship;
                //add that point to ship's occupaid points
                shipToPlace.occupiedPoint.Add(Ocean[startx, starty]);

                if ((PlaceShipDirection)direction == PlaceShipDirection.Horizontal)
                {
                    startx++;
                }
                else
                    starty++;
            }
            //add that ship to list of ships in the ocean
            ListOfShips.Add(shipToPlace);
        }

        //method to display grid to user
        public void DisplayOcean(bool showShips)
        {
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("    0  1  2  3  4  5  6  7  8  9  X");
            Console.WriteLine("===================================");
            Console.ResetColor();

            //loop for y axis
            for (int y = 0; y < 10; y++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("{0}||", y);
                Console.ResetColor();

                for (int x = 0; x < 10; x++)
                {
                    if (Ocean[x, y].status == PointStatus.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[ ]");
                        Console.ResetColor();
                    }
                    else if ((Ocean[x, y].status == PointStatus.Ship) && showShips)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("[S]");
                        Console.ResetColor();
                    }
                    else if ((Ocean[x, y].status == PointStatus.Ship) && !showShips)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("[ ]");
                        Console.ResetColor();
                    }
                    else if (Ocean[x, y].status == PointStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[X]");
                        Console.ResetColor();
                    }
                    else if (Ocean[x, y].status == PointStatus.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[O]");
                        Console.ResetColor();
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Y||");
            Console.ResetColor();
        }

        //Hit or Miss: determine the logic for hits or misses
        public void Target(Point point)
        {
            if (Ocean[point.x, point.y].status == PointStatus.Ship)
            {
                Ocean[point.x, point.y].status = PointStatus.Hit;
                point.status = PointStatus.Hit;
                
            }
            else if (Ocean[point.x, point.y].status == PointStatus.Empty)
            {
                Ocean[point.x, point.y].status = PointStatus.Miss;
                point.status = PointStatus.Miss;
                
            }
        }



        //function returns False if there is a ship in that part of ocean or reached end of ocean
        public bool CanPlaceShip(Ship shipToPlace, PlaceShipDirection direction, int startx, int starty)
        {
            //Make sure there is not already ship in that part of ocean
            //Also can't place a ship out of a ocean
            for (int i = 0; i < shipToPlace.length; i++)
            {
                if (Ocean[startx, starty].status == PointStatus.Ship)
                    return false;

                if (direction == PlaceShipDirection.Vertical)
                {
                    starty++;
                    if (starty > 9)
                        return false;
                }
                else if (direction == PlaceShipDirection.Horizontal)
                {
                    startx++;
                    if (startx > 9)
                        return false;
                }

            }
            return true;

        }

        //function to add high score to the data base
        //public static void AddHighScoreToDB()
        //{
        //    //create a connection to the data base
        //    //JayaEntities db = new JayaEntities();

        //    //create an instance of class High Score(from data base table)
        //    //HighScore currentScore = new HighScore();

        //    //added all information to current score object
        //    currentScore.DateCreated = DateTime.Now;
        //    currentScore.Game = "Battle Ship";
        //    currentScore.Score = Score;
        //    Console.Clear();
        //    Console.WriteLine("\n\nEnter your name");
        //    currentScore.Name = Console.ReadLine();

        //    //add it to the data base
        //    db.HighScores.Add(currentScore);

        //    //commit the changes to the data base
        //    db.SaveChanges();
        //}

        ////function to get high score from the data base table HighScores and display to the user
        //public static void DisplayHighScore()
        //{
        //    //clear the console before displaying the high score
        //    Console.Clear();
        //    Console.WriteLine("Your score is: " + Score);
        //    Console.WriteLine("Battle Ship High Scores");
        //    Console.WriteLine("**********************");

        //    //create a connection to the data base
        //    JayaEntities db = new JayaEntities();

        //    //get the top high scores from data base table using db object
        //    //List<HighScore> highScoreList = db.HighScores.Where(x => x.Name == "Battle Ship").OrderByDescending(x => x.Score).Take(10).ToList();

        //    //Display that list to the user
        //    foreach (var item in db.HighScores.Where(x => x.Game == "Battle Ship").OrderByDescending(x => x.Score).Take(10))
        //    {
        //        //Console.WriteLine("{0}, {1} - {2}", highScoreList.IndexOf(highScore) + 1, highScore.Name, highScore.Score);
        //        Console.WriteLine(item.Name + " " + item.Score);
        //    }
        //}

    }

}


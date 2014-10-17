using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipWithAI
{
    public enum Direction 
    {
        Right,
        Left,
        Up,
        Down
    }

    class Computer: BattleShipPlayer
    {
        //some extra properties not in the parent class
        public Point shipStart;
        public Direction directionOfShip;
        //public Random rng;
        public bool shipFound;
        public bool directionFound;
        public bool shipCompleted;
        public bool isFirstDirection;
        public bool isSecondDirection;
        public Direction secondDirection;
        
        public bool isRight;
        public bool isLeft;
        public bool isUp;
        public bool isDown;

        public Point rightPoint;
        public Point leftPoint;
        public Point upPoint;
        public Point downPoint;

        public bool isRandomCoordinates;
        //constructors
        public Computer()
        {
            this.name = "Computer";
            this.shipFound = false;
            this.directionFound = true;
            this.shipCompleted = true;
            //this.rng = new Random();
            this.point.status = PointStatus.Miss;

            //below variables are to finish the ship's both directions
            this.isFirstDirection = false;
            this.isSecondDirection = false;

            //to check if we verified in that direction for 2nd hit
            this.isRight = false;
            this.isLeft = false;
            this.isUp = false;
            this.isDown = false;

            this.isRandomCoordinates = true;
        }
        
        //Generate Coordinates
        public override void GenerateCoordinates()
        {
           
            if (!shipFound)
            {
                FindShip();
                //return;
            }
               
            if (!directionFound)
            {
                FindDirection();
                //return;
            }
                 
            //we find the ship and direction
            //now we need to finish the ship
            if (!shipCompleted)
            {
                directionFound = true;
                if(isFirstDirection && point.status == PointStatus.Hit)
                {
                    if(IntelligentCoordinates(directionOfShip))
                        return;
                    else
                    {
                        isFirstDirection = false;
                        isSecondDirection = true;
                        point = shipStart;
                        directionOfShip = secondDirection;
                        if (IntelligentCoordinates(secondDirection))
                            return;
                        //pointsHit.Add(point);
                        else
                            SankTheShip();
                        return;
                    }
                }
                
               else if((point.status == PointStatus.Miss) && (isFirstDirection))
                {
                    isFirstDirection = false;
                    isSecondDirection = true;
                    point = shipStart;
                    directionOfShip = secondDirection;
                    if(IntelligentCoordinates(secondDirection))
                        return;
                   else
                    {
                        SankTheShip();
                        
                    }
                }
                else if (isSecondDirection)
                {
                    if (point.status == PointStatus.Hit)
                    {
                        if (IntelligentCoordinates(directionOfShip))
                            return;
                        else
                        {

                            SankTheShip();
                        }
                    }
                    else if (point.status == PointStatus.Miss)
                    {
                        SankTheShip();
                    }
                }
                
                
            }
                    
        }
        

        //Function to generate random coordinates
        public void RandomCoordinates()
        {
            Random rng = new Random();
            int x = rng.Next(0, 10);
            int y = rng.Next(0, 10);
            if (GoodCoordinates(x, y))
            {
                this.point.x = x;
                this.point.y = y;
                //pointsHit.Add(point);
            }
            else
                RandomCoordinates();
        }

        //Function to check if coordinates are good to use
        public override bool GoodCoordinates()
        {
            return !(pointsHit.Any(x => x.x == this.point.x && x.y == this.point.y));
        }

        public bool GoodCoordinates(int x, int y)
        {
            return !((pointsHit.Any(po => po.x == x && po.y == y)) || (x < 0) || (x > 9) || (y < 0) || (y > 9));
        }

        //generate intelligent coordinates
        public bool IntelligentCoordinates(Direction direction)
        {
            int x, y;
            if(direction == Direction.Right)
            {
                x = point.x + 1;
                y = point.y;
                if(GoodCoordinates(x, y))
                {
                    point.x = x;
                    //pointsHit.Add(point);
                    return true;
                }

            }
            else if(direction == Direction.Left)
            {
                x = point.x - 1;
                y = point.y;
                if(GoodCoordinates(x, y))
                {
                    point.x = x;
                    //pointsHit.Add(point);
                    return true;
                }
            }
            else if(direction == Direction.Up)
            {
                x = point.x;
                y = point.y - 1;
                if(GoodCoordinates(x, y))
                {
                    point.y = y;
                    //pointsHit.Add(point);
                    return true;
                }
            }
            else if(direction == Direction.Down)
            {
                x = point.x;
                y = point.y + 1;
                if(GoodCoordinates(x, y))
                {
                    point.y = y;
                    //pointsHit.Add(point);
                    return true;
                }
            }
            return false;
        }

        //Function to find ship
        public void FindShip()
        {
            int x, y;
            if((isRandomCoordinates) && !(point.status == PointStatus.Hit))
            {
                RandomCoordinates();
                return;
            }
            else if(point.status == PointStatus.Hit) //means we found the ship..now need to generate 4 surronding points
            {
                isRandomCoordinates = false;
                shipStart = point;
                shipFound = true;
                directionFound = false;
                //shipCompleted = false;
                //right point
                x = point.x + 1;
                y = point.y;
                if (GoodCoordinates(x, y))
                    rightPoint = new Point(x, y, PointStatus.Miss);
                else
                    rightPoint = null;
                //left point
                x = point.x - 1;
                y = point.y;
                if (GoodCoordinates(x, y))
                    leftPoint = new Point(x, y, PointStatus.Miss);
                else
                    leftPoint = null;
                //up point
                x = shipStart.x;
                y = shipStart.y - 1;
                if (GoodCoordinates(x, y))
                {
                    upPoint = new Point(x, y, PointStatus.Miss);
                }
                else
                    upPoint = null;

                //down point
                x = shipStart.x;
                y = shipStart.y + 1;
                if (GoodCoordinates(x, y))
                {
                    downPoint = new Point(x, y, PointStatus.Miss);
                }
                else
                    downPoint = null;
            }
          return;
        }

        //Function to find the direction
        public void FindDirection()
        {
            if (point.status == PointStatus.Hit)
            {
                if (isRight)
                {
                    directionOfShip = Direction.Right;
                    secondDirection = Direction.Left;
                    isFirstDirection = true;
                    shipCompleted = false;
                    isRight = false;
                    //secondDirection = Direction.Left;
                    return;
                    
                }
                else if (isLeft)
                {
                    directionOfShip = Direction.Left;
                    isSecondDirection = true;
                    isFirstDirection = false;
                    shipCompleted = false;
                    isLeft = false;
                    return;
                    //secondDirection = Direction.Right;
                    //point = leftPoint;

                }
                else if (isUp)
                {
                    directionOfShip = Direction.Up;
                    secondDirection = Direction.Down;
                    isFirstDirection = true;
                    shipCompleted = false;
                    isUp = false;
                    return;
                }
                else if (isDown)
                {
                    directionOfShip = Direction.Down;
                    isFirstDirection = false;
                    isSecondDirection = true;
                    shipCompleted = false;
                    isDown = false;
                    //secondDirection = Direction.Up;
                    return;
                }
            }
            
                if (rightPoint != null)
                {
                    point = rightPoint;
                    //pointsHit.Add(point);
                    rightPoint = null;
                    isRight = true;
                    return;
                }
                else if (leftPoint != null)
                {
                    point = leftPoint;
                    //pointsHit.Add(point);
                    leftPoint = null;
                    isRight = false;
                    isLeft = true;
                    return;
                }

                else if (upPoint != null)
                {
                    point = upPoint;
                    //pointsHit.Add(point);
                    upPoint = null;
                    isRight = false;
                    isLeft = false;
                    isUp = true;
                    return;
                }
                else if (downPoint != null)
                {
                    point = downPoint;
                    //pointsHit.Add(point);
                    downPoint = null;
                    isRight = false;
                    isLeft = false;
                    isUp = false;
                    isDown = true;
                    return;
                }


        }
           
            //at this point we have ship starting point and also hit the right point
            //if right is a miss then need to check all 3 surronding points for 2nd hit

            //if(point.status == PointStatus.Hit) //means direction is right
            //    {
            //        directionFound = true;
            //        directionOfShip = Direction.Right;
            //        isRight = true;
            //        secondDirection = Direction.Left;
            //        isFirstDirection = true;
            //        if (IntelligentCoordinates(Direction.Right))
            //            return;
            //        else //either reached end or already hit that point
            //        {
            //            directionOfShip = Direction.Left;
            //            isLeft = true;
            //            isSecondDirection = true;
            //            isFirstDirection = false;
            //            //secondDirection = Direction.Right;
            //            point = leftPoint;
            //            return;
            //        }

            //    }
            //else //check for other directions.
            //{

            //}
            //if(rightPoint != null)
            //    {
            //        point = rightPoint;
            //        pointsHit.Add(point);
            //        rightPoint = null;
            //        isRight = true;
            //        return;
            //    }

        

        //Finish that ship
        public void SankTheShip()
        {
            shipCompleted = true;
            directionFound = true;
            shipFound = false;
            //this.rightCompleted = false;
            //this.leftCompleted = false;
            //this.upCompleted = false;
            //this.downCompleted = false;
            this.isFirstDirection = false;
            this.isSecondDirection = false;
            this.isRight = false;
            this.isLeft = false;
            this.isUp = false;
            this.isDown = false;
            this.isRandomCoordinates = true;
            RandomCoordinates();
        }
        
}
}

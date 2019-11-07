using System;
using System.Collections.Generic;
using System.Drawing;

namespace Snake
{
    enum Direction { Right = 1, Up = 2, Left = 3, Down = 4 };

    class Snake
    {

        private List<Point> body;
        private Point headPos;
        private Point tailPos;
        private int currentDirection = (int)Direction.Down;
        private Point oldPosition1;

        public Snake()
        {
            headPos.Y = 5;
            headPos.X = 5; // Arbitrary head & tail initial position.
            tailPos.X = 3;
            tailPos.Y = 5;
            Point midPos = new Point();
            midPos.X = headPos.X - 1;
            midPos.Y = headPos.Y;
            body = new List<Point>() { headPos, midPos, tailPos };
        }

        public void Eat()
        {
            this.LengthenSnake();
        }

        // Adds 1 segment to snake at the tail.
        private void LengthenSnake()
        {
                Point newTail = new Point();
                newTail = oldPosition1;
                body.Add(newTail);
        }

        public void MoveSnake()
        {
            this.Move();
        }

        // Returns string "right", "up", "left", or "down" indicating the direction of head travel
        public string GetDirection()
        {
            return (Enum.GetName(typeof(Direction), currentDirection));
        }

        // ChangeDirection takes the string "right", "up", "left", or "down" indicating the new direction of the snake. The string input is related to an int
        // per the enum above. 
        public void ChangeDirection(string vector)
        {
            if (vector == Enum.GetName(typeof(Direction), 1)) currentDirection = (int)Direction.Right;
            else if (vector == Enum.GetName(typeof(Direction), 2)) { currentDirection = (int)Direction.Up; }
            else if (vector == Enum.GetName(typeof(Direction), 3)) { currentDirection = (int)Direction.Left; }
            else if (vector == Enum.GetName(typeof(Direction), 4)) { currentDirection = (int)Direction.Down; }
        }

        // This method returns a List of Points containing snake body coordinates. **Index 0 is the snake head.**
        public List<Point> GetSnakePosition()
        {
            return (body);
        }

        // Move(): Store current headPos, get current direction & change Point coordinate of headPos accordingly. The second unit of snake will then assume 
        // the previous Point coordinate of the headPos, the third unit of snake will then assume the previous position of the second unit, and so on, until
        // all parts of the snake have advanced. 
        private void Move()
        {
            //Console.WriteLine("headPos: " + headPos + "  body[0]: " + body[0]); //Test line
            //Console.WriteLine(Enum.GetName(typeof(Direction), currentDirection)); //Test line

            switch (Enum.GetName(typeof(Direction), currentDirection))
            {
                case ("Right"):
                    {
                        headPos.X += 1;
                        break;
                    }
                case ("Up"):
                    {
                        headPos.Y -= 1; // Up is in the -Y direction on the console axis.
                        break;
                    }
                case ("Left"):
                    {
                        headPos.X -= 1;
                        break;
                    }
                case ("Down"):
                    {
                        headPos.Y += 1; // Down is in the +Y direction on the console axis.
                        break;
                    }
            }

            /*Point*/ oldPosition1 = body[0];
            body[0] = headPos;
            //Console.WriteLine("headPos: " + headPos + "  body[0]: " + body[0]); // Test line.

            for (int index = 1; index < body.Count; index++) // **
            {
                Point oldPosition2 = body[index];
                body[index] = oldPosition1;
                oldPosition1 = oldPosition2;
            }
        }
    }
}

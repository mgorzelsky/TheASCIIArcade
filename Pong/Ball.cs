using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Timers;

namespace Pong
{
    enum BallDirection { UpLeft, DownLeft, UpRight, DownRight };
    class Ball
    {
        private Point ballPosition;
        private BallDirection ballDirection;
        Random rnd = new Random();
        private readonly Timer timer = new Timer(170);
        private int angle;
        private int counter;

        //Set a random starting direction and set up the timer used to handle ball movement on object creation.
        public Ball()
        {
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }

        public Point BallPosition { get => ballPosition; }

        public GameItems[,] CollisionObjects { get; set; }

        public void Serve()
        {
            ballPosition = new Point(Game.Width / 2, Game.Height / 2);
            ballDirection = (BallDirection)rnd.Next(0, 4);
            timer.Start();
            timer.Interval = 170;
            counter = 1;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Move();

            if (counter % 100 == 0)
                timer.Interval -= 10;
            counter++;
        }

        //On timed event update the ballposition. The angle is based on a randomly generated direction that changes 
        //each time the ball bounces off a paddle. Angle stays constant through wall bounces.
        private void Move()
        {
            if (angle % 2 == 0)     //flatter angler
            {
                switch (ballDirection)
                {
                    case (BallDirection.UpLeft): //up-left
                        ballPosition.Offset(-2, 1);
                        if (ballPosition.X < 0)
                            ballPosition.X = 0;
                        break;
                    case (BallDirection.DownLeft): //down-left
                        ballPosition.Offset(-2, -1);
                        if (ballPosition.X < 0)
                            ballPosition.X = 0;
                        break;
                    case (BallDirection.UpRight): //up-right
                        ballPosition.Offset(2, 1);
                        if (ballPosition.X > Game.Width - 1)
                            ballPosition.X = Game.Width - 1;
                        break;
                    case (BallDirection.DownRight): //down-right
                        ballPosition.Offset(2, -1);
                        if (ballPosition.X > Game.Width - 1)
                            ballPosition.X = Game.Width - 1;
                        break;
                }
            }
            else     //sharper angle
            {
                switch (ballDirection)
                {
                    case (BallDirection.UpLeft): //up-left
                        ballPosition.Offset(-1, 1);
                        if (ballPosition.X < 0)
                            ballPosition.X = 0;
                        break;
                    case (BallDirection.DownLeft): //down-left
                        ballPosition.Offset(-1, -1);
                        if (ballPosition.X < 0)
                            ballPosition.X = 0;
                        break;
                    case (BallDirection.UpRight): //up-right
                        ballPosition.Offset(1, 1);
                        if (ballPosition.X > Game.Width - 1)
                            ballPosition.X = Game.Width - 1;
                        break;
                    case (BallDirection.DownRight): //down-right
                        ballPosition.Offset(1, -1);
                        if (ballPosition.X > Game.Width - 1)
                            ballPosition.X = Game.Width - 1;
                        break;
                }
            }
            CollisionCheck();
        }

        //When collision is detected to a wall or paddle the bounce method is called to change the angle
        //of movement. If it is a paddle bounce the angle variable is randomized to choose either a flat
        //or sharp angle.
        public void Bounce(GameItems surface)
        {
            if (surface == GameItems.Paddle)
            {
                switch (ballDirection)
                {
                    case (BallDirection.UpLeft):                  //up-left TO
                        ballDirection = BallDirection.UpRight;    //up-right
                        break;
                    case (BallDirection.DownLeft):                //down-left TO
                        ballDirection = BallDirection.DownRight;  //down-right
                        break;
                    case (BallDirection.UpRight):                 //up-right TO
                        ballDirection = BallDirection.UpLeft;     //up-left
                        break;
                    case (BallDirection.DownRight):               //down-right TO
                        ballDirection = BallDirection.DownLeft;   //down-left
                        break;
                }
                angle = rnd.Next(0, 100);
            }
            if (surface == GameItems.Wall)
            {
                switch (ballDirection)
                {
                    case (BallDirection.UpLeft):                 //up-left TO
                        ballDirection = BallDirection.DownLeft;  //down-left
                        break;
                    case (BallDirection.DownLeft):               //down-left TO
                        ballDirection = BallDirection.UpLeft;    //up-left
                        break;
                    case (BallDirection.UpRight):                 //up-right TO
                        ballDirection = BallDirection.DownRight;  //down-right
                        break;
                    case (BallDirection.DownRight):               //down-right TO
                        ballDirection = BallDirection.UpRight;    //up-right
                        break;
                }
            }
        }
        //Ball handles its own collision check to see if it has hit a wall, paddle, or goal area.
        private void CollisionCheck()
        {
            //Does the ball hit a paddle?
            if (CollisionObjects[BallPosition.X, BallPosition.Y] == GameItems.Paddle)
                Bounce(GameItems.Paddle);

            //Does the ball hit a wall?
            if (BallPosition.Y >= Game.Height - 1 || BallPosition.Y <= 0)
                Bounce(GameItems.Wall);

            //Does the ball hit a goal?
            if (CollisionObjects[BallPosition.X, BallPosition.Y] == GameItems.Goal)
            {
                Game.roundRunning = false;
                timer.Stop();
            }
        }
    }
}

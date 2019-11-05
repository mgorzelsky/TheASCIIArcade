using System;
using System.Drawing;
using System.Timers;

namespace FlappyBird
{
    public class Bird
    {
        private int gravity = 1;
        private int stepCounter = 1;
        private Point position;
        public Point Position { get { return position; } }

        public Bird()
        {
            position.X = 20;
            position.Y = 5;
            Game.gameStep.Elapsed += new ElapsedEventHandler(onGameStep);
        }

        public void Flap()
        {
            position.Y -= 3;
            if (position.Y < 0)
                position.Y = 0;
        }

        public void Drop()
        {
            position.Y += gravity;
        }

        private void onGameStep(object sender, ElapsedEventArgs e)
        {
            stepCounter++;
            if (stepCounter == 10)
            {
                Drop();
                stepCounter = 1;
            }
        }


    }
}

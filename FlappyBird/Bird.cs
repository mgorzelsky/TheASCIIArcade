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

        //on instantiation set the starting position and hook up the gameStep counter to a delegate.
        public Bird()
        {
            position.X = 20;
            position.Y = 5;
            Game.gameStep.Elapsed += new ElapsedEventHandler(onGameStep);
        }
        //On flap move up 3 spaces, not to exceed the roof to prevent OOB issues.
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

        //count the number of times the event fires off so that every 500ms gravity acts on the bird
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

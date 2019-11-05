using System;
using System.Drawing;
using System.Timers;

namespace FlappyBird
{
    public class Bird
    {
        private int gravity = 1;
        private Timer timer = new Timer(500);
        private Point position;
        public Point Position { get { return position; } }

        public Bird()
        {
            position.X = 20;
            position.Y = 5;
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }

        public void Flap()
        {
            position.Y -= 3;
        }

        public void Drop()
        {
            position.Y += gravity;
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Drop();
        }


    }
}

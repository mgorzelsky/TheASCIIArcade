using System;

using System.Drawing;
using System.Timers;

using System.Linq;

namespace FlappyBird
{
    public class Walls
    {

        private Timer timer = new Timer(50);
        private int currentX = 159; // starts at the right edge of the screen
        //private Point position = new Point(159, 0);
        //private Point priorPosition = new Point(159, 0);
        private int height = 40; // height is the screen size
        //private int width = 2;
        public int gap = 15;
        public int offset;
        Random rnd;
        private int speed = 1;
        //private Render render = new Render();
        private int[] wall;



        public Walls()
        {
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
            rnd = new Random();
            wall = new int[height];
            offset = height - gap;
            WallBuilder();

        }



        //public Point getPosition()
        //{
        //    return position;
        //}

        //public Point getPriorPosition()
        //{
        //    return priorPosition;
        //}

        //public int getHeight()
        //{
        //    return height;
        //}

        //public int getWidth()
        //{
        //    return width;
        //}

        // returns the current x position of the wall
        public int getCurrentX()
        {
            return currentX;
        }



        // moves the x position of the wall 
        public void Move()
        {
            if (currentX > 0)
                currentX -= speed;
        }

        // returns an array that contains the y position of the wall. 0 represents wall, 1 represents gap
        public int[] getWall()
        {
            return wall;
        }

        void WallBuilder()
        {
            int wallGap = rnd.Next(0, offset);
            for (int i = 0; i < gap; i++)
            {
                wall[wallGap + i] = 1;
            }
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Move();
        }
    }
}





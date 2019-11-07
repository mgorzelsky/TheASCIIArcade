using System;

using System.Drawing;
using System.Timers;

using System.Linq;

namespace FlappyBird
{
    public class Wall
    {
        private int currentX = FlappyBirdProgram.width - 1; // starts at the right edge of the screen
        public int gap = 12;
        public int offset;
        Random rnd;
        private int speed = 1;
        private int[] wall;

        public Wall()
        {
            Game.gameStep.Elapsed += new ElapsedEventHandler(onGameStep);
            rnd = new Random();
            wall = new int[FlappyBirdProgram.height];
            offset = FlappyBirdProgram.height - gap;
            WallBuilder();

        }

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

        //Builds a wall with a gap placed in a random location
        void WallBuilder()
        {
            int wallGap = rnd.Next(0, offset);
            for (int i = 0; i < gap; i++)
            {
                wall[wallGap + i] = 1;
            }
        }

        private void onGameStep(object sender, ElapsedEventArgs e)
        {
            Move();
        }
    }
}





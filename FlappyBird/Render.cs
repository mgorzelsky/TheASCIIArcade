using System;
using System.Text;

namespace FlappyBird
{
    public class Render
    {
        public void DrawScreen(CellState[,] gameState, int height, int width)
        {
            StringBuilder screenAsString = new StringBuilder("", height * width);
            char currentCharacter = Convert.ToChar(32);
            for (int y = 0; y < height; y++)
            {
                screenAsString.Clear();
                for (int x = 0; x < width; x++)
                {
                    if (y == 0)
                        currentCharacter = '-';
                    if (y == 39)
                        currentCharacter = '-';
                    if (y > 0 && y < height - 1)
                    {

                        switch (gameState[y, x])
                        {
                            case (CellState.Empty):
                                currentCharacter = Convert.ToChar(32);
                                break;
                            case (CellState.Bird):
                                currentCharacter = '>';
                                break;
                            case (CellState.Pillar):
                                currentCharacter = '|';
                                break;
                        }
                    }
                    screenAsString.Append(new char[] { currentCharacter });

                }
                Console.SetCursorPosition(0, y);
                Console.WriteLine(screenAsString);
            }
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine(screenAsString);
        }
        
        
        
        
        
        
        /*
        public Render()
        {
        }

        public void drawBird(Bird bird)
        {
            Console.SetCursorPosition(bird.getX(), bird.getY());
            Console.Write("*");
            Console.SetCursorPosition(bird.getX(), bird.priorY);
            Console.Write(" ");
            
        }

        public void drawWalls(Walls wall)
        {
            int x = wall.getPosition().X;
            //int y = wall.getPosition().Y;
            int priorX = wall.getPriorPosition().X;
            //int priorY = wall.getPriorPosition().Y;
            //int height = wall.getHeight();
            //int width = wall.getWidth();

            //for(int i = 0; i < height;i++)
            //{
            //    Console.SetCursorPosition(x, y++);
            //    Console.Write("<");
            //    Console.SetCursorPosition(priorX, priorY++);
            //    Console.Write(" ");

            //}

            //for (int i = 0; i < width; i++)
            //{
            //    Console.SetCursorPosition(x++, y);
            //    Console.Write("<");
            //    Console.SetCursorPosition(priorX++, priorY);
            //    Console.Write(" ");

            //}

            //for (int i = 0; i <= height; i++)
            //{
            //    Console.SetCursorPosition(x, y--);
            //    Console.Write("<");
            //    Console.SetCursorPosition(priorX, priorY--);
            //    Console.Write(" ");
            //}
            int[] pillar = wall.getWall();

            for(int i = 0; i < 40; i++)
            {
                Console.SetCursorPosition(x, i);
                if (pillar[i] == 0) Console.Write("<");
                else Console.Write(" ");
                Console.SetCursorPosition(priorX, i);
                Console.Write(" ");
            }
        }
        */
    }
}

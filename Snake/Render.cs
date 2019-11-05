using System;
using System.Text;

namespace Snake
{
    class Render
    {
        //  methods

        /// <summary>
        ///  DrawScreen: Takes an array of states, saves them in a string, then prints on console
        /// </summary>
        /// <param name="gameBoard"></param>
        public void DrawScreen()
        {
            StringBuilder screenBuffer = new StringBuilder(Game.gameBoard.Length);

            for (int y = 0; y < Game.gameBoard.GetLength(1); y++)        // rows
            {
                for (int x = 0; x < Game.gameBoard.GetLength(0); x++)    // columns
                {
                    switch (Game.gameBoard[x, y])
                    {
                        case StateOfLocation.Empty:
                            screenBuffer.Append(" ");
                            break;
                        case StateOfLocation.Snake:
                            screenBuffer.Append("#");
                            break;
                        case StateOfLocation.Food:
                            screenBuffer.Append("*");
                            break;
                        default:
                            break;
                    }

                }
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(screenBuffer);
        }
    }
}


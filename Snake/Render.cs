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
        public void DrawScreen(StateOfLocation[,] gameBoard)
        {
            StringBuilder screenBuffer = new StringBuilder(gameBoard.Length);

            for (int y = 0; y < gameBoard.GetLength(1); y++)        // rows
            {
                for (int x = 0; x < gameBoard.GetLength(0); x++)    // columns
                {
                    switch (gameBoard[x, y])
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


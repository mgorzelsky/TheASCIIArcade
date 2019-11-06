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
                    if (x == 0 && y == 0)
                        screenBuffer.Append("+");
                    else if (x == 0 && y == SnakeProgram.height - 1)
                        screenBuffer.Append("+");
                    else if (x == SnakeProgram.width - 1 && y == 0)
                        screenBuffer.Append("+");
                    else if (x == SnakeProgram.width - 1 && y == SnakeProgram.height - 1)
                        screenBuffer.Append("+");
                    else if (x == 0)
                        screenBuffer.Append("|");
                    else if (x == SnakeProgram.width - 1)
                        screenBuffer.Append("|");
                    else if (y == 0)
                        screenBuffer.Append("-");
                    else if (y == SnakeProgram.height - 1)
                        screenBuffer.Append("-");
                    else
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
                screenBuffer.Append("\n");
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(screenBuffer);
        }
        public void DrawGenericScreen(string thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset);
            foreach (char character in thingToDraw)
            {
                if (character.Equals('\0'))
                {
                    heightOffset++;
                    Console.SetCursorPosition(widthOffset, heightOffset);
                }
                if (!character.Equals('\0'))
                    Console.Write(character);
            }
        }

        public void DrawGenericScreen(string[] thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset);
            foreach (string line in thingToDraw)
            {
                Console.SetCursorPosition(widthOffset, heightOffset);
                Console.Write(line);
                heightOffset++;
            }
        }
    }
}


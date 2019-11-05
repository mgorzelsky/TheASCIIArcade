using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public class Renderer
    {
        public void DrawGame(GameItems [,] screenArray)
        {
            StringBuilder screenAsString = new StringBuilder("", Game.Width * Game.Height);
            char currentCharacter = Convert.ToChar(32);
            for (int y = 0; y < Game.Height; y++)
            {
                for (int x = 0; x < Game.Width; x++)
                {
                    switch (screenArray[x, y])
                    {
                        case (GameItems.Nothing):
                            currentCharacter = Convert.ToChar(32);
                            break;
                        case (GameItems.Paddle):
                            currentCharacter = '|';
                            break;
                        case (GameItems.Wall):
                            currentCharacter = '-';
                            break;
                        case (GameItems.Ball):
                            currentCharacter = 'o';
                            break;
                        case (GameItems.Goal):
                            currentCharacter = Convert.ToChar(32);
                            break;
                    }
                    screenAsString.Append(new char[] { currentCharacter });
                }
                screenAsString.Append(Environment.NewLine);
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(screenAsString);
        }

        public void DrawGenericScreen(string thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset + 1);
            foreach (char character in thingToDraw)
            {
                if (character.Equals('\a'))
                {
                    heightOffset++;
                    Console.SetCursorPosition(widthOffset, heightOffset + 1);
                }
                    Console.Write(character);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    public class Renderer
    {
        //Turn the GameItems[,] into a string that then draws to the screen.
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
                screenAsString.Append(Environment.NewLine); //append line breaks to the string so that it auto-returns at the appropriate point
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(screenAsString);
        }

        //same as in TheASCIIArcade.Draw
        public void DrawGenericScreen(string thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset + 1);
            foreach (char character in thingToDraw)
            {
                if (character.Equals('\0'))
                {
                    heightOffset++;
                    Console.SetCursorPosition(widthOffset, heightOffset + 1);
                }
                if (!character.Equals('\0'))
                    Console.Write(character);
            }
        }
    }
}
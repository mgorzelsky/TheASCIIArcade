using System;
using System.Text;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace TheASCIIArcade
{
    class Draw
    {
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

        public void DrawLines(string thingToDraw, int widthOffset, int heightOffset, int numberOfChars, bool isVerticle)
        {
            if (isVerticle)
                for (int y = heightOffset; y < numberOfChars; y++)
                {
                        Console.SetCursorPosition(widthOffset, y);
                        Console.Write(thingToDraw);
                }
            else
                for (int x = widthOffset; x < numberOfChars; x++)
                {
                    Console.SetCursorPosition(x, heightOffset);
                    Console.Write(thingToDraw);
                }
        }

        public void DrawSelectionBox()
        {
            int flashIncrementer = 0;
            while (!Program.selectionIsMade)
            {
                if (Program.currentSelection == new Point(0, 0))
                {
                    FlashySquare(0, 0, flashIncrementer);
                }
                if (Program.currentSelection == new Point(0, 1))
                {
                    FlashySquare(0, Program.height / 2, flashIncrementer);
                }
                if (Program.currentSelection == new Point(1, 0))
                {
                    FlashySquare(Program.width / 2, 0, flashIncrementer);
                }
                if (Program.currentSelection == new Point(1, 1))
                {
                    FlashySquare(Program.width / 2, Program.height / 2, flashIncrementer);
                }
                flashIncrementer++;
                if (flashIncrementer > 1)
                    flashIncrementer = 0;
                Thread.Sleep(200);
            }
        }

        private void FlashySquare(int xOffset, int yOffset, int flashIncrementer)
        {
            for (int y = yOffset; y < (Program.height / 2 - 1) + yOffset; y++)
            {
                for (int x = xOffset; x < (Program.width / 2 - 2) + xOffset; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (flashIncrementer == 0)
                    {
                        if (y == 0 + yOffset && x == 0 + xOffset)
                            Console.Write("+");
                        else if (y == 0 && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == 0)
                            Console.Write("+");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+");
                        else if (y == 0 || y == (Program.height / 2 - 2) + yOffset)
                            Console.Write("-");
                        else if (x == 0 || x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("|");
                    }
                    else
                    {
                        if (y == 0 + yOffset && x == 0 + xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == 0 && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == (Program.height / 2 - 2) + yOffset && x == 0)
                            Console.Write("+", Color.Red);
                        else if (y == (Program.height / 2 - 2) + yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == 0 || y == (Program.height / 2 - 2) + yOffset)
                            Console.Write("-", Color.Red);
                        else if (x == 0 || x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("|", Color.Red);
                    }
                }
            }
        }
    }
}

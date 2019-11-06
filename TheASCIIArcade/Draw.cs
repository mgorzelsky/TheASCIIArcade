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
                    for (int y = 0; y < Program.height/2 - 1; y++)
                    {
                        for (int x = 0; x < Program.width/2 - 2; x++)
                        {
                            Console.SetCursorPosition(x, y);
                            if (flashIncrementer == 0)
                            {
                                if (y == 0 && x == 0)
                                    Console.Write("+");
                                else if (y == 0 && x == Program.width / 2 - 3)
                                    Console.Write("+");
                                else if (y == Program.height / 2 - 2 && x == 0)
                                    Console.Write("+");
                                else if (y == Program.height / 2 - 2 && x == Program.width / 2 - 3)
                                    Console.Write("+");
                                else if (y == 0 || y == Program.height / 2 - 2)
                                    Console.Write("-");
                                else if (x == 0 || x == Program.width / 2 - 3)
                                    Console.Write("|");
                            }
                            else
                            {
                                if (y == 0 && x == 0)
                                    Console.Write("+", Color.Red);
                                else if (y == 0 && x == Program.width / 2 - 3)
                                    Console.Write("+", Color.Red);
                                else if (y == Program.height / 2 - 2 && x == 0)
                                    Console.Write("+", Color.Red);
                                else if (y == Program.height / 2 - 2 && x == Program.width / 2 - 3)
                                    Console.Write("+", Color.Red);
                                else if (y == 0 || y == Program.height / 2 - 2)
                                    Console.Write("-", Color.Red);
                                else if (x == 0 || x == Program.width / 2 - 3)
                                    Console.Write("|", Color.Red);
                            }
                        }
                    }
                }
                if (Program.currentSelection == new Point(0, 1))
                {

                }
                if (Program.currentSelection == new Point(1, 0))
                {

                }
                if (Program.currentSelection == new Point(1, 1))
                {

                }
                flashIncrementer++;
                if (flashIncrementer > 1)
                    flashIncrementer = 0;
                Thread.Sleep(200);
            }
        }
    }
}

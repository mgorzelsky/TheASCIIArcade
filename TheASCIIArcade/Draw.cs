using System;
using System.Text;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace TheASCIIArcade
{
    class Draw
    {
        //2 overlaods. Draws the input string or string[] with positioning based on the supplied width and height offset.
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

        //Takes in a character with a height and width offset and draws it the size of the screen in a straight line. Bool
        //to tell if it draws verticle or horizontal.
        public void DrawLines(char charToDraw, int widthOffset, int heightOffset, int numberOfChars, bool isVerticle)
        {
            if (isVerticle)
                for (int y = heightOffset; y < numberOfChars; y++)
                {
                    Console.SetCursorPosition(widthOffset, y);
                    Console.Write(charToDraw);
                }
            else
                for (int x = widthOffset; x < numberOfChars; x++)
                {
                    Console.SetCursorPosition(x, heightOffset);
                    Console.Write(charToDraw);
                }
        }

        //Runs on a seperate thread. re-drawing the selection box based on a sleep timer. Flashes between white and red.
        public void DrawSelectionBox()
        {
            int flashIncrementer = 0;
            while (!Program.selectionIsMade)
            {
                SquareEraser();
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
                    FlashySquare(Program.width / 2 + 1, 0, flashIncrementer);
                }
                if (Program.currentSelection == new Point(1, 1))
                {
                    FlashySquare(Program.width / 2 + 1, Program.height / 2, flashIncrementer);
                }
                flashIncrementer++;
                if (flashIncrementer > 1)
                    flashIncrementer = 0;
                Thread.Sleep(200);
            }
        }

        //abstracted out logic for drawing the square
        private void FlashySquare(int xOffset, int yOffset, int flashIncrementer)
        {
            for (int y = yOffset; y < (Program.height / 2 - 1) + yOffset; y++)
            {
                for (int x = xOffset; x < (Program.width / 2 - 2) + xOffset; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (flashIncrementer == 0)
                    {
                        if (y == yOffset && x == xOffset)
                            Console.Write("+");
                        else if (y == yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == xOffset)
                            Console.Write("+");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+");
                        else if (y == yOffset || y == (Program.height / 2 - 2) + yOffset)
                            Console.Write("-");
                        else if (x == xOffset || x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("|");
                    }
                    else
                    {
                        if (y == yOffset && x == xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == (Program.height / 2 - 2) + yOffset && x == xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == (Program.height / 2 - 2) + yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("+", Color.Red);
                        else if (y == yOffset || y == (Program.height / 2 - 2) + yOffset)
                            Console.Write("-", Color.Red);
                        else if (x == xOffset || x == (Program.width / 2 - 3) + xOffset)
                            Console.Write("|", Color.Red);
                    }
                }
            }
        }
        //Call SquareEraser Every time through the DrawSquare method. It will erase any square that exists outside of
        //the current selection without erasing whats inside of that square.
        private void SquareEraser()
        {
            if (Program.currentSelection == new Point(0, 0))
            {
                Erase(0, Program.height / 2);
                Erase(Program.width / 2 + 1, 0);
                Erase(Program.width / 2 + 1, Program.height / 2);
            }
            if (Program.currentSelection == new Point(0, 1))
            {
                Erase(0, 0);
                Erase(Program.width / 2 + 1, 0);
                Erase(Program.width / 2 + 1, Program.height / 2);
            }
            if (Program.currentSelection == new Point(1, 0))
            {
                Erase(0, 0);
                Erase(0, Program.height / 2);
                Erase(Program.width / 2 + 1, Program.height / 2);
            }
            if (Program.currentSelection == new Point(1, 1))
            {
                Erase(0, 0);
                Erase(0, Program.height / 2);
                Erase(Program.width / 2 + 1, 0);
            }

            void Erase(int xOffset, int yOffset)
            {
                for (int y = yOffset; y < (Program.height / 2 - 1) + yOffset; y++)
                {
                    for (int x = xOffset; x < (Program.width / 2 - 2) + xOffset; x++)
                    {
                        Console.SetCursorPosition(x, y);
                        if (y == yOffset && x == xOffset)
                            Console.Write(" ");
                        else if (y == yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write(" ");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == xOffset)
                            Console.Write(" ");
                        else if (y == (Program.height / 2 - 2) + yOffset && x == (Program.width / 2 - 3) + xOffset)
                            Console.Write(" ");
                        else if (y == yOffset || y == (Program.height / 2 - 2) + yOffset)
                            Console.Write(" ");
                        else if (x == xOffset || x == (Program.width / 2 - 3) + xOffset)
                            Console.Write(" ");
                    }
                }
            }
        }
    }
}

using System;
using FlappyBird;
using Snake;
using Pong;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace TheASCIIArcade
{
    class Program
    {
        public static bool selectionIsMade = false;
        public static Point currentSelection;
        public static int width = 80;
        public static int height = 20;
        static Draw draw;
        static void Main()
        {
            draw = new Draw();
            Console.CursorVisible = false;
            Thread selectionBox = new Thread(draw.DrawSelectionBox);

            FlappyBirdProgram flappyBird = new FlappyBirdProgram();
            SnakeProgram snake = new SnakeProgram();
            PongProgram pong = new PongProgram();


            string[] arcadeLogo = File.ReadAllLines(@"txt\logo.txt");
            string[] pongLogo = File.ReadAllLines(@"txt\PongLogo.txt"); //character width 11
            string[] dungeonCrawlerLogo = File.ReadAllLines(@"txt\DungeonCrawlerLogo.txt"); //character width 13
            string[] flappyBirdLogo = File.ReadAllLines(@"txt\FlappyBirdLogo.txt"); //character width 13
            string[] snakeLogo = File.ReadAllLines(@"txt\SnakeLogo.txt"); //character width 13
            string contributers = "Contributers: Michael Gorzelsky";
            string instructions = @"Use the arrow ^ keys to move your selection, Enter| to confirm it." + "\0" +
                                   "            < V >                                <-";
            string quitInstructions = "Press Q to quit";

            draw.DrawGenericScreen(arcadeLogo, (width - 73) / 2, 1);
            draw.DrawGenericScreen(instructions, (width - 67)/2, height - (height/3));
            draw.DrawGenericScreen(contributers, (width - contributers.Length)/2, height - 1);
            Thread.Sleep(5000);
            Console.Clear();


            currentSelection = new Point(0, 0);
            bool stillPlaying = true;
            bool threadRunning = false;
            while (stillPlaying)
            {
                Console.CursorVisible = false;
                draw.DrawGenericScreen(quitInstructions, (width - quitInstructions.Length) / 2, height + 1);

                draw.DrawLines("-", 0, (height / 2) - 1, width, false);
                draw.DrawLines("|", (width / 2) - 1, 0, height, true);

                //Draw the 4 squares representing your game choices
                draw.DrawGenericScreen(pongLogo, ((width / 2) - 11) / 2, (height / 2 - 5) / 2);
                draw.DrawGenericScreen(dungeonCrawlerLogo, ((width / 2) - 13) / 2 + (width / 2), (height / 2 - 7) / 2);
                draw.DrawGenericScreen(flappyBirdLogo, ((width / 2) - 13) / 2, ((height / 2 - 6) / 2) + (height / 2));
                draw.DrawGenericScreen(snakeLogo, ((width / 2) - 13) / 2 + (width / 2), ((height / 2 - 6) / 2) + (height / 2));

                if (!threadRunning)
                {
                    selectionBox = new Thread(draw.DrawSelectionBox);
                    selectionBox.Start();
                    threadRunning = true;
                }

                while (!selectionIsMade)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case (ConsoleKey.UpArrow):
                            if (currentSelection.Y - 1 < 0)
                                currentSelection.Y = 0;
                            else
                                currentSelection.Y--;
                            break;
                        case (ConsoleKey.LeftArrow):
                            if (currentSelection.X - 1 < 0)
                                currentSelection.X = 0;
                            else
                                currentSelection.X--;
                            break;
                        case (ConsoleKey.DownArrow):
                            if (currentSelection.Y + 1 > 1)
                                currentSelection.Y = 1;
                            else
                                currentSelection.Y++;
                            break;
                        case (ConsoleKey.RightArrow):
                            if (currentSelection.X + 1 > 1)
                                currentSelection.X = 1;
                            else
                                currentSelection.X++;
                            break;
                        case (ConsoleKey.Enter):
                            selectionIsMade = true;
                            break;
                        case (ConsoleKey.Q):
                            selectionIsMade = true;
                            return;
                    }
                }
                if (currentSelection == new Point(0, 0))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    pong.StartPong();
                    selectionIsMade = false;
                    Console.Clear();
                }
                if (currentSelection == new Point(1, 0))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    Debug.WriteLine("not yet available");
                    selectionIsMade = false;
                    Console.Clear();
                }
                if (currentSelection == new Point(0, 1))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    flappyBird.StartFlappyBird();
                    selectionIsMade = false;
                    Console.Clear();
                }
                if (currentSelection == new Point(1, 1))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    snake.StartSnake();
                    selectionIsMade = false;
                    Console.Clear();
                }
            }
        }
    }
}

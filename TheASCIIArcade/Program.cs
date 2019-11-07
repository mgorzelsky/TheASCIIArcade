using System;
using FlappyBird;
using Snake;
using Pong;
using EscapeFromDarkForest;
using System.Threading;
using System.IO;
using System.Drawing;

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

            //Grab all the images from their text files and put them into strings or string[]s for use later.
            string[] arcadeLogo = File.ReadAllLines(@"txt\logo.txt");
            string[] pongLogo = File.ReadAllLines(@"txt\PongLogo.txt"); //character width 11
            string[] dungeonCrawlerLogo = File.ReadAllLines(@"txt\DungeonCrawlerLogo.txt"); //character width 13
            string[] flappyBirdLogo = File.ReadAllLines(@"txt\FlappyBirdLogo.txt"); //character width 13
            string[] snakeLogo = File.ReadAllLines(@"txt\SnakeLogo.txt"); //character width 13
            string contributers = "Contributers: Michael Gorzelsky";
            string instructions = @"Use the arrow ^ keys to move your selection, Enter| to confirm it." + "\0" +
                                   "            < V >                                <-";
            string quitInstructions = "Press Q to quit";

            //Draw the initial splash screen, wait 5 seconds, then move on
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

                //Lines seperating the 4 game icons
                draw.DrawLines('-', 0, (height / 2) - 1, width, false);
                draw.DrawLines('|', (width / 2) - 1, 0, height, true);

                //position each of the game logos in the center of their quadrant, regardless of size of program size (width/height) 
                draw.DrawGenericScreen(pongLogo, ((width / 2) - 11) / 2, (height / 2 - 5) / 2);
                draw.DrawGenericScreen(dungeonCrawlerLogo, ((width / 2) - 13) / 2 + (width / 2), (height / 2 - 7) / 2);
                draw.DrawGenericScreen(flappyBirdLogo, ((width / 2) - 13) / 2, ((height / 2 - 6) / 2) + (height / 2));
                draw.DrawGenericScreen(snakeLogo, ((width / 2) - 13) / 2 + (width / 2), ((height / 2 - 6) / 2) + (height / 2));

                //The drawing thread should only be created and started if there isn't already one running. Sets threadRunning
                //to true when thread is started so that the next time through the loop this is skipped.
                if (!threadRunning)
                {
                    selectionBox = new Thread(draw.DrawSelectionBox);
                    selectionBox.Start();
                    threadRunning = true;
                }

                //Allows for moving the selection box between games. If statements keep it within the bounds of the
                //4 choices.
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
                            return; //returns out of main method, ending program.
                    }
                }
                //Once enter or Q is pressed move from loop into here where it launches the program based on the current
                //point position.
                if (currentSelection == new Point(0, 0))
                {
                    selectionBox.Join(); //Make sure this thread finishes running before launching the game so that there are no rendering conflicts.
                    threadRunning = false; //Now that the thread has Join()ed set this to false since it is not running.
                    PongProgram pong = new PongProgram();
                    pong.StartPong();
                    selectionIsMade = false; //Set this back to false so that we can enter the input loop again.
                    Console.Clear();
                }
                if (currentSelection == new Point(1, 0))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    EscapeFromDarkForestProgram forest = new EscapeFromDarkForestProgram();
                    forest.Start();
                    selectionIsMade = false;
                    Console.Clear();
                }
                if (currentSelection == new Point(0, 1))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    FlappyBirdProgram flappyBird = new FlappyBirdProgram();
                    flappyBird.StartFlappyBird();
                    selectionIsMade = false;
                    Console.Clear();
                }
                if (currentSelection == new Point(1, 1))
                {
                    selectionBox.Join();
                    threadRunning = false;
                    SnakeProgram snake = new SnakeProgram();
                    snake.StartSnake();
                    selectionIsMade = false;
                    Console.Clear();
                }
            }
        }
    }
}

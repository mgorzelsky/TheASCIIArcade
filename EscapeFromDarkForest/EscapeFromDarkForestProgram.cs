using System;
using System.IO;
using System.Threading;

namespace EscapeFromDarkForest
{
    class EscapeFromDarkForest
    {
        public static Game game;
        public static int width = 100;
        public static int height = 33;
        public static bool playAgain;
        public static bool viewInstructions;
        public void Start()
        {
            Console.Clear();
            Console.SetCursorPosition((width - 92) / 2, 0);
            Console.Write("Adjust your window size vertically to see the lower message at the same time as this message");
            Console.SetCursorPosition((width - 24) / 2, height / 2);
            Console.Write("Press any key when ready");
            Console.SetCursorPosition((width - 92) / 2, height);
            Console.Write("Adjust your window size veritcally to see the upper message at the same time as this message");

            Console.ReadKey(true);
            Console.Clear();
            Console.CursorVisible = false;

            string[] escapeFromDarkForestSplash = File.ReadAllLines(@"txt\EscapeFromDarkForestSplash.txt");
            string contributers = "Contributers: Michael Gorzelsky";

            DrawGenericScreen(escapeFromDarkForestSplash, (width - escapeFromDarkForestSplash[0].Length) / 2, 0);
            DrawGenericScreen(contributers, (width - contributers.Length) / 2, height);

            Thread.Sleep(5000);
            Console.Clear();

            string[] storyText = File.ReadAllLines(@"txt\StoryText.txt");
            int iterationCount = 0;
            foreach (string line in storyText)
            {
                WriteTextProcedurally(line, iterationCount);
                iterationCount++;
                Thread.Sleep(500);
            }
            Thread.Sleep(2000);

            string[] playerSprite = File.ReadAllLines(@"txt\Player.txt");
            string[] enemySprite = File.ReadAllLines(@"txt\Enemy.txt");
            string[] foodSprite = File.ReadAllLines(@"txt\Food.txt");
            string[] wallSprite = File.ReadAllLines(@"txt\Wall.txt");
            string[] exitSprite = File.ReadAllLines(@"txt\Exit.txt");

            viewInstructions = true;
            while (viewInstructions)
            {
                Console.Clear();

                DrawGenericScreen(playerSprite, 2, 1);
                DrawGenericScreen(enemySprite, 2, (height / 5));
                DrawGenericScreen(foodSprite, 2, (height / 5) * 2);
                DrawGenericScreen(wallSprite, 2, (height / 5) * 3);
                DrawGenericScreen(exitSprite, 2, (height / 5) * 4);

                DrawGenericScreen("This is you. Use the arrow keys to navigate to the Exit.", 9, 1 + 1);
                DrawGenericScreen("These are the mysterious, dangerous, creatures of the wood.\0 Watch out, if they get too close you might have trouble.", 9, (height / 5) + 1);
                DrawGenericScreen("Food! Make sure you swing by these to give yourself a\0 little recharge and hopefully make it out of here.", 9, (height / 5) * 2 + 1);
                DrawGenericScreen("These thick brambles block your way. You can destroy them,\0 but the effort will make you hungier. (move into them)", 9, (height / 5) * 3 + 1);
                DrawGenericScreen("A place where you can work your way up out of the depths\0 of this forest. Who knows how far down you are.", 9, (height / 5) * 4 + 1);
                DrawGenericScreen("Press any key to continue", (width - 25) / 2, height - 2);

                Console.ReadKey(true);

                playAgain = true;
                while (playAgain)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    game = new Game();
                    game.Start();
                }
            }
        }

        public static void WriteTextProcedurally(string line, int iterationCount)
        {
            Console.SetCursorPosition((width - line.Length) / 2, ((height / 2) - 2) + iterationCount);
            foreach (char character in line)
            {
                Console.Write(character);
                Thread.Sleep(75);
            }
        }

        public static void DrawGenericScreen(string thingToDraw, int widthOffset, int heightOffset)
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

        private static void DrawGenericScreen(string[] thingToDraw, int widthOffset, int heightOffset)
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

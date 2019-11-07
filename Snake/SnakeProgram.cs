using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Snake
{
    public class SnakeProgram
    {
        public static int width = 50;
        public static int height = 20;
        private List<int> scores = new List<int>();

        //Provides a starting point for the game itself. Will be used later for menu options such as difficulty,
        //high scores, and restarting after loss.
        public void StartSnake()
        {
            int consoleWidth = 120;
            int consoleHeight = 25;
            Console.Clear();
            Console.CursorVisible = false;

            string[] snakeSplash = File.ReadAllLines(@"txt\SnakeSplash.txt");
            string contributers = "Contributers: Michael Barta, Michael Gorzelsky, Radiah Jones";
            string instructions = "Use the arrow keys to turn. Avoid walls and yourself. Eat the food --> *";

            Render renderer = new Render();
            renderer.DrawGenericScreen(snakeSplash, (consoleWidth - snakeSplash[7].Length) / 2, 1);
            renderer.DrawGenericScreen(contributers, (consoleWidth - contributers.Length) / 2, consoleHeight - 1);
            renderer.DrawGenericScreen(instructions, (consoleWidth - instructions.Length) / 2, consoleHeight / 2 + 2);

            Thread.Sleep(5000);

            //bool playAgain = true;
            //while (playAgain)
            //{
                Console.Clear();
                Game game = new Game();
                int score = game.PlayGame();

                AddScoreToHighscores(score);

                //string playAgainMessage = "Play Again? Y/N";
                //Console.SetCursorPosition((width - playAgainMessage.Length) / 2, height / 2);

            //    ConsoleKey playAgainChoice = Console.ReadKey(true).Key;
            //    if (playAgainChoice != ConsoleKey.Y)
            //        playAgain = false;
            //}
        }

        public List<int> GetScores()
        {
            return scores;
        }
        public void AddScoreToHighscores(int recentScore)
        {
            scores.Add(recentScore);
            scores.Sort();
            scores.Reverse();
        }
    }
}
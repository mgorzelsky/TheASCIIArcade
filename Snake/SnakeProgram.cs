using System;
using System.Collections.Generic;

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
            bool playAgain = true;
            while (playAgain)
            {
                Game game = new Game();
                int score = game.PlayGame();

                AddScoreToHighscores(score);

                Console.SetCursorPosition(10, 5);
                Console.WriteLine("Game Over!");
                Console.SetCursorPosition(10, 6);
                Console.WriteLine("Score: {0}", score);
                foreach (int s in scores)
                    Console.Write($"{s} ");

                ConsoleKey playAgainChoice = Console.ReadKey(true).Key;
                if (playAgainChoice != ConsoleKey.Y)
                    playAgain = false;
            }
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
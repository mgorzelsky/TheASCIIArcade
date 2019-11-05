using System;
using FlappyBird;
using Snake;
using Pong;

namespace TheASCIIArcade
{
    class Program
    {
        static void Main()
        {
            FlappyBirdProgram flappyBird = new FlappyBirdProgram();
            //flappyBird.StartFlappyBird();
            SnakeProgram snake = new SnakeProgram();
            //snake.StartSnake();
            PongProgram pong = new PongProgram();
            //pong.StartPong();
        }
    }
}

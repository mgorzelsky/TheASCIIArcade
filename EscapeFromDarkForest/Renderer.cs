using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;

namespace EscapeFromDarkForest
{
    class Renderer
    {
        private ulong renderCount = 0;
        private int playerAnimationStep = 1;
        private int enemyAnimationStep = 1;
        private int foodAnimationStep = 1;
        private bool enemyAttackBool = false;
        private int enemyAttackStep = 1;
        private Point attackingEnemy;
        private Player player;
        public Renderer(Player player)
        {
            this.player = player;
            Enemy.EnemyAttack += EnemyAttacked;
        }
        
        //Started on its own thread within the Game class. Continuously runs updating the screen
        //to display the current state of the screen. Found that 240/sec is smooth and responsive.
        public void DrawScreen()
        {
            while (!Game.gameOver)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        switch (Game.gameBoard[x, y])
                        {
                            case (GameObjects.empty):
                                DrawEmpty(x, y);
                                break;
                            case (GameObjects.wall):
                                DrawWall(x, y);
                                break;
                            case (GameObjects.exit):
                                DrawExit(x, y);
                                break;
                            case (GameObjects.food):
                                DrawFood(x, y);
                                break;
                            case (GameObjects.player):
                                DrawPlayer(x, y);
                                break;
                            case (GameObjects.enemy):
                                DrawEnemy(x, y);
                                break;
                        }
                    }
                    Console.SetCursorPosition(2, 0);
                    Console.Write($"Current Level:       {Game.level}     ");
                    Console.SetCursorPosition(2, 1);
                    Console.Write($"Current Food left:   {player.Food}      ");
                    Thread.Sleep(1000 / 240);

                    //These allow incrementing and resetting of a variable that controls what 
                    //animation frame of each sprite is rendered based on the number of times
                    //the screen has refreshed to get variable timings.
                    renderCount++;
                    if (renderCount % 120 == 0)
                    {
                        playerAnimationStep++;
                        if (playerAnimationStep > 2)
                            playerAnimationStep = 1;
                    }
                    if (renderCount % 100 == 0)
                    {
                        foodAnimationStep++;
                        if (foodAnimationStep > 2)
                            foodAnimationStep = 1;
                    }
                    if (renderCount % 30 == 0)
                    {
                        enemyAnimationStep++;
                        if (enemyAnimationStep > 8)
                            enemyAnimationStep = 1;
                    }
                    if (enemyAttackBool)
                    {
                        if (renderCount % 20 == 0)
                        {
                            enemyAttackStep++;
                            if (enemyAttackStep > 3)
                            {
                                enemyAttackStep = 1;
                                enemyAttackBool = false;
                            }
                        }
                    }

                }
            }
        }


        //Empty cell background. Mild green ticks to represent grass
        private void DrawEmpty(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string emptyTexture1 = " ` ` ";
            string emptyTexture2 = "` ` `";
            string emptytexture3 = " ` ` ";

            Console.SetCursorPosition(x, y);
            Console.Write(emptyTexture1, Color.DarkOliveGreen);
            Console.SetCursorPosition(x, y + 1);
            Console.Write(emptyTexture2, Color.DarkOliveGreen);
            Console.SetCursorPosition(x, y + 2);
            Console.Write(emptytexture3, Color.DarkOliveGreen);
        }

        //Player has two animation frames controlled by the playerAnimationStep int
        private void DrawPlayer(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string playerTexture1;
            string playerTexture2;
            string playerTexture3;
            if (playerAnimationStep % 2 == 0)
            {
                playerTexture1 = " {0}O/ ";
                playerTexture2 = "{0}/| {0}";
                playerTexture3 = " / \\ ";
            }
            else
            {
                playerTexture1 = " \\O{0} ";
                playerTexture2 = "{0} |\\{0}";
                playerTexture3 = " / \\ ";
            }
            string[] bg = new string[] { "`" };

            Console.SetCursorPosition(x, y);
            Console.WriteFormatted(playerTexture1, Color.DarkOliveGreen, Color.White, bg);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteFormatted(playerTexture2, Color.DarkOliveGreen, Color.White, bg);
            Console.SetCursorPosition(x, y + 2);
            Console.WriteFormatted(playerTexture3, Color.DarkOliveGreen, Color.White, bg);
        }

        //Enemy has two different animations. A basic idle animation of 8 frames 
        //as well as an attack animation of 3 frames. The enemyAttackBool is controlled by
        //the EnemyAttack event.
        private void DrawEnemy(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string enemyTexture1 = @"  o  ";
            string enemyTexture2 = @" <|> ";
            string enemyTexture3 = @"  V  ";
            if (enemyAttackBool && (attackingEnemy.X + 1) * 5 == x && (attackingEnemy.Y + 1) * 3 == y)
            {
                switch (enemyAttackStep)
                {
                    case (1):
                        enemyTexture1 = @"  -  ";
                        enemyTexture2 = @" <*> ";
                        enemyTexture3 = @"  -  ";
                        break;
                    case (2):
                        enemyTexture1 = @"*   *";
                        enemyTexture2 = @"< O >";
                        enemyTexture3 = @"*   *";
                        break;
                    case (3):
                        enemyTexture1 = @" . . ";
                        enemyTexture2 = @"  o  ";
                        enemyTexture3 = @" * * ";
                        break;
                }
            }
            else
            {
                switch (enemyAnimationStep)
                {
                    case (1):
                        enemyTexture1 = @"  o  ";
                        enemyTexture2 = @" <|> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (2):
                        enemyTexture1 = @"  O  ";
                        enemyTexture2 = @" <\> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (3):
                        enemyTexture1 = @"  o  ";
                        enemyTexture2 = @" <-> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (4):
                        enemyTexture1 = @"  O  ";
                        enemyTexture2 = @" </> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (5):
                        enemyTexture1 = @"  o  ";
                        enemyTexture2 = @" <|> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (6):
                        enemyTexture1 = @"  O  ";
                        enemyTexture2 = @" <\> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (7):
                        enemyTexture1 = @"  o  ";
                        enemyTexture2 = @" <-> ";
                        enemyTexture3 = @"  V  ";
                        break;
                    case (8):
                        enemyTexture1 = @"  O  ";
                        enemyTexture2 = @" </> ";
                        enemyTexture3 = @"  V  ";
                        break;

                }
            }
            
            Console.SetCursorPosition(x, y);
            Console.Write(enemyTexture1);
            Console.SetCursorPosition(x, y + 1);
            Console.Write(enemyTexture2);
            Console.SetCursorPosition(x, y + 2);
            Console.Write(enemyTexture3);
        }

        //Simple brown walls.
        private void DrawWall(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string wallTexture1 = @"+~~~+";
            string wallTexture2 = @"~~+~~";
            string wallTexture3 = @"+~~~+";

            Console.SetCursorPosition(x, y);
            Console.Write(wallTexture1, Color.FromArgb(91, 41, 18));
            Console.SetCursorPosition(x, y + 1);
            Console.Write(wallTexture2, Color.FromArgb(91, 41, 18));
            Console.SetCursorPosition(x, y + 2);
            Console.Write(wallTexture3, Color.FromArgb(91, 41, 18));
        }

        //Food is a two frame animation controlled by the foodAnimationStep variable.
        private void DrawFood(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string foodTexture1;
            string foodTexture2;
            string foodTexture3;
            if (foodAnimationStep == 1)
            {
                foodTexture1 = " {0}({0} ";
                foodTexture2 = "{0} _)_";
                foodTexture3 = " c\\_/";
            }
            else
            {
                foodTexture1 = " {0} {0})";
                foodTexture2 = "{0} _(_";
                foodTexture3 = " c\\_/";
            }
            string[] bg = new string[] { "`" };

            Console.SetCursorPosition(x, y);
            Console.WriteFormatted(foodTexture1, Color.DarkOliveGreen, Color.White, bg);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteFormatted(foodTexture2, Color.DarkOliveGreen, Color.White, bg);
            Console.SetCursorPosition(x, y + 2);
            Console.WriteFormatted(foodTexture3, Color.DarkOliveGreen, Color.White, bg);
        }
        private void DrawExit(int x, int y)
        {
            x = (x + 1) * 5;
            y = (y + 1) * 3;
            string exitTexture1 = "U P _";
            string exitTexture2 = "  _| ";
            string exitTexture3 = "_|   ";

            Console.SetCursorPosition(x, y);
            Console.Write(exitTexture1);
            Console.SetCursorPosition(x, y + 1);
            Console.Write(exitTexture2);
            Console.SetCursorPosition(x, y + 2);
            Console.Write(exitTexture3);
        }

        //Method called by the EnemyAttack delegate for when the attack event is called. Controls the
        //enemyAttackBool as well as passes along the position of the attacking enemy so
        //only the attacking one actually animates instead of all of them.
        void EnemyAttacked(object sender, EnemyAttackEventArgs e)
        {
            enemyAttackBool = true;
            this.attackingEnemy = e.Position;
        }
    }
}

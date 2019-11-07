using System;
using System.Drawing;
using System.Threading;

namespace EscapeFromDarkForest
{
    class Enemy
    {
        private Point position;
        public Point Position { get { return position; } }
        private bool recentlyAttacked = false;
        Player player;

        public static event EventHandler<EnemyAttackEventArgs> EnemyAttack;

        //The enemy has to interact with the player so when constructed it takes in reference to the player
        //so it can interact directly with it.
        public Enemy(Player player)
        {
            StartingPosition();
            this.player = player;
        }
        private void StartingPosition()
        {
            while (true)
            {
                position.X = Game.rnd.Next(2, 6);   //Enemy can't spawn too close to the edges, giving the player more of a chance to get to the exit
                position.Y = Game.rnd.Next(2, 6);
                if (Game.gameBoard[position.X, position.Y] == GameObjects.empty)
                    return;
            }
        }

        public void Act()
        {
            int xPlus = Math.Clamp(position.X + 1, 0, 7);
            int xMinus = Math.Clamp(position.X - 1, 0, 7);
            int yPlus = Math.Clamp(position.Y + 1, 0, 7);
            int yMinus = Math.Clamp(position.Y - 1, 0, 7);

            //Check to see if any of the adjacent squares to the enemy are the player, if they are call Attack()
            if (Game.gameBoard[xPlus, position.Y] == GameObjects.player ||
                Game.gameBoard[xMinus, position.Y] == GameObjects.player ||
                Game.gameBoard[position.X, yPlus] == GameObjects.player ||
                Game.gameBoard[position.X, yMinus] == GameObjects.player)
            {
                //Ensure that the player can't get attacked while in the exit zone.
                if (!new Point(xPlus, position.Y).Equals(new Point(7, 0)) &&
                    !new Point(position.X, yMinus).Equals(new Point(7, 0)))
                    Attack();
            }
        }

        public void Move()
        {
            //Enemy can't move if it attacked last turn
            if (recentlyAttacked)
            {
                recentlyAttacked = false;
                return;
            }
            //There is a 40% chance the enemy moves towards the player. If it fails that there is a 40% chance
            //it moves in a random direction. If it fails that too then it does not move this turn.
            int direction = 0;
            if (Game.rnd.Next(0, 100) < 40)
            {
                if (player.Position.X > position.X && Game.gameBoard[position.X + 1, position.Y] == GameObjects.empty)
                    direction = 3; //right
                if (player.Position.X < position.X && Game.gameBoard[position.X - 1, position.Y] == GameObjects.empty)
                    direction = 1; //left
                if (player.Position.Y > position.Y && Game.gameBoard[position.X, position.Y + 1] == GameObjects.empty)
                    direction = 2; //down
                if (player.Position.Y < position.Y && Game.gameBoard[position.X, position.Y - 1] == GameObjects.empty)
                    direction = 0; //up
            }
            else if (Game.rnd.Next(0, 100) < 40)
            {
                direction = Game.rnd.Next(0, 4);
            }
            else
                return;

            //int 0 - 3 controls direction of movement. This could probably be more clear with an enum (future goals).
            switch (direction)
            {
                case (0):        //up
                    if (position.Y > 0)
                    {
                        if (Game.gameBoard[position.X, position.Y - 1] == GameObjects.empty)
                        {
                            position.Y--;
                        }
                    }
                    break;
                case (1):      //left
                    if (position.X > 0)
                    {
                        if (Game.gameBoard[position.X - 1, position.Y] == GameObjects.empty)
                        {
                            position.X--;
                        }
                    }
                    break;
                case (2):      //down
                    if (position.Y < 7)
                    {
                        if (Game.gameBoard[position.X, position.Y + 1] == GameObjects.empty)
                        {
                            position.Y++;
                        }
                    }
                    break;
                case (3):     //right
                    if (position.X < 7)
                    {
                        if (Game.gameBoard[position.X + 1, position.Y] == GameObjects.empty)
                        {
                            position.X++;
                        }
                    }
                    break;
            }
        }
        //Attack has its own method so that it can raise the event EnemyAttack and allow any listeners (renderer)
        //to respond to it. It also has a sleep that is the duration of the attack animation so that the player
        //can't move until the animation is finished. It is short so doesn't interrupt gameplay.
        private void Attack()
        {
            EnemyAttackEventArgs args = new EnemyAttackEventArgs();
            args.Position = position;
            OnEnemyAttack(args);
            recentlyAttacked = true;
            Thread.Sleep((1000 / 240) * 60);
            player.TakeDamage();
        }
        protected virtual void OnEnemyAttack(EnemyAttackEventArgs e)
        {
            EventHandler<EnemyAttackEventArgs> handler = EnemyAttack;
            handler?.Invoke(this, e);
        }
    }
}

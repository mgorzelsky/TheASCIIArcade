using System;
using System.Drawing;

namespace EscapeFromDarkForest
{
    public class EnemyAttackEventArgs : EventArgs
    {
        public Point Position { get; set; }
    }
}
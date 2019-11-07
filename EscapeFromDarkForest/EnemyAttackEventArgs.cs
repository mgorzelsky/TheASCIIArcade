using System;
using System.Drawing;
//This class allows the event to pass along information, specifically point information of
//which enemy is attacking so that only that one animates.
namespace EscapeFromDarkForest
{
    public class EnemyAttackEventArgs : EventArgs
    {
        public Point Position { get; set; }
    }
}
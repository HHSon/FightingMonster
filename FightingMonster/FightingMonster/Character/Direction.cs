using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightingMonster.Character
{
    public enum Direction : int
    {
        /*             Top
         *              |
         *              |
         *     left ---- ----- Right
         *              |
         *              |
         *            Bottom
         */
        
        Left = 0,
        LeftTop,
        Top,
        TopRight,
        Right,
        RightBottom,
        Bottom,
        BottomLeft
    }
}

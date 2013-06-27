using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightingMonster.Character
{
    public enum CharacterState : int
    {
        Standing = 0,
        Walking,
        Running,
        Attacking,
        IsAttacked
    }
}

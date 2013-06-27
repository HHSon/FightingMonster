using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.Map;

namespace FightingMonster.Character
{
    public class Warrior : Human
    {
        protected override void MoveToTargetPosition()
        {
            base.MoveToTargetPosition();

            if ((_state == CharacterState.Walking) || (_state == CharacterState.Running))
                if (_map != null)
                    _map.Move(stepX, stepY);
        }
    }
}

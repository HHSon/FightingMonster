using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace FightingMonster.Character
{
    public class SilverWarrior : Warrior
    {
        public SilverWarrior()
            : base()
        {
            Width = 70;
            Height = 90;
            PositionX = 400;
            PositionY = 400;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            _standingTextures[(int)Direction.BottomLeft].LoadContent(@"Image/Human/SilverWarrior/Standing_BottomLeft", 1, 12, content);
            _standingTextures[(int)Direction.Left].LoadContent(@"Image/Human/SilverWarrior/Standing_Left", 1, 12, content);
            _standingTextures[(int)Direction.LeftTop].LoadContent(@"Image/Human/SilverWarrior/Standing_LeftTop", 1, 12, content);
            _standingTextures[(int)Direction.Right].LoadContent(@"Image/Human/SilverWarrior/Standing_Right", 1, 12, content);
            _standingTextures[(int)Direction.RightBottom].LoadContent(@"Image/Human/SilverWarrior/Standing_RightBottom", 1, 12, content);
            _standingTextures[(int)Direction.TopRight].LoadContent(@"Image/Human/SilverWarrior/Standing_TopRight", 1, 12, content);


            _walkingTextures[(int)Direction.BottomLeft].LoadContent(@"Image/Human/SilverWarrior/Walking_BottomLeft", 1, 8, content);
            _walkingTextures[(int)Direction.Left].LoadContent(@"Image/Human/SilverWarrior/Walking_Left", 1, 8, content);
            _walkingTextures[(int)Direction.LeftTop].LoadContent(@"Image/Human/SilverWarrior/Walking_LeftTop", 1, 8, content);
            _walkingTextures[(int)Direction.Right].LoadContent(@"Image/Human/SilverWarrior/Walking_Right", 1, 8, content);
            _walkingTextures[(int)Direction.RightBottom].LoadContent(@"Image/Human/SilverWarrior/Walking_RightBottom", 1, 8, content);
            _walkingTextures[(int)Direction.TopRight].LoadContent(@"Image/Human/SilverWarrior/Walking_TopRight", 1, 8, content);

            base.LoadContent(content);
        }

        public override Direction GetDirection(float pX, float pY)
        {
            Direction direction = base.GetDirection(pX, pY);

            if (direction == Direction.Top)
                return Direction.TopRight;

            if (direction == Direction.Bottom)
                return Direction.RightBottom;

            return direction;
        }
    }
}

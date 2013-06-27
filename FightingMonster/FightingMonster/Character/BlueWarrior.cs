using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TSLibrary.Input;

namespace FightingMonster.Character
{
    public class BlueWarrior : Warrior
    {
        public BlueWarrior() : base()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            _standingTextures[(int)Direction.BottomLeft].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_BottomLeft"));
            _standingTextures[(int)Direction.Left].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_Left"));
            _standingTextures[(int)Direction.LeftTop].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_LeftTop"));
            _standingTextures[(int)Direction.Right].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_Right"));
            _standingTextures[(int)Direction.RightBottom].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_RightBottom"));
            _standingTextures[(int)Direction.TopRight].add(content.Load<Texture2D>(@"Image/Human/Blue/Standing_TopRight"));


            _walkingTextures[(int)Direction.BottomLeft].LoadContent(@"Image/Human/Blue/Walking_BottomLeft", 1, 8, content);
            _walkingTextures[(int)Direction.Left].LoadContent(@"Image/Human/Blue/Walking_Left", 1, 8, content);
            _walkingTextures[(int)Direction.LeftTop].LoadContent(@"Image/Human/Blue/Walking_LeftTop", 1, 8, content);
            _walkingTextures[(int)Direction.Right].LoadContent(@"Image/Human/Blue/Walking_Right", 1, 8, content);
            _walkingTextures[(int)Direction.RightBottom].LoadContent(@"Image/Human/Blue/Walking_RightBottom", 1, 8, content);
            _walkingTextures[(int)Direction.TopRight].LoadContent(@"Image/Human/Blue/Walking_TopRight", 1, 8, content);

            base.LoadContent(content);

            State = CharacterState.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

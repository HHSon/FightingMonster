using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.GameEntity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FightingMonster.GameEntity
{
    public class Tree : TSGameEntity
    {
        public static Texture2D TreeTexture;

        public override void LoadContent(ContentManager content)
        {
            if (content == null)
                return;

            if (TreeTexture == null)
                TreeTexture = content.Load<Texture2D>("Image/GameEntity/Tree/Tree0");

            Random random = new Random();
            this.Width = random.Next(100, 200);
            this.Height = this.Width * TreeTexture.Height / TreeTexture.Width;
            this.Background.add(TreeTexture);


            base.LoadContent(content);
        }
    }
}

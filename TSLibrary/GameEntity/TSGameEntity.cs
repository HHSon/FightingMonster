using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSLibrary.Texture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TSLibrary.GameEntity
{
    public class TSGameEntity : TSVisibleGameObject
    {
		protected TSTexture2DList _background;
		
		#region Property Region
		
		public TSTexture2DList Background
		{
			get { return _background; }
			set { _background = value; }
		}
		
		#endregion
		
		
		public TSGameEntity()
		{
			_background = new TSTexture2DList();
		}
		
		public TSGameEntity(TSTexture2DList background)
		{
			_background = background;
		}
		
		public override void Update(GameTime gameTime)
		{
			if (_enabled == false)
				return;
				
			if (_background != null)
				_background.Update(gameTime);
		}
		
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (_visibled == false)
				return;
				
			if (_background != null)
				if (_background.CurrentTexture != null)
					spriteBatch.Draw(
								_background.CurrentTexture, 
								new Rectangle((int)_position.X, (int)_position.Y, _width, _height), 
								Color.White);
		}

        public void Draw(Vector2 position, GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_visibled == false)
                return;

            if (_background != null)
                if (_background.CurrentTexture != null)
                    spriteBatch.Draw(
                                _background.CurrentTexture,
                                new Rectangle((int)position.X, (int)position.Y, _width, _height),
                                Color.White);
        }
    }
}

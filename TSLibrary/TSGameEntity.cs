using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TSLibrary
{
    public abstract class TSGameEntity
    {
        protected bool _enabled = true;


        #region Property Region

        public virtual bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        #endregion


        public virtual void Initialize() 
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}

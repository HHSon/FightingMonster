using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TSLibrary.Layout;

namespace TSLibrary.Screen
{
    public class TSScreenManager : TSInvisibleGameEntity
    {
        private List<TSScreen> _screens;
        private int _currScreenIdx;

        public TSScreenManager()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void ShowMessageBox(TSLayout layout)
        {

        }
    }
}

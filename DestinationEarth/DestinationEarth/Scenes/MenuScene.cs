using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.MenuComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.Scenes
{
    /// <summary>
    /// Scene that holds the menu screen.
    /// </summary>
    class MenuScene : GameScene
    {
        public MenuScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new Background(Game, "SpaceBg", 1));
            AddComponent(new MenuScreen(Game));

            base.Initialize();
        }
    }
}

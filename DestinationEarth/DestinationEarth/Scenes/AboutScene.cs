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
    /// Scene that holds the About Screen.
    /// </summary>
    class AboutScene : GameScene
    {
        public AboutScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new AboutScreen(Game));

            base.Initialize();
        }

        /// <summary>
        /// Pressing escape will return to the main menu.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Escape))
            {
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<MenuScene>().Show();
            }

            base.Update(gameTime);
        }
    }
}

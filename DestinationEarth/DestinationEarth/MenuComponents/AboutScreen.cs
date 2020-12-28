using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.MenuComponents
{
    /// <summary>
    /// Defines the About screen display, which just
    /// includes an image showing the authors of the game.
    /// </summary>
    class AboutScreen : DrawableGameComponent
    {
        Texture2D texture;
        Vector2 position;

        public AboutScreen(Game game) : base(game)
        {
            this.position = Vector2.Zero;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("About-screen");

            base.LoadContent();
        }

        /// <summary>
        /// Displays the about image which shows the game's authors.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}

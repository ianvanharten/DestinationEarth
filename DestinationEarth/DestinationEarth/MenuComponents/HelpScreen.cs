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
    /// Defines the Help screen display, which includes
    /// an image describing how to play the game.
    /// </summary>
    class HelpScreen : DrawableGameComponent
    {
        Texture2D texture;
        Vector2 position;

        public HelpScreen(Game game) : base(game)
        {
            this.position = Vector2.Zero;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Help-screen");

            base.LoadContent();
        }

        /// <summary>
        /// Displays the help image, which explains the game
        /// and how to play.
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

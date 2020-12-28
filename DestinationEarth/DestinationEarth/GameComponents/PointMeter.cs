using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth
{
    /// <summary>
    /// Keeps track of the player score throughout
    /// the game, and displays it to the screen.
    /// </summary>
    class PointMeter : DrawableGameComponent
    {
        SpriteFont font;

        /// <summary>
        /// Holds the position of the score
        /// displayed on the screen.
        /// </summary>
        Vector2 position;

        /// <summary>
        /// Holds the player's score.
        /// </summary>
        public int playerScore;

        public PointMeter(Game game) : base(game)
        {
            position = Vector2.Zero;
            playerScore = 0;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("fuelmeter");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(font, $"Score: {playerScore}", position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}

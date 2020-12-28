using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.GameComponents
{
    /// <summary>
    /// Defines the screen that is displayed when 
    /// the player wins the game. Shows the high score,
    /// saves it to the high score list, and resets the game.
    /// </summary>
    class WinScreen : DrawableGameComponent
    {
        SpriteFont regularFont;
        Color regularColor = Color.White;
        Vector2 position;

        string winMessage;

        int playerScore;


        public WinScreen(Game game) 
            : base(game)
        {           
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2);
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");

            base.LoadContent();
        }

        /// <summary>
        /// Update gets the playerScore from the PointMeter.
        /// When the user presses Enter, their score is saved
        /// to the file.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            playerScore = Game.Services.GetService<PointMeter>().playerScore;

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Enter))
            {
                int finalScore = playerScore;
                Game.Services.GetService<HighScoreWriter>().AddNewScoreToFile(finalScore);
                ResetGame();
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<HighScoreScene>().Show();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Displays the winning message to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            winMessage = $"Congratulations! You win!\nScore: {playerScore}";

            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(regularFont, winMessage, position, regularColor);
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// To reset the game, remove the player and the point meter,
        /// set them both to null, and reset the rest of the level.
        /// </summary>
        private void ResetGame()
        {
            Player player = Game.Services.GetService<Player>();
            PointMeter pointMeter = Game.Services.GetService<PointMeter>();

            if (player != null)
            {
                Game.Services.RemoveService(player.GetType());
                player = null;
            }

            Game.Services.RemoveService(pointMeter.GetType());
            pointMeter = null;

            ((Game1)Game).Reset(Game.Services.GetService<LevelOneScene>());
            Game.Services.GetService<MenuScene>().Show();
        }
    }
}

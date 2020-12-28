using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DestinationEarth.GameComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.MenuComponents
{
    /// <summary>
    /// Defines the High Score screen display
    /// </summary>
    class HighScoreScreen : DrawableGameComponent
    {
        const int PADDING_WIDTH = 100;
        const int PADDING_HEIGHT = 10;       

        SpriteFont regularFont;
        Color regularColor = Color.White;
        Color highlightedColor = Color.Orange;
        Vector2 startingPosition;
        string title = "High Scores";
        string returnToMainMessage = "Press 'Escape' to return to Main Menu";

        // Using the HighScoreWriter class to get the most up to date
        // high scores.
        HighScoreWriter highScoreWriter;
        string[] scoresFromFile;

        public HighScoreScreen(Game game) : base(game)
        {
           highScoreWriter = Game.Services.GetService<HighScoreWriter>();
            startingPosition = new Vector2(PADDING_WIDTH, PADDING_HEIGHT);
        }

        /// <summary>
        /// Get all the high scores from the file and save to
        /// the scoresFromFile string array.
        /// </summary>
        public override void Initialize()
        {
            scoresFromFile = highScoreWriter.ReadScoresFromFile();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");

            base.LoadContent();
        }

        /// <summary>
        /// Update ensures that the scoresFromFile string array
        /// has the most up to date scores from the game.
        /// The escape key takes the user back to the main menu.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            scoresFromFile = Game.Services.GetService<HighScoreWriter>().ReadScoresFromFile();

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Escape))
            {
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<MenuScene>().Show();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Displays the title, list of scores, and a message
        /// for how to return to the main menu.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();
            sb.DrawString(regularFont, title, nextPosition, highlightedColor);
            nextPosition.Y += regularFont.LineSpacing;

            for (int score = 0; score < scoresFromFile.Length; score++)
            {
                string scorePosition = (score + 1).ToString();
                string scoreToPrint = $"{scorePosition}.    {scoresFromFile[score]}";

                sb.DrawString(regularFont, scoreToPrint, nextPosition, regularColor);
                nextPosition.Y += regularFont.LineSpacing;
            }

            sb.DrawString(regularFont, returnToMainMessage, nextPosition, highlightedColor);

            sb.End();

            base.Draw(gameTime);
        }
    }
}

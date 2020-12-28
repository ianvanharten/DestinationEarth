using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinationEarth.GameComponents;
using DestinationEarth.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DestinationEarth.MenuComponents
{
    enum MenuSelection
    {
        StartGame,
        HighScores,
        Help,
        About,
        Quit
    }

    /// <summary>
    /// Defines the menu screen display.
    /// </summary>
    class MenuScreen : DrawableGameComponent
    {
        const int TITLE_PADDING = 50;

        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int selectedIndex;
        private Vector2 startingPosition;

        private string title;
        private Vector2 titlePosition;

        private Color regularColor = Color.White;
        private Color highlightColor = Color.Orange;

        private KeyboardState prevKS;

        public MenuScreen(Game game) : base(game)
        {
            title = "DESTINATION EARTH";
            menuItems = new List<string>
            {
                "Start Game",
                "High Scores",
                "Help",
                "About",
                "Quit"
            };
            startingPosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2);
            titlePosition = new Vector2(TITLE_PADDING, TITLE_PADDING);
            prevKS = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");
            highlightFont = Game.Content.Load<SpriteFont>("selectedItemFont");

            // Use the MusicPlayer class to play the menu song.
            Game.Services.GetService<MusicPlayer>().PlayMenuSong();

            base.LoadContent();
        }

        /// <summary>
        /// Update checks for input from either the up or down
        /// key to move up or down the menu, and the enter key
        /// to make a selection.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && prevKS.IsKeyUp(Keys.Down))
            {
                selectedIndex++;

                if (selectedIndex >= menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            else if (ks.IsKeyDown(Keys.Up) && prevKS.IsKeyUp(Keys.Up))
            {
                selectedIndex--;

                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            else if (ks.IsKeyDown(Keys.Enter) && prevKS.IsKeyUp(Keys.Enter))
            {
                SwitchScenes();
            }

            prevKS = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the game title and menu items to the screen, checking
        /// which is the active item to give it a highlighted colour.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();

            sb.DrawString(regularFont, title, titlePosition, regularColor);

            for (int menuItem = 0; menuItem < menuItems.Count; menuItem++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                if (menuItem == selectedIndex)
                {
                    activeFont = highlightFont;
                    activeColor = highlightColor;
                }

                sb.DrawString(activeFont, menuItems[menuItem], nextPosition, activeColor);

                nextPosition.Y += regularFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method called when the user selects an option from
        /// the menu. First hide all scenes, then get the service
        /// of the selected scene and show it.
        /// </summary>
        private void SwitchScenes()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)selectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<MusicPlayer>().PlayGameSong();
                    Game.Services.GetService<LevelOneScene>().Show();
                    break;

                case MenuSelection.HighScores:
                    Game.Services.GetService<HighScoreScene>().Show();
                    break;

                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;

                case MenuSelection.About:
                    Game.Services.GetService<AboutScene>().Show();
                    break;

                case MenuSelection.Quit:
                    Game.Exit();
                    break;

                default:
                    Game.Services.GetService<MusicPlayer>().PlayMenuSong();
                    Game.Services.GetService<MenuScene>().Show();
                    break;
            }
        }
    }
}

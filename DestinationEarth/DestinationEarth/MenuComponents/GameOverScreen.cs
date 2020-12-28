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

namespace DestinationEarth.MenuComponents
{
    enum GameOverSelection
    {
        MainMenu,
        Quit
    }

    /// <summary>
    /// Defines the Game Over screen, which is displayed
    /// if the player dies. It gives the option to either
    /// go back to the main menu, or to quit the game.
    /// </summary>
    class GameOverScreen : DrawableGameComponent
    {
        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int selectedIndex;
        private Vector2 startingPosition;

        private Color regularColor = Color.White;
        private Color highlightColor = Color.Orange;

        private KeyboardState prevKS;

        public GameOverScreen(Game game) : base(game)
        {
            menuItems = new List<string>
            {
                "Back to Main Menu",
                "Quit"
            };
            startingPosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 2);
            prevKS = Keyboard.GetState();
        }

        protected override void LoadContent()
        {
            regularFont = Game.Content.Load<SpriteFont>("menuFont");
            highlightFont = Game.Content.Load<SpriteFont>("selectedItemFont");

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
        /// Draw the menu items to the screen, checking
        /// which is the active item to give it a highlighted colour.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();
            sb.DrawString(regularFont, "You died.", nextPosition, regularColor);
            nextPosition.Y += regularFont.LineSpacing * 2;

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
        /// This method is called when the user selects an
        /// option from the menu. First hide all scenes,
        /// then call and show the selected scene.
        /// </summary>
        private void SwitchScenes()
        {
            ((Game1)Game).HideAllScenes();

            switch ((GameOverSelection)selectedIndex)
            {
                case GameOverSelection.MainMenu:
                    Game.Services.GetService<MusicPlayer>().PlayMenuSong();
                    Game.Services.GetService<MenuScene>().Show();
                    break;

                case GameOverSelection.Quit:
                    Game.Exit();
                    break;

                default:
                    Game.Services.GetService<MenuScene>().Show();
                    break;

            }
        }
    }
}

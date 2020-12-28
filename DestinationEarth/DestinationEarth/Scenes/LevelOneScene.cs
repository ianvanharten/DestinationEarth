using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.Scenes
{
    /// <summary>
    /// Scene that holds and enables the main gameplay components.
    /// </summary>
    public class LevelOneScene : GameScene
    {
        public LevelOneScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            this.AddComponent(new Background(Game, "SpaceBg", 1));
            this.AddComponent(new SpaceObjectGenerator(Game, this));

            PointMeter fuelMeter = new PointMeter(Game);
            this.AddComponent(fuelMeter);
            Game.Services.AddService<PointMeter>(fuelMeter);

            Player player = new Player(Game);
            this.AddComponent(player);
            Game.Services.AddService<Player>(player);

            base.Initialize();
        }

        /// <summary>
        /// Pressing escape will pause the game and go back to
        /// the main menu.
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

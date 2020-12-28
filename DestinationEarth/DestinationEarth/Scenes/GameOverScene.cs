using DestinationEarth.MenuComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationEarth.Scenes
{
    /// <summary>
    /// Scene that holds the GameOver Screen.
    /// </summary>
    class GameOverScene : GameScene
    {
        public GameOverScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new GameOverScreen(Game));

            base.Initialize();
        }
    }
}

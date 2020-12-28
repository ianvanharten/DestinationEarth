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
    /// Scene that holds the High Score Screen.
    /// </summary>
    class HighScoreScene : GameScene
    {
        public HighScoreScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new HighScoreScreen(Game));

            base.Initialize();
        }
    }
}

using DestinationEarth.GameComponents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinationEarth.Scenes
{
    /// <summary>
    /// Scene that holds the Win Game screen.
    /// </summary>
    class WinScene : GameScene
    {
        public WinScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            AddComponent(new WinScreen(Game));

            base.Initialize();
        }
    }
}

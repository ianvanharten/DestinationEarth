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

namespace DestinationEarth
{
    /// <summary>
    /// Controls the creation, placement, speed,
    /// and release of all the obstacles and point 
    /// objects onto the game screen.
    /// </summary>
    class SpaceObjectGenerator : GameComponent
    {
        const int MAX_OBJECT_HEIGHT = 30;
        const float ASTEROID_INTERVAL = 0.7f;
        const float POINT_INTERVAL = 1.0f;
        const float SPACESHIP_INTERVAL = 4.0f;
        const int POINT_OBJECT_SPEED = 3;

        /// <summary>
        /// Used to get random speeds and object
        /// types where applicable.
        /// </summary>
        Random random;

        GameScene parentScene;

        /* 
         * Each object type has a quantity, a list
         * to hold that quantity, an index to keep track
         * of which is the current object in the list,
         * and an interval timer to track when to release
         * the next one.
         */

        int numberOfAsteroids = 85;  // 85
        List<Asteroid> asteroids;      
        int asteroidIndex = 0;
        double asteroidIntervalTimer = 0;

        int numberOfPointObjects = 50;  // 50
        List<PointsObject> pointObjects;
        int pointIndex = 0;
        double pointIntervalTimer = 0;

        int numberOfSpaceShips = 15;  // 15
        List<SpaceShip> spaceShips;
        int spaceShipIndex = 0;
        double spaceShipIntervalTimer = 0;

        // The planet has only one instance, and it is
        // released at the end of the level.
        List<Planet> planets;
        int planetIndex = 0;
        

        public SpaceObjectGenerator(Game game, GameScene parent) 
            : base(game)
        {
            parentScene = parent;
            random = new Random();
            asteroids = new List<Asteroid>();
            pointObjects = new List<PointsObject>();
            spaceShips = new List<SpaceShip>();
            planets = new List<Planet>();
        }

        /// <summary>
        /// Loads each game object list with objects according to the quantities
        /// defined above. Methods using the random variable used to get random
        /// positions, speeds, and object types.
        /// </summary>
        public override void Initialize()
        {
            for (int a = 0; a < numberOfAsteroids; a++)
            {
                asteroids.Add(new Asteroid(Game, CreateRandomPosition(), CreateRandomSpeed(), GetRandomAsteroidType()));
            }

            for (int f = 0; f < numberOfPointObjects; f++)
            {
                pointObjects.Add(new PointsObject(Game, CreateRandomPosition(), POINT_OBJECT_SPEED));
            }

            for (int s = 0; s < numberOfSpaceShips; s++)
            {
                spaceShips.Add(new SpaceShip(Game, CreateRandomPosition(), CreateRandomSpeed(), GetRandomSpaceShipType()));
            }

            planets.Add(new Planet(Game));

            base.Initialize();
        }

        /// <summary>
        /// Keeps track of the time passed, and adds the different
        /// objects from their lists into the game based on their interval timers.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            asteroidIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (asteroidIndex < numberOfAsteroids)
            {
                if (asteroidIntervalTimer >= ASTEROID_INTERVAL)
                {
                    parentScene.AddComponent(asteroids[asteroidIndex]);
                    asteroidIndex++;

                    asteroidIntervalTimer = 0;
                }
            }

            pointIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (pointIndex < numberOfPointObjects)
            {
                if (pointIntervalTimer >= POINT_INTERVAL)
                {
                    parentScene.AddComponent(pointObjects[pointIndex]);
                    pointIndex++;

                    pointIntervalTimer = 0;
                }
            }

            spaceShipIntervalTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spaceShipIndex < numberOfSpaceShips)
            {
                if (spaceShipIntervalTimer >= SPACESHIP_INTERVAL)
                {
                    parentScene.AddComponent(spaceShips[spaceShipIndex]);
                    spaceShipIndex++;

                    spaceShipIntervalTimer = 0;
                }
            }

            // Once the asteroids have all passed, add the planet
            if (asteroidIndex >= numberOfAsteroids)
            {
                if (planetIndex == 0)
                {
                    parentScene.AddComponent(planets[planetIndex]);
                    planetIndex++;
                }               
            }

            base.Update(gameTime);
        }

        private AsteroidType GetRandomAsteroidType()
        {
            return (AsteroidType)random.Next(0, 4);
        }

        private SpaceShipType GetRandomSpaceShipType()
        {
            return (SpaceShipType)random.Next(0, 2);
        }

        private Vector2 CreateRandomPosition()
        {
            return new Vector2(Game.GraphicsDevice.Viewport.Width,
                random.Next(0, Game.GraphicsDevice.Viewport.Height - MAX_OBJECT_HEIGHT));
        }

        private int CreateRandomSpeed()
        {
            return random.Next(5, 10);
        }
    }
}

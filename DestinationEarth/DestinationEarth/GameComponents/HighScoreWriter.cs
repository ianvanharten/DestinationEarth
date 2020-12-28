using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DestinationEarth.GameComponents
{
    /// <summary>
    /// Used to create, add, and store high scores
    /// in the game.
    /// </summary>
    public class HighScoreWriter : GameComponent
    {
        StreamWriter writer;

        /// <summary>
        /// Holds the name of the high score file.
        /// </summary>
        public string fileName;

        /// <summary>
        /// Holds a list of all the recorded high scores.
        /// </summary>
        public List<int> highScores;

        public HighScoreWriter(Game game) : base(game)
        {
            fileName = "HighScoreList.txt";
        }

        /// <summary>
        /// Initializes the high score list with initial values
        /// and writes them to the file. This method is only called
        /// if the file does not already exist.
        /// </summary>
        public void CreateHighScoreListFile()
        {
            highScores = new List<int>() { 30, 20, 10 };

            using (writer = new StreamWriter(fileName))
            {
                foreach (int score in highScores)
                {
                    writer.WriteLine($"{score}");
                }
            }
        }

        /// <summary>
        /// Add a new score to the high score list. First
        /// checks that the file exists, adds the new score
        /// to the list, sorts the list, if list contains over
        /// ten scores, remove the last one, then write the scores 
        /// to the file.
        /// </summary>
        /// <param name="playerScore"></param>
        public void AddNewScoreToFile(int playerScore)
        {
            if (!File.Exists(fileName))
            {
                CreateHighScoreListFile();
            }

            highScores.Add(playerScore);
            SortHighScores();

            if (highScores.Count > 10)
            {                
                highScores.Remove(10);
            }           

            using (writer = new StreamWriter(fileName))
            {
                foreach (int score in highScores)
                {
                    writer.WriteLine(score);
                }
                
            }           
        }

        /// <summary>
        /// Read the scores from the high score file
        /// into a string array. Used to transfer the 
        /// data to the high score screen. Also checks
        /// the high score list to ensure it is updated.
        /// </summary>
        /// <returns></returns>
        public string[] ReadScoresFromFile()
        {
            if (!File.Exists(fileName))
            {
                CreateHighScoreListFile();
            }

            string[] scoresFromFile = File.ReadAllLines(fileName);

            if (highScores == null)
            {
                highScores = new List<int>();
                for (int i = 0; i < scoresFromFile.Length; i++)
                {
                    int score = Convert.ToInt32(scoresFromFile[i]);
                    highScores.Add(score);
                }
            }

            return scoresFromFile;
        }

        /// <summary>
        /// Sorts the high score list from highest to lowest.
        /// Then overwrites the file to show the scores in the proper
        /// order.
        /// </summary>
        public void SortHighScores()
        {
            highScores.Sort();
            highScores.Reverse();

            using (writer = new StreamWriter(fileName))
            {
                foreach (int score in highScores)
                {
                    writer.WriteLine($"{score}");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Platformer.Managers
{
    //This class is in charge of handling scores
    public class ScoreManager
    {
        //This follows the singleton design pattern.
        //This means that there is single instance of this class over the whole
        //projects
        private static ScoreManager instance = null;
        public static ScoreManager Instance
        {
            //Getter function
            get
            {

                //If there is no isntance, then make one
                if (instance == null)
                    instance = new ScoreManager();
                return instance;
            }
        }

        //Used to map a level to its high score
        private Dictionary<string, int> scoreDictionary = new Dictionary<string, int>();

        private ScoreManager()
        {
            //ReadScoreFile();
        }

        private void ReadScoreFile()
        {
            //Make sure the dictionary is clean
            /*scoreDictionary = new Dictionary<string, int>();

            //Application.persistentDataPath returns a path that can be used to 
            //create files that persist through restarts
            string filePath = Application.persistentDataPath + "/Score.data";

            //If the file doesn't exist, then returns
            if (!File.Exists(filePath))
                return;

            //Open the file
            FileStream fStream = new FileStream(filePath, FileMode.Open);

            //Read every byte in
            byte[] data = new byte[fStream.Length];
            fStream.Read(data, 0, (int)fStream.Length);

            string line = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '\n')
                {
                    //The level identifier and high score are split by ':'
                    string[] split = line.Split(':');
                    scoreDictionary.Add(split[0], int.Parse(split[1]));
                    //Clear prev lines
                    line = "";
                }
                //If not a new line char, then it is a part of the line
                else
                {
                    line += (char)data[i];
                }
            }*/
        }

        //Updates the score file with the all of the data in the dictionary
        private void UpdateScoreFile()
        {
            //Make sure to use the same file path
            /*string filePath = Application.persistentDataPath + "/Score.data";
            //FileMode.Create will clear old contents
            FileStream fStream = new FileStream(filePath, FileMode.Create);
            //Iterate through each element in the dictionarys
            foreach (string key in scoreDictionary.Keys)
            {
                string line = key + ':' + scoreDictionary[key].ToString() + '\n';
                fStream.Write(Encoding.ASCII.GetBytes(line), 0, line.Length);
            }
            fStream.Close();*/
        }

        //Returns the high score for the provided level index
        public int GetHighScore(int levelIndex)
        {
            //If there is not a high score for the level, return a 0
            /*if (scoreDictionary.ContainsKey("Level" + levelIndex))
            {
                return scoreDictionary["Level" + levelIndex];
            }
            else
            {
                return 0;
            }*/
            return 0;
        }

        //Sets  anew high score for the provided level index
        //Will check to make sure the score is actually a high score
        public void SetHighScore(int levelIndex, int score)
        {
            /*if (scoreDictionary.ContainsKey("Level" + levelIndex))
            {
                if (scoreDictionary["Level" + levelIndex] < score)
                {
                    scoreDictionary["Level" + levelIndex] = score;
                    UpdateScoreFile();
                }
            }
            else
            {
                scoreDictionary["Level" + levelIndex] = score;
                UpdateScoreFile();
            }*/
        }
    }
}

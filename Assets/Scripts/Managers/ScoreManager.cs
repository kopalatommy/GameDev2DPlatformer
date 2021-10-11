using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace Platformer.Managers
{
    public class ScoreManager
    {
        private static ScoreManager instance = null;

        private Dictionary<string, int> scoreDict = new Dictionary<string, int>();

        public static ScoreManager Instance { 
            get 
            {
                if(instance == null)
                {
                    instance = new ScoreManager();
                }
                return instance;
            }
        }

        public int GetHighScore(int levelIndex)
        {
            if(scoreDict.ContainsKey("Level" + levelIndex))
            {
                return scoreDict["Level" + levelIndex];
            }
            else
            {
                return 0;
            }
        }

        public void SetHighScore(int levelIndex, int score)
        {
            if(scoreDict.ContainsKey("Level" + levelIndex))
            {
                if(scoreDict["Level" + levelIndex] < score)
                {
                    scoreDict["Level" + levelIndex] = score;
                    UpdateScoreFile();
                }
            }
            else
            {
                scoreDict["Level" + levelIndex] = score;
                UpdateScoreFile();
            }
        }

        private ScoreManager()
        {
            ReadScoreFile();
        }

        private void ReadScoreFile()
        {
            scoreDict = new Dictionary<string, int>();

            string filePath = Application.persistentDataPath + "/Score.data";
            Debug.Log(filePath);

            if (!File.Exists(filePath))
                return;
            
            FileStream fStream = new FileStream(filePath, FileMode.Open);

            byte[] data = new byte[fStream.Length];
            fStream.Read(data, 0, (int)fStream.Length);

            string line = "";
            for(int i = 0; i < data.Length; i++)
            {
                if(data[i] == '\n')
                {
                    string[] split = line.Split(':');
                    scoreDict.Add(split[0], int.Parse(split[1]));
                    Debug.Log(line);
                    line = "";
                }
                else
                {
                    line += (char)data[i];
                }
            }
        }

        private void UpdateScoreFile()
        {
            string filePath = Application.persistentDataPath + "/Score.data";
            FileStream fStream = new FileStream(filePath, FileMode.Create);

            Dictionary<string, int> ks = new Dictionary<string, int>();
            
            foreach(string key in scoreDict.Keys)
            {
                string line = key + ':' + scoreDict[key].ToString() + '\n';
                fStream.Write(Encoding.ASCII.GetBytes(line), 0, line.Length);
            }
            fStream.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Pong
{
    class GameSaver
    {
        readonly static string path = "D:/PhantomPower/Test/Pong/Save/save.txt";

        public static void Save(int playerScore, int enemyScore)
        {
            StreamWriter writer = new StreamWriter(path, false);
            string line = $"{playerScore} {enemyScore}";
            writer.WriteLine(line);
            writer.Close();
        }

        public static void Load(out int playerScore, out int enemyScore)
        {
            if (!File.Exists(path))
            {
                playerScore = 0;
                enemyScore = 0;
                return; 
            }     

            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            string[] data = line.Split(' ');
            playerScore = int.Parse(data[0]);
            enemyScore = int.Parse(data[1]); 
            reader.Close();
        }
    }
}

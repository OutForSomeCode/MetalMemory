using System;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetalMemory
{
    class SaveLoad
    {
        private static BinaryFormatter BinaryFormatter = new BinaryFormatter();
        private static string SaveFile = "SaveGames/memory.sav";

        private static Stream SaveFileStream = File.Open(SaveFile, FileMode.Create);

        private static void Save(Stream SaveFileStream, Object obj)
        {
            BinaryFormatter.Serialize(SaveFileStream, obj);
        }

        public static void SaveSomthing()
        {
            Save(SaveFileStream, InitializeCards.GetCardList);
            Save(SaveFileStream, GameLogic.TurnOfPlayer1);
            Save(SaveFileStream, InitializeGame.Player1);
            Save(SaveFileStream, GameLogic.ScoreOfPlayer1);
            Save(SaveFileStream, InitializeGame.Player2);
            Save(SaveFileStream, GameLogic.ScoreOfPlayer2);

            SaveFileStream.Close();
        }

        private bool CanLoad()
        {
            if (File.Exists(SaveFile))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private Object Load(Stream SaveFileStream)
        {
            return BinaryFormatter.Deserialize(SaveFileStream);
        }
    }
}

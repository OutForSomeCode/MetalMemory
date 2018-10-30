using System;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace MetalMemory
{
    class SaveLoad
    {
        private static string SaveFile = "SaveGames/memory.sav";

        [Serializable]
        public class SaveData
        {
            public List<InitializeCards.CardTagData> SavecardTagData;
            public string SavePlayer1;
            public string SavePlayer2;
            public int SaveScoreOfPlayer1;
            public int SaveScoreOfPlayer2;
            public int SaveGridColumn;
            public int SaveGridRows;
            public bool SaveTurnOfPlayer1;
        }

        public static void SaveSomething()
        {
            SaveData Save = new SaveData();

            Save.SavecardTagData = InitializeCards.GetTagDataList;
            Save.SavePlayer1 = InitializeGame.Player1;
            Save.SavePlayer2 = InitializeGame.Player2;
            Save.SaveScoreOfPlayer1 = GameLogic.ScoreOfPlayer1;
            Save.SaveScoreOfPlayer2 = GameLogic.ScoreOfPlayer2;
            Save.SaveGridColumn = InitializeGame.GridColumn;
            Save.SaveGridRows = InitializeGame.GridRows;
            Save.SaveTurnOfPlayer1 = GameLogic.TurnOfPlayer1;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveFile, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, Save);
            stream.Close();
        }

        public static void LoadSomething()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveFile, FileMode.Open, FileAccess.Read);
            SaveData Load = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            InitializeCards.GetTagDataList = Load.SavecardTagData;
            InitializeGame.Player1 = Load.SavePlayer1;
            InitializeGame.Player2 = Load.SavePlayer2;
            GameLogic.ScoreOfPlayer1 = Load.SaveScoreOfPlayer1;
            GameLogic.ScoreOfPlayer2 = Load.SaveScoreOfPlayer2;
            InitializeGame.GridColumn = Load.SaveGridColumn;
            InitializeGame.GridRows = Load.SaveGridRows;
            GameLogic.TurnOfPlayer1 = Load.SaveTurnOfPlayer1;
        } 
    }
}

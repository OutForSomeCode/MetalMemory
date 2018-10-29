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
            SaveData obj = new SaveData();

            obj.SavecardTagData = InitializeCards.GetTagDataList;
            obj.SavePlayer1 = InitializeGame.Player1;
            obj.SavePlayer2 = InitializeGame.Player2;
            obj.SaveScoreOfPlayer1 = GameLogic.ScoreOfPlayer1;
            obj.SaveScoreOfPlayer2 = GameLogic.ScoreOfPlayer2;
            obj.SaveGridColumn = InitializeGame.GridColumn;
            obj.SaveGridRows = InitializeGame.GridRows;
            obj.SaveTurnOfPlayer1 = GameLogic.TurnOfPlayer1;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveFile, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void LoadSomething()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveFile, FileMode.Open, FileAccess.Read);
            SaveData objnew = (SaveData)formatter.Deserialize(stream);
            stream.Close();

            InitializeCards.GetTagDataList = objnew.SavecardTagData;
            InitializeGame.Player1 = objnew.SavePlayer1;
            InitializeGame.Player2 = objnew.SavePlayer2;
            GameLogic.ScoreOfPlayer1 = objnew.SaveScoreOfPlayer1;
            GameLogic.ScoreOfPlayer2 = objnew.SaveScoreOfPlayer2;
            InitializeGame.GridColumn = objnew.SaveGridColumn;
            InitializeGame.GridRows = objnew.SaveGridRows;
            GameLogic.TurnOfPlayer1 = objnew.SaveTurnOfPlayer1;
        } 
    }
}

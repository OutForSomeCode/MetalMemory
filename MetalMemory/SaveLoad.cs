﻿using System;
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
    /// <summary>
    /// slaat data op in memory.sav en laad deze weer terug
    /// </summary>
    class SaveLoad
    {
        // naam van de save file
        private static string SaveFile = "SaveGames/memory.sav";
        private static string SaveHighscore = "SaveGames/highscore.sav";

        /// <summary>
        /// data class met variabelen waar de data in opgeslagen word
        /// </summary>
        [Serializable]  // geeft aan dat deze class omgezet kan worden in een stroom van data
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

        [Serializable]
        public class SaveHighscoreData
        {
            public Dictionary<string, int> SaveHighscoreDictionary;
        }

        /// <summary>
        /// opslaan door de data in binary(10100110) in een file te streamen
        /// </summary>
        public static void SaveSomething()
        {
            // maakt een nieuwe class aan met de indeling van "SaveData" en vult deze met de data die wij opslaan willen
            SaveData Save = new SaveData();

            // data die wij opslaan
            Save.SavecardTagData = InitializeCards.GetTagDataList;
            Save.SavePlayer1 = InitializeGame.Player1;
            Save.SavePlayer2 = InitializeGame.Player2;
            Save.SaveScoreOfPlayer1 = GameLogic.ScoreOfPlayer1;
            Save.SaveScoreOfPlayer2 = GameLogic.ScoreOfPlayer2;
            Save.SaveGridColumn = InitializeGame.GridColumn;
            Save.SaveGridRows = InitializeGame.GridRows;
            Save.SaveTurnOfPlayer1 = GameLogic.TurnOfPlayer1;

            // pakt alle data hierboven en verandert deze in 1 & 0 en slaat deze op in memory.sav
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveFile, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, Save);
            stream.Close();
        }

        public static void SaveHighscores()
        {
            SaveHighscoreData Save = new SaveHighscoreData();

            Save.SaveHighscoreDictionary = HighScore.highscores;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SaveHighscore, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, Save);
            stream.Close();
        }

        /// <summary>
        /// laad de data uit de memory.sav file
        /// </summary>
        public static void LoadSomething()
        {
            if (File.Exists(SaveFile))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(SaveFile, FileMode.Open, FileAccess.Read);

                // maakt een nieuwe class aan met de indeling van "SaveData" en vult deze met de data uit onze save file "memory.sav"
                SaveData Load = (SaveData)formatter.Deserialize(stream);
                stream.Close();

                // geeft aan waar de geladen data heen moet(haalt hij uit de load class, hierboven aangemaakt)
                InitializeCards.GetTagDataList = Load.SavecardTagData;
                InitializeGame.Player1 = Load.SavePlayer1;
                InitializeGame.Player2 = Load.SavePlayer2;
                GameLogic.ScoreOfPlayer1 = Load.SaveScoreOfPlayer1;
                GameLogic.ScoreOfPlayer2 = Load.SaveScoreOfPlayer2;
                InitializeGame.GridColumn = Load.SaveGridColumn;
                InitializeGame.GridRows = Load.SaveGridRows;
                GameLogic.TurnOfPlayer1 = Load.SaveTurnOfPlayer1;
            }
            else
            {
                return;
            }
        }

        public static void LoadHighscores()
        {
            if (File.Exists(SaveHighscore))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(SaveHighscore, FileMode.Open, FileAccess.Read);

                // maakt een nieuwe class aan met de indeling van "SaveData" en vult deze met de data uit onze save file "memory.sav"
                SaveHighscoreData Load = (SaveHighscoreData)formatter.Deserialize(stream);
                stream.Close();

                HighScore.highscores = Load.SaveHighscoreDictionary;
            }
            else
            {
                return;
            }    
        }
    }
}

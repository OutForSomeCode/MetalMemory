using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MetalMemory
{
    /// <summary>
    /// creeert een lijst met kaarten(alleen de data, word in gamelogic met een Tag aan een button gekoppelt)
    /// </summary>
    class InitializeCards
    {
        /// <summary>
        /// class die de opbouw van de info voor de Tag(word gekoppelt aan een button) bevat
        /// </summary>
        [Serializable] // geeft aan dat deze class omgezet kan worden in een stroom van data
        public class CardTagData
        {
            public int IndexNumber;
            public string SourceCardFace;
            public string SourceCardBack;
            public bool CardHidden;

            public CardTagData(int CardNumber, string CardFace, string CardBack, bool HideCard)
            {
                IndexNumber = CardNumber;
                SourceCardFace = CardFace;
                SourceCardBack = CardBack;
                CardHidden = HideCard;
            }
        }

        public static List<CardTagData> GetTagDataList = new List<CardTagData>();       // lijst waar of nieuwe Tag info in word gezet of tag info uit een save file

        private Random RandomNumberGenerator = new Random();     
        private int TotalCards;
        private int UniqueCards;

        private List<int> IntList = new List<int>(Enumerable.Range(1, 32));             // maakt een lijst met de nummers 1 t/m 32
        private List<CardTagData> ButtonTag = new List<CardTagData>();                  // maakt een lege afbeeldingen lijst genaamd Images

        /// <summary>
        /// berekent aantal totaal en unieke kaarten, start de method(FillImages) en shud de kaarten door elkaar
        /// </summary>
        /// <param name="column">aantal kolommen</param>
        /// <param name="row">aantal rijen</param>
        public InitializeCards(int column, int row)     // geeft de grid naam, aantal kolommen & rijen mee
        {
            TotalCards = column * row;                  // Berekend het aantal kaarten
            UniqueCards = TotalCards / 2;               // geeft aan hoeveel unieke kaarten er zijn
            FillButtonsList();                          // start de method(FillImages)
            GetTagDataList = ButtonTag.OrderBy(y => RandomNumberGenerator.Next()).ToList();  // Randomized de volgorde van de lijst met afbeeldingen en zet deze in GetImageList
        }

        /// <summary>
        /// vult een lijst met met informatie(kaartnummer, voorkant, achterkant, verberg kaart?) die later aan een button gekoppelt word. de hoeveelheid is aangegeven door de spelers(16, 36, 64 kaarten)
        /// </summary>
        private void FillButtonsList()
        { 
            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards);   // Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList)   // voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                string CardFace = "Images/Cards/Card" + CardNumber + ".png";        // locatie en naam van de afbeeldingen voorkant kaarten
                string CardBack = "Images/Cards/CardBack.png";                      // naam van de afbeeldingen achterkan kaarten 
                bool HideCard = false;                                              // om aan te geven of een kaart van het bord verdwijnen moet

                for (int i = 0; i < 2; i++)
                {
                    var CardTag = new CardTagData(CardNumber, CardFace, CardBack, HideCard);
                    ButtonTag.Add(CardTag);
                }
            }
        }
    }
}


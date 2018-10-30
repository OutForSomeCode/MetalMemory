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
    class InitializeCards
    {
        [Serializable]
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

        public static List<CardTagData> GetTagDataList = new List<CardTagData>();

        private Random RandomNumberGenerator = new Random();     
        private int TotalCards;                                         //lege variabele
        private int UniqueCards;                                        //lege variabele

        private List<int> IntList = new List<int>(Enumerable.Range(1, 32));             //maakt een lijst met de nummers 1 t/m 32
        private List<CardTagData> ButtonTag = new List<CardTagData>();                  //maakt een lege afbeeldingen lijst genaamd Images

        public InitializeCards(int column, int row)     //geeft de grid naam, aantal kolommen & rijen mee
        {
            TotalCards = column * row;                  //Berekend het aantal kaarten
            UniqueCards = TotalCards / 2;               //geeft aan hoeveel unieke kaarten er zijn
            FillButtonsList();                          //start de method(FillImages) en geeft
            GetTagDataList = ButtonTag.OrderBy(y => RandomNumberGenerator.Next()).ToList();  //Randomized de volgorde van de lijst met afbeeldingen en zet deze in GetImageList
        }

        private void FillButtonsList()
        { 
            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards);   //Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList)   //voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                string CardFace = "Images/Cards/Card" + CardNumber + ".png";
                string CardBack = "Images/Cards/CardBack.png";
                bool HideCard = false;

                for (int i = 0; i < 2; i++)
                {
                    var CardTag = new CardTagData(CardNumber, CardFace, CardBack, HideCard);
                    ButtonTag.Add(CardTag);
                }
            }
        }
    }
}


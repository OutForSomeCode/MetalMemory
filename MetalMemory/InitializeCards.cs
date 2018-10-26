﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MetalMemory
{
    class InitializeCards
    {
        public class CardTagData
        {
            public int IndexNumber;
            public ImageSource SourceCardFace;
            public ImageSource SourceCardBack;
            public bool FaceUp;

            public CardTagData(int CardNumber, ImageSource CardFace, ImageSource CardBack, bool CardFlip)
            {
                IndexNumber = CardNumber;
                SourceCardFace = CardFace;
                SourceCardBack = CardBack;
                FaceUp = CardFlip;
            }
        }

        public static List<Button> GetCardList = new List<Button>();

        private Random RandomNumberGenerator = new Random();     
        private int TotalCards;                                         //lege variabele
        private int UniqueCards;                                        //lege variabele

        private List<int> IntList = new List<int>(Enumerable.Range(1, 32));             //maakt een lijst met de nummers 1 t/m 32
        private List<Button> Buttons = new List<Button>();                     //maakt een lege afbeeldingen lijst genaamd Images

        public InitializeCards(int column, int row)    //geeft de grid naam, aantal kolommen & rijen mee
        {
            TotalCards = column * row;      //Berekend het aantal kaarten
            UniqueCards = TotalCards / 2;   //geeft aan hoeveel unieke kaarten er zijn
            FillImages();                   //start de method(FillImages) en geeft
            GetCardList = Buttons.OrderBy(y => RandomNumberGenerator.Next()).ToList();  //Randomized de volgorde van de lijst met afbeeldingen en zet deze in GetImageList
        }

        private void FillImages()
        { 
            var RandomIntList = IntList.OrderBy(x => RandomNumberGenerator.Next()).Take(UniqueCards);   //Randomized de volgorde van de lijst met 32 nummers en pakt er (UniqueCards) uit

            foreach (int CardNumber in RandomIntList)   //voor elk nummer(int) uit RandomIntList voert hij de onderstaande code uit (hoe vaak? --> UniqueCards) 
            {
                ImageSource CardFace = new BitmapImage(new Uri("Images/Cards/Card" + CardNumber + ".png", UriKind.Relative));
                ImageSource CardBack = new BitmapImage(new Uri("Images/Cards/CardBack.png", UriKind.Relative));
                bool CardFlip = false;

                for (int i = 0; i < 2; i++)
                {
                    Button Card = new Button();
                    Card.Tag = new CardTagData(CardNumber, CardFace, CardBack, CardFlip);
                    Buttons.Add(Card);
                }
            }
        }
    }
}


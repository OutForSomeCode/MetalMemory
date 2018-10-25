using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetalMemory
{
    class PlaySounds
    {
        private string FileName;
        private string PlayType;

        public PlaySounds(string FileToPlay, string TypeToPlay)
        {
            FileName = FileToPlay;
            PlayType = TypeToPlay;
<<<<<<< HEAD
=======

>>>>>>> 94567cdcb5317d15dc57514d6ae6f2d3e50fdaf8
            Play();
        }

        private void Play()
        {
            string path = Assembly.GetExecutingAssembly().Location;             //kijkt waar de .exe staat
            path = Path.GetDirectoryName(path);                                 //geeft het pad naar de .exe terug als string
            path = Path.Combine(path, "Sounds/" + FileName);                    //combineerd het pad naar de .exe met het pad naar het geluids bestand
<<<<<<< HEAD
            SoundPlayer PlaySounds = new SoundPlayer(path);                     //maakt een soundplayer aan met verwijzing naar het geluids bestand                                                  

            if (PlayType == "PlaySync")
            {
                PlaySounds.PlaySync();                                          //speelt het geluid af, en pauseert de code tot het geluit klaar is met afspelen
            }
            if (PlayType == "PlayLoop")
            {
                PlaySounds.PlayLooping();                                       //speelt het geluid af in een loop
            }
            if (PlayType == "Play")
            {
                PlaySounds.Play();                                              //speelt het geluid af
=======
            SoundPlayer PlaySounds = new SoundPlayer(path);                     //maakt een soundplayer aan met verwijzing naar het geluids bestand


            if (PlayType == "PlaySync")                                         //speelt het geluid af, en pauseert de code tot het geluit klaar is met afspelen
            {
                PlaySounds.PlaySync();
            }                                                                   

            if (PlayType == "PlayLoop")                                          //speelt het geluid af in een loop
            {
                PlaySounds.PlayLooping();
            }

            if (PlayType == "Play")                                             //speelt het geluid af
            {
                PlaySounds.Play();
>>>>>>> 94567cdcb5317d15dc57514d6ae6f2d3e50fdaf8
            }
        }
    }
}

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

        public PlaySounds(string FileToPlay)
        {
            FileName = FileToPlay;
            Play();
        }

        private void Play()
        {
            string path = Assembly.GetExecutingAssembly().Location;             //kijkt waar de .exe staat
            path = Path.GetDirectoryName(path);                                 //geeft het pad naar de .exe terug als string
            path = Path.Combine(path, "Sounds/" + FileName);                    //combineerd het pad naar de .exe met het pad naar het geluids bestand
            SoundPlayer PlaySounds = new SoundPlayer(path);                     //maakt een soundplayer aan met verwijzing naar het geluids bestand
            PlaySounds.Play();                                                  //speelt het geluid af, en pauseert de code tot het geluit klaar is met afspelen
        }
    }
}

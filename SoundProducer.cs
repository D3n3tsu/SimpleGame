using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Windows.Media;



namespace TarasGame2
{

    /*internal enum Sounds
    {
        ZombiesRoaming =0 ,
    }*/

    class SoundProducer:IDisposable
    {
        //MediaPlayer soundZaZombie;
        //Dictionary<Sounds, Uri> sounds;
        bool useSoundPlayer;
        SoundPlayer zombiePlayer;
        SoundPlayer tekilaPlayer;
        SoundPlayer themePlayer;

        public SoundProducer()
        {
            //sounds = new Dictionary<Sounds, Uri>();
            //LocateSounds();
            useSoundPlayer = true;
            InitSounds(); 
        }
        public void PlayTekila()
        {
            tekilaPlayer.Play();
        }
        public void PlayTheme()
        {
            themePlayer.PlayLooping();
        }
        public void PlayZombies()
        {
            if (useSoundPlayer)
            {
                zombiePlayer.PlayLooping();
            }
            else {
                //soundZaZombie.Volume = 1.0f;

                //soundZaZombie.Play();
            }
        }

        /* void LocateSounds()
         {
             sounds.Add(Sounds.ZombiesRoaming, null);


             Sounds[] tempArr = sounds.Keys.ToArray();
             for (int i = 0; i < sounds.Count && !useSoundPlayer; i++)
             {

                 string temp = GetFileName();
                 string tempUristr = new Uri(temp).AbsoluteUri;
                 Uri tempUri = new Uri(tempUristr);
                 if (Uri.IsWellFormedUriString(tempUristr, UriKind.Absolute))
                 {
                     try
                     {
                         using (Stream str = File.OpenWrite(temp))
                         {
                             switch (tempArr[i])
                             {
                                 case 0: Properties.Resources.Zombie_sound_effects___zombie_group_roaming.CopyTo(str); break;
                             }

                         }

                         sounds[tempArr[i]] = tempUri;
                     }
                     catch
                     {
                         useSoundPlayer = true;
                     }
                 }
             }
             foreach (var item in sounds)
             {
                 if (item.Value == null) useSoundPlayer = true;
             }
    }*/
        void InitSounds()
        {


            /*soundZaZombie = new MediaPlayer();
            
            soundZaZombie.Open(sounds[Sounds.ZombiesRoaming]);
            soundZaZombie.Play();
            Uri t = soundZaZombie.Source;
            while (soundZaZombie.IsBuffering) ;*/
            if (useSoundPlayer) { zombiePlayer = new SoundPlayer(Properties.Resources.Zombie_sound_effects___zombie_group_roaming);
                themePlayer = new SoundPlayer(Properties.Resources.Undertale_OST___Temmie_Village_Extended);
                tekilaPlayer = new SoundPlayer(Properties.Resources.Dog_shaking_off_water_Sound_effect);
                    }
        }

        static string GetFileName()
        {
            string directory = Path.GetTempPath();
            string file = Guid.NewGuid() + ".wav";
            return Path.Combine(directory, file);
        }

        public void Dispose()
        {
            /*soundZaZombie.Close();
            if (sounds.Count > 0)
            {
                foreach (var item in sounds)
                {
                    File.Delete(item.Value.AbsolutePath);
                }
            }*/
            if (zombiePlayer != null) zombiePlayer.Dispose();
        }
    }
}

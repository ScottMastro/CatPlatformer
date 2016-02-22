using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace CatPlatformer
{
    class Sounds
    {
        public SoundEffect meow0;
        public SoundEffect meow1;
        public SoundEffect meow2;
        public SoundEffect meow3;
        public SoundEffect bell;
        public SoundEffect scratch0;
        public SoundEffect scratch1;
        public SoundEffect scratch2;
        public SoundEffect step0;
        public SoundEffect step1;
        public Song music;

        //constructor
        public Sounds()
        {
        }

        //loads sounds
        public void LoadSounds(ContentManager content)
        {
            meow0 = content.Load<SoundEffect>("sounds\\meow0");
            meow1 = content.Load<SoundEffect>("sounds\\meow1");
            meow2 = content.Load<SoundEffect>("sounds\\meow2");
            meow3 = content.Load<SoundEffect>("sounds\\meow3");
            bell = content.Load<SoundEffect>("sounds\\bell");
            scratch0 = content.Load<SoundEffect>("sounds\\scratch0");
            scratch1 = content.Load<SoundEffect>("sounds\\scratch1");
            scratch2 = content.Load<SoundEffect>("sounds\\scratch2");
            step0 = content.Load<SoundEffect>("sounds\\step0");
            step1 = content.Load<SoundEffect>("sounds\\step1");
            music = content.Load<Song>("sounds\\music");
        }
    }
}

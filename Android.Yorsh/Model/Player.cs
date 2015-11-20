using System;
using Android.Graphics;

namespace Android.Yorsh.Model
{
    public class Player
    {
        public Player(string name, Bitmap photo, bool isPlay = false, int score = 0)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name of Player");
            if (photo==null) throw new ArgumentNullException("Photo of Player");
            Name = name;
            Photo = photo;
            IsPlay = isPlay;
            Score = score;
            
        }
        public Bitmap Photo { get; private set; }
        public string Name { get; private set; }

        public bool IsPlay { get; set; }

        public int Score { get; set; }
    }
}

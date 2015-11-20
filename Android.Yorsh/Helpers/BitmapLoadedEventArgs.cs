using System;

namespace Android.Yorsh.Helpers
{
    public class BitmapLoadedEventArgs : EventArgs
    {
        private readonly string _path;

        public BitmapLoadedEventArgs(string path)
        {
            _path = path;
        }

        public string Path
        {
            get { return _path; }
        }
    }
}
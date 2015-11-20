using System;
using System.Net.Mime;
using Android.App;

namespace Android.Yorsh.Helpers
{
    internal class MakeTask : TaskDialogAbstract
    {
        private readonly int _taskScore;
        private readonly string _playerName;
        private readonly int _playerScore;

        public MakeTask(int taskScore, string playerName, int playerScore)
        {
            _taskScore = taskScore;
            _playerName = playerName;
            _playerScore = playerScore;
        }

		public override int ResourceArray 
		{
			get 
			{
				return Resource.Array.MakeThis;
			}
		}

        public override string GetChangedScore()
        {
            return string.Format(base.GetChangedScore(),_taskScore);
        }

        public override string GetEndDesc()
        {
            return base.GetEndDesc() + _playerName;
        }

        public override string GetCurentScore()
        {
            return base.GetCurentScore() + _playerScore;
        }
    }
}
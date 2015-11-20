using System;

namespace Android.Yorsh.Helpers
{
	internal class BearTask : TaskDialogAbstract
    {
		private readonly string _playerName;
        private readonly int _playerScore;

        public BearTask(string playerName, int playerScore)
        {
            _playerName = playerName;
            _playerScore = playerScore;
        }

		public override int ResourceArray 
		{
			get 
			{
				return Resource.Array.Bear;
			}
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
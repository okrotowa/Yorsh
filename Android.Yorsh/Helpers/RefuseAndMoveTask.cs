namespace Android.Yorsh.Helpers
{
    internal class RefuseAndMoveTask : TaskDialogAbstract
    {
        private readonly string _playerName;
        private readonly int _playerScore;

        public RefuseAndMoveTask(string playerName, int playerScore)
        {
            _playerName = playerName;
            _playerScore = playerScore;
        }

		public override int ResourceArray 
		{
			get 
			{
				return Resource.Array.RefuseAndMove;
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
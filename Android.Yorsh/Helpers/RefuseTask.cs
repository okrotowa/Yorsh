using System;

namespace Android.Yorsh.Helpers
{
    internal class RefuseTask : TaskDialogAbstract
    {
        private readonly int _taskScore;

        public RefuseTask(int taskScore)
        {
            _taskScore = taskScore;
        }

		public override int ResourceArray 
		{
			get 
			{
				return Resource.Array.Refuse;
			}
		}

        public override string GetCurentScore()
        {
            return base.GetCurentScore() + _taskScore;
        }
    }
}
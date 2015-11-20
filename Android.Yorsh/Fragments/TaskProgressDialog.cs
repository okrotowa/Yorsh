using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Android.Graphics.Drawables;

namespace Android.Yorsh.Fragments
{
    public class TaskProgressDialog : DialogFragment
    {
        private readonly TaskDialog _taskDialog;
        private readonly Player _currentPlayer;
        private readonly int _taskScore;


        public TaskProgressDialog(TaskDialog taskDialog, Player currentPlayer, int taskScore)
        {
            _taskDialog = taskDialog;
            _currentPlayer = currentPlayer;
            _taskScore = taskScore;

        }

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			var dialog =  base.OnCreateDialog (savedInstanceState);
			dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			dialog.Window.SetFlags(WindowManagerFlags.ForceNotFullscreen, WindowManagerFlags.ForceNotFullscreen); 
			dialog.Window.SetBackgroundDrawableResource (Android.Resource.Color.Transparent);
			return dialog;
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			//this.Dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			//

            var view = inflater.Inflate(Resource.Layout.TaskStatus, container, false);
            var taskStrings = TaskDialogBuilder.GetTask(_taskDialog, _taskScore, _currentPlayer);
			var color = Resources.GetColor (_taskDialog == TaskDialog.Make 
				? Resource.Color.task_progress_green 
				: Resource.Color.task_progress_red);

            var statusTitleText = view.FindViewById<TextView>(Resource.Id.statusTitleText);
			statusTitleText.SetTypeface (Activity.MyriadProFont (MyriadPro.SemiboldCondensed), Android.Graphics.TypefaceStyle.Normal);
			statusTitleText.SetTextColor (color);
			statusTitleText.Text = taskStrings.GetStatusTitle();

            var statusDescriptionText = view.FindViewById<TextView>(Resource.Id.statusDescriptionText);
			statusDescriptionText.SetTypeface (Activity.MyriadProFont (MyriadPro.Condensed), Android.Graphics.TypefaceStyle.Normal);
			statusDescriptionText.SetTextColor (color);
			statusDescriptionText.Text = taskStrings.GetStartDesc();

			var boldConsededFont = Activity.MyriadProFont (MyriadPro.BoldCondensed);
            var changeCountScoreText = view.FindViewById<TextView>(Resource.Id.changeCountScoreText);
			changeCountScoreText.SetTypeface (boldConsededFont, Android.Graphics.TypefaceStyle.Normal);
            changeCountScoreText.Text = taskStrings.GetChangedScore();

            var endDescriptionText = view.FindViewById<TextView>(Resource.Id.endDescriptionText);
			endDescriptionText.SetTypeface (Activity.MyriadProFont (MyriadPro.LightCondensed), Android.Graphics.TypefaceStyle.Normal);
            endDescriptionText.Text = taskStrings.GetEndDesc();

            var currentScoreText = view.FindViewById<TextView>(Resource.Id.currentScoreText);
			currentScoreText.SetTypeface (boldConsededFont, Android.Graphics.TypefaceStyle.Normal);
            currentScoreText.Text = taskStrings.GetCurentScore();

			var continueButton = view.FindViewById<ImageButton>(Resource.Id.continueButton);

            continueButton.Click += (sender, args) => Dismiss();
			return view;
        }

		private void ShowBonusDialog()
		{
			var fragmentTransaction = FragmentManager.BeginTransaction();
			var prev = (DialogFragment)FragmentManager.FindFragmentByTag("bonus");
			var dialog = prev ?? new BonusDialog();
			dialog.Show(fragmentTransaction, "bonus");
		}

		public override void Dismiss ()
		{			
			if (TaskDialog.Make == _taskDialog) {
				ShowBonusDialog ();
			}
			base.Dismiss();
		}



    }
}
using Android.App;
using Android.OS;
using Android.Yorsh.Activities;
using Android.Widget;
using Android.Content;
using Android.Yorsh.Helpers;
using Android.Graphics;

namespace Android.Yorsh
{
	[Activity (Label = "@string/PickPlayerString", ParentActivity= typeof(GameActivity))]			
	public class ChoosePlayerActivity : BaseActivity
	{
		TextView _readyButton;
		PlayerListAdapter _adapter;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ChoosePlayer);
			SetHomeButtonEnabled (false);
			var list = this.FindViewById<ListView>(Resource.Id.playerList);
			_adapter = new PlayerListAdapter(this);

			list.Adapter = _adapter;
			_readyButton = (TextView)CreateActionButton(Resource.Drawable.table_button);
			SetTextButtonEnabled (false);
			_readyButton.SetText (Resource.String.ConfirmString);
			_readyButton.SetTextColor (Resources.GetColorStateList(Resource.Drawable.ready_label_enable));
			_readyButton.Click+= delegate {
				SetResult(Result.Ok, new Intent().PutExtra("player_id",_adapter.SelectedPosition));
				base.OnBackPressed();
			};

		}
		//After ReadyButton Initialization
		public void SetTextButtonEnabled(bool enabled)
		{
			_readyButton.Enabled = enabled;
		}


		public override void OnBackPressed ()
		{
			var toast = Toast.MakeText (this, _adapter.SelectedView == null 
					? Resource.String.PleaseSelectThePlayerString
					: Resource.String.PressReadyButtonString, 
				ToastLength.Short);
			toast.Show ();
		}
	}
}


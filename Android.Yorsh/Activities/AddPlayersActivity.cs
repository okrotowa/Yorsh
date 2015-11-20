using System;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Content;
using Android.Graphics;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/PlayersString",ParentActivity = (typeof(MainMenuActivity)), ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddPlayersActivity : BaseActivity
    {
		Button _startGameButton;
        AddNewPlayerListAdapter _adapter;
        bool _isPlayersCountValidate;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddPlayers);
			CreateActionButton (Resource.Drawable.add_player_button).Click+=(sender,e)=>
				StartActivity(new Intent(this, typeof(AddNewPlayerActivity)));	
			_startGameButton = FindViewById<Button>(Resource.Id.startPlayButton);
			_startGameButton.Touch += (sender, e) => this.OnTouchButtonDarker (_startGameButton, e);
			_startGameButton.SetTypeface (this.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
		}

        protected override void OnResume()
        {
            base.OnResume();
            var listView = FindViewById<ListView>(Resource.Id.playersList);
            _adapter = new AddNewPlayerListAdapter(this);
            listView.Adapter = _adapter;

            _startGameButton.Click += _startGameButton_Click;
			IsPlayersCountValidate = Rep.Instance.Players.Count > 1;
			Rep.Instance.Players.ItemRemoved += delegate {				
				{
						IsPlayersCountValidate = Rep.Instance.Players.Count > 1;
						listView.InvalidateViews();
				}

			};
        }

        private bool IsPlayersCountValidate
        {
            get { return _isPlayersCountValidate; }
            set
            {
                _isPlayersCountValidate = value;

                int buttonNameString;
                if (_isPlayersCountValidate)
                {
                    buttonNameString = Resource.String.StartGameButtonString;
					SetButtonEnabled(Rep.Instance.Players.IsAllPlay);
				}
                else
                {
                    buttonNameString = Resource.String.AddNewPlayerString;
					SetButtonEnabled(true);
                }
				_startGameButton.Text = Resources.GetString(buttonNameString);
            }
        }

        void _startGameButton_Click(object sender, EventArgs e)
        {
            this.StartActivityWithoutBackStack (new Intent (this, !IsPlayersCountValidate ? typeof(AddNewPlayerActivity) : typeof(GameActivity)));
        }

		public void SetButtonEnabled(bool enabled)
		{	
			if (_startGameButton.Enabled == enabled)
				return;
			_startGameButton.Enabled = enabled;
			_startGameButton.SetTextColor(enabled ? Resources.GetColor(Resource.Color.white) : this.GetColorWithOpacity(Resource.Color.white, Resource.Color.button_text_disabled));
			if (enabled)
				_startGameButton.Background.ClearColorFilter ();
			else {
				_startGameButton.Background.SetColorFilter (Resources.GetColor (Resource.Color.button_disabled), PorterDuff.Mode.SrcAtop);
			}
		}
    }
}
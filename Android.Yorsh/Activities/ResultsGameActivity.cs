using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/ResultsString", MainLauncher = true, ParentActivity = typeof(GameActivity),ScreenOrientation = ScreenOrientation.Portrait)]
    public class ResultsGameActivity : BaseActivity
    {
		protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ResultsGame);
			await this.StubInitialize ();
            var isEndGame = Intent.GetBooleanExtra("isEnd", false);
            var listView = FindViewById<ListView>(Resource.Id.playerTournamentListView);
			var adapter = new ListAdapter(this, isEndGame);
			listView.Adapter = adapter;
			if (isEndGame)
				SetButtonsAndActionBarIsEndGame ();
			else
				SetButtonsAndActionBarIsNotEndGame ();
        }

		void SetButtonsAndActionBarIsEndGame()
		{
			var startPlayButton = FindViewById<Button>(Resource.Id.startPlayButton);
			startPlayButton.Visibility = ViewStates.Visible;
			FindViewById<RelativeLayout> (Resource.Id.relativeLayout).Visibility = ViewStates.Gone;
			this.ActionBar.Hide ();
			startPlayButton.Touch+=(sender, e)=>this.OnTouchButtonDarker(startPlayButton, e);
			startPlayButton.SetTypeface (this.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			startPlayButton.Click+= delegate {
				Rep.Instance.Clear();				 
				this.StartActivityWithoutBackStack(new Intent(this,typeof(MainMenuActivity)));
			};
				
		}

		void SetButtonsAndActionBarIsNotEndGame()
		{
			FindViewById<Button> (Resource.Id.startPlayButton).Visibility = ViewStates.Gone;
			FindViewById<RelativeLayout> (Resource.Id.relativeLayout).Visibility = ViewStates.Visible;
			this.ActionBar.Show ();
			var completeGameButton = FindViewById<Button> (Resource.Id.completeGameButton);
			completeGameButton.Touch+=(sender, e)=>this.OnTouchButtonDarker(completeGameButton, e);
			completeGameButton.SetTypeface (this.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			completeGameButton.Click+= delegate {
				Intent.PutExtra("isEnd",true);
				this.Recreate();
			};
			var shareButton = FindViewById<Button> (Resource.Id.shareButton);
			shareButton.Touch+=(sender, e)=>this.OnTouchButtonDarker(shareButton, e);
			shareButton.SetTypeface (this.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
		}

        private class ListAdapter : BaseAdapter<Player>
        {
            private readonly Context _context;
			bool _isEndGame;
            private readonly IList<Player> _players;

            public ListAdapter(Context context, bool isEndGame)
            {
                _context = context;
				_isEndGame = isEndGame;
                _players = Rep.Instance.Players.OrderBy(player => player.Score).ToList();
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView != null) return convertView;
                return SetPlayerView(position);
            }

            private View SetPlayerView(int position)
            {
				var inflater = (LayoutInflater)_context.GetSystemService(LayoutInflaterService);
				var viewLead = inflater.Inflate (Resource.Layout.FirstPlayerItem, null);
				var textLead = viewLead.FindViewById<TextView> (Resource.Id.leadText);
				var textScore = viewLead.FindViewById<TextView> (Resource.Id.scoreText);
				textLead.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
				textScore.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
				var viewPlayerItem = inflater.Inflate (Resource.Layout.PlayerItem, null);
				var textPlayerItemName = viewPlayerItem.FindViewById<TextView> (Resource.Id.playerName);
				var textPlayerItemScore = viewPlayerItem.FindViewById<TextView> (Resource.Id.playerScore);
				textPlayerItemName.SetTypeface (this.Activity.MyriadProFont (MyriadPro.Bold), Android.Graphics.TypefaceStyle.Normal);
				textPlayerItemScore.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
				var isFirst = position == 0;
                var view = inflater.Inflate(isFirst
                    ? Resource.Layout.FirstPlayerItem
                    : Resource.Layout.PlayerItem, null);
                var imageView = view.FindViewById<ImageView>(Resource.Id.playerImage);
                var playerName = view.FindViewById<TextView>(Resource.Id.playerName);
                var playerScore = view.FindViewById<TextView>(Resource.Id.playerScore);
				SetLeadString (isFirst, view);
				imageView.SetImageBitmap(this[position].Photo);
				playerName.Text = this[position].Name;
                playerScore.Text = this[position].Score.ToString();
                return view;
            }

			private void SetLeadString(bool firstItem, View view)
			{
				if (!firstItem)
					return;
				var leadString = view.FindViewById<TextView> (Resource.Id.leadText);
				leadString.Text = _context.Resources.GetString(_isEndGame 
					? Resource.String.WinnerString 
					: Resource.String.LeadString);
			}

            public override int Count
            {
                get { return _players.Count; }
            }

            public override Player this[int position]
            {
                get { return _players[position]; }
            }
        }
    }


}
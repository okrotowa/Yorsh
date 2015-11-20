using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Android.Content.PM;
namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/ResultsString",MainLauncher = true, ParentActivity = typeof(GameActivity),ScreenOrientation = ScreenOrientation.Portrait)]
    public class ResultsGameActivity : BaseActivity
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ResultsGame);
			await ActivityExtensions.StubInitialize (this);
            var isEndGame = Intent.GetBooleanExtra("isEnd", false);
            var listView = FindViewById<ListView>(Resource.Id.playerTournamentListView);
			var adapter = new ListAdapter(this, isEndGame);
			listView.Adapter = adapter;
			var view = this.LayoutInflater.Inflate(Resource.Layout.FirstPlayerItem, null);
			listView.AddHeaderView (view);
            SetButtonsAndActionBar(isEndGame);
        }

        private void SetButtonsAndActionBar(bool isEndGame)
        {
                      
            var completeGameButton = FindViewById<Button>(Resource.Id.completeGameButton);
			completeGameButton.Text = GetString(isEndGame 
				? Resource.String.NewGameString 
				: Resource.String.CompleteGameString);
			if (isEndGame) Window.RequestFeature(WindowFeatures.ActionBar);
			//ActionBar.Hide();


            completeGameButton.Click += (sender, args) =>
            {
                Rep.Instance.Clear();
                this.StartActivityWithoutBackStack(new Intent(this, typeof(MainMenuActivity)));
            };


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
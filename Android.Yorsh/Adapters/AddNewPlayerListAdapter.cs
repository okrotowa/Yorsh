using System;
using Android.Widget;
using Android.Yorsh.Model;
using Android.App;
using Android.Views;
using Android.Content;
using Android.Yorsh.Activities;
using Android.Yorsh.Helpers;

namespace Android.Yorsh
{
	public class AddNewPlayerListAdapter : BaseAdapter<Player>
	{
		private readonly AddPlayersActivity _context;
		private readonly PlayerList _players;

		public AddNewPlayerListAdapter(Activity context)
		{
			_context = (AddPlayersActivity)context;
			_players = Rep.Instance.Players;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			if (convertView != null) return convertView;

			var player = _players[position];
			var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);

			convertView = inflater.Inflate(Resource.Layout.AddPlayerItem, null);
			convertView.FindViewById<ImageView>(Resource.Id.playerImage).SetImageBitmap(player.Photo);

			var playerName = convertView.FindViewById<TextView> (Resource.Id.playerName);
			playerName.Text = player.Name;
			playerName.SetTypeface (_context.MyriadProFont (MyriadPro.Bold), Android.Graphics.TypefaceStyle.Normal);

			var enableTextView = convertView.FindViewById<TextView>(Resource.Id.isPlayText);
			enableTextView.SetTypeface(_context.MyriadProFont (MyriadPro.Regular), Android.Graphics.TypefaceStyle.Normal);
			var removeButton = convertView.FindViewById<ImageButton>(Resource.Id.removeButton);
			var doneImage = convertView.FindViewById<ImageView>(Resource.Id.doneImage);

			enableTextView.Enabled = player.IsPlay;

			if (player.IsPlay)
			{
				removeButton.Visibility = ViewStates.Gone;
				doneImage.Visibility = ViewStates.Visible;
				enableTextView.Text = _context.Resources.GetString(Resource.String.IsPlayString);
			}
			else
			{
				removeButton.Visibility = ViewStates.Visible;
				doneImage.Visibility = ViewStates.Gone;
				enableTextView.Text = _context.Resources.GetString(Resource.String.IsNotPlayString);
			}

			removeButton.Click += (sender, e) => _players.RemoveAt (position);
			//Rep.Instance.Players.RemoveAt(position);

			convertView.FindViewById<RelativeLayout>(Resource.Id.playerNameLayout).Click += (sender, e) =>
			{
				var isEnabledNew = !enableTextView.Enabled;
				enableTextView.Enabled = isEnabledNew;
				Rep.Instance.Players[position].IsPlay = isEnabledNew;

				if (isEnabledNew)
				{
					removeButton.Visibility = ViewStates.Gone;
					doneImage.Visibility = ViewStates.Visible;
					enableTextView.Text = _context.Resources.GetString(Resource.String.IsPlayString);
				}
				else
				{
					removeButton.Visibility = ViewStates.Visible;
					doneImage.Visibility = ViewStates.Gone;
					enableTextView.Text = _context.Resources.GetString(Resource.String.IsNotPlayString);
				}

				_context.SetButtonEnabled(_players.Count <= 1 || Rep.Instance.Players.IsAllPlay);
			};

			return convertView;
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


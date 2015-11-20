using System;
using Android.Widget;
using Android.Yorsh.Model;
using Android.App;
using Android.Views;
using Android.Yorsh.Helpers;

namespace Android.Yorsh
{
	public class PlayerListAdapter : BaseAdapter<Player>
	{
		ChoosePlayerActivity _context;
		readonly PlayerList _players;

		public PlayerListAdapter (ChoosePlayerActivity context)
		{
			_context = context;
			_players = Rep.Instance.Players;
		}

		public override long GetItemId (int position)
		{
			return position;
		}
		private View _selectedView;
		public View SelectedView {
			get { return _selectedView;}
			set {
				if (_selectedView == value)
					return;
				if (_selectedView!=null)
					_selectedView.FindViewById<ImageView> (Resource.Id.choosePlayer).Visibility = ViewStates.Gone;
				_selectedView = value;
				_selectedView.FindViewById<ImageView> (Resource.Id.choosePlayer).Visibility = ViewStates.Visible;
				_context.SetTextButtonEnabled (true);
			}
		}

		public int SelectedPosition {
			get;
			private set;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			if (convertView != null) return convertView;
			var player = _players [position];
			convertView = _context.LayoutInflater.Inflate (Resource.Layout.ChoosePlayerItem, null);
			var playerImage = convertView.FindViewById<ImageView> (Resource.Id.playerImage);
			playerImage.SetImageBitmap (player.Photo);
			var doneImage = convertView.FindViewById<ImageView> (Resource.Id.choosePlayer);
			doneImage.Visibility = convertView.Selected ? ViewStates.Visible : ViewStates.Gone;
			var playerName = convertView.FindViewById<TextView> (Resource.Id.playerName);
			playerName.Text = player.Name;
			playerName.SetTypeface (_context.MyriadProFont (MyriadPro.Bold), Android.Graphics.TypefaceStyle.Normal);
			convertView.Click += (sender, e) => {
				SelectedView = convertView;
				SelectedPosition = position;
			};
			return convertView;
		}
	
		public override int Count 
		{
			get 
			{
				return _players.Count;
			}
		}

		public override Player this [int index] 
		{
			get 
			{
				return _players [index];
			}
		}
	}
}


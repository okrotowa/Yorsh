using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Model;

namespace Android.Yorsh.Fragments
{
    public class GameFragment : Fragment
    {
        private readonly Player _player;
        public GameFragment(Player player)
        {
            _player = player;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PlayerInGameItem, null);
            view.FindViewById<ImageView>(Resource.Id.playerInGameImage).SetImageBitmap(_player.Photo);
            view.FindViewById<TextView>(Resource.Id.playerInGameName).Text = _player.Name;
            view.FindViewById<TextView>(Resource.Id.playerInGamePostion).Text =
                Resources.GetString(Resource.String.PositionPlayerString) + Rep.Instance.Players.GetPosition(_player);
            view.FindViewById<TextView>(Resource.Id.playerInGameScore).Text = _player.Score + "\n"
                + Resources.GetString(Resource.String.ScoreString);
            return view;
        }
    }
}

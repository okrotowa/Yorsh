using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Activities;
using Android.Yorsh.Model;

namespace Android.Yorsh.Fragments
{
    public class PickPlayerDialog : DialogFragment
    {
        public PickPlayerDialog()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PickPlayerDialog, container, false);
            var listView = view.FindViewById<ListView>(Resource.Id.playerPickList);
            listView.Adapter = new ListAdapter(this);
            return view;
        }


        private class ListAdapter : BaseAdapter<Player>
        {
            private readonly DialogFragment _context;
            private readonly PlayerList _players;

            public ListAdapter(DialogFragment context)
            {
                _context = context;
                _players = Rep.Instance.Players;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView != null) return convertView;
                var inflater = (LayoutInflater) _context.Activity.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.PlayerItem, null);
                var imageView = convertView.FindViewById<ImageView>(Resource.Id.playerImage);
                var playerName = convertView.FindViewById<TextView>(Resource.Id.playerName);
                imageView.SetImageBitmap(this[position].Photo);
                playerName.Text = this[position].Name;
                convertView.Click += (sender, args) => View_Click(position); 
                return convertView;
            }

            private void View_Click(int position)
            {
			   Rep.Instance.Players[position].Score -= 5;
				((GameActivity)_context.Activity).ShowDialogOnButtonClick(TaskDialog.Bear, this[position]);
               _context.Dismiss();
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

public enum TaskDialog
{
    Make,
    Refuse,
    RefuseAndMove,
    Bear
}
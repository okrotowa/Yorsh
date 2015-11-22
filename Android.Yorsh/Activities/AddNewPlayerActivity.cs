using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views.InputMethods;
using Android.Widget;
using Android.Yorsh.Fragments;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Xamarin.Contacts;
using Android.Content.PM;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/AddNewPlayerLowCaseString",ParentActivity = typeof(AddPlayersActivity), ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddNewPlayerActivity : BaseActivity
    {
        Bitmap _playerImage;
		Button _confirmButton;
		Button _chooseFromContactsButton;
		Button _cancelButton;
		EditText _editText;
		ImageButton _playerImageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddNewPlayer);
			Initialize ();	

			//Make photo or choose from gallery
			_playerImageButton.Click += ChooseNewPhoto;

            //Choose from contacts realization
			_chooseFromContactsButton.Click += delegate
            {
                var contactPickerIntent = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
                StartActivityForResult(contactPickerIntent, 101);
            };

			//For interaction
			_confirmButton.Click += (sender, e) => {
				Rep.Instance.Players.Add (FindViewById<EditText> (Resource.Id.playerName).Text, PlayerImage);
				this.StartActivityWithoutBackStack (ParentActivityIntent);
			};

			_editText.TextChanged += (obj, e) =>
            {
                var text = e.Text.ToString();
				SetConfirmButtonEnabled(!string.IsNullOrEmpty(text));
            };

			var inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
			inputMethodManager.ShowSoftInput(_editText, ShowFlags.Implicit);

            //Cancel
			_cancelButton.Click += (sender, e) => this.StartActivityWithoutBackStack (ParentActivityIntent);
        }

		private void Initialize()
		{
			_chooseFromContactsButton = FindViewById<Button>(Resource.Id.chooseFromContButton);
			_confirmButton = FindViewById<Button>(Resource.Id.confirmButton);
			_cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
			_editText = FindViewById<EditText> (Resource.Id.playerName);
			_playerImageButton = FindViewById<ImageButton> (Resource.Id.playerImage);

			SetFont (_cancelButton);
			SetFont (_confirmButton);
			SetFont (_chooseFromContactsButton);
			SetFont (_editText);

			_confirmButton.Touch += (sender, e) => this.OnTouchButtonDarker(_confirmButton, e);
			_cancelButton.Touch += (sender, e) => this.OnTouchButtonDarker(_cancelButton, e);
			_chooseFromContactsButton.Touch += (sender, e) => this.OnTouchButtonDarker(_chooseFromContactsButton, e);
			_confirmButton.Enabled = true;	SetConfirmButtonEnabled(false);
			//To set _playImage to default
			PlayerImage = null;
		}


        private Bitmap PlayerImage
        {
            get { return _playerImage; }
            set
            {
                if (value == null)
                {
					_playerImageButton
                        .SetImageDrawable(Resources.GetDrawable(Resource.Drawable.photo_default));
                    _playerImage = BitmapFactory.DecodeResource(Resources, Resource.Drawable.photo_default);
                }
                else
                {
					var image =  value.GetRoundedCornerBitmap((int)Resources.GetDimension (Resource.Dimension.RoundedCorners));
					_playerImageButton.SetImageBitmap(image);
					_playerImage = image;
                }
            }
        }


		private void ChooseNewPhoto(object sender, EventArgs e)
		{
			var fragmentTransaction = FragmentManager.BeginTransaction();
			var prev = FragmentManager.FindFragmentByTag("dialog");

			ChoosePhotoDialog dialog;
			if (prev == null)
			{
				var button = FindViewById<ImageButton>(Resource.Id.playerImage);
				var desireWidth = button.Width;
				var desireHeight = button.Height;
				dialog = new ChoosePhotoDialog();
				dialog.Show(fragmentTransaction, "dialog");
				dialog.BitmapLoaded += (object obj, BitmapLoadedEventArgs args) => 
					BitmapExtensions.DecodeBitmapAsync (args.Path, desireWidth, desireHeight)
					.ContinueWith (t => PlayerImage = t.Result, TaskScheduler.FromCurrentSynchronizationContext ());
			}
			else
			{
				dialog = (ChoosePhotoDialog)prev;
				dialog.Show(fragmentTransaction, "dialog");
			}
		}

        //Get Photo and Name from Contact
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode != 101 || resultCode != Result.Ok) return;
            if (data == null || data.Data == null) return;

            var addressBook = new AddressBook(Application.Context)
            {
                PreferContactAggregation = false
            };

            var contact = addressBook.Load(data.Data.LastPathSegment);
            if (string.IsNullOrEmpty(contact.DisplayName))
            {
                Toast.MakeText(this, Resources.GetString(Resource.String.NoNameString), ToastLength.Short).Show();
                return;
            }

			_editText.Text = contact.DisplayName;
			PlayerImage = contact.GetThumbnail();
        }

		private void SetConfirmButtonEnabled(bool enabled)
		{
			if (_confirmButton.Enabled == enabled) return;
			_confirmButton.Enabled = enabled;
			_confirmButton.SetTextColor(enabled ? Resources.GetColor(Resource.Color.white) : this.GetColorWithOpacity(Resource.Color.white, Resource.Color.button_text_disabled));
			if (enabled) _confirmButton.Background.ClearColorFilter ();
			else _confirmButton.Background.SetColorFilter (Resources.GetColor (Resource.Color.button_disabled), PorterDuff.Mode.SrcAtop);
		}

		private void SetFont(TextView textView)
		{
			textView.SetTypeface(this.MyriadProFont(MyriadPro.BoldCondensed), TypefaceStyle.Normal);
		}

		protected override void OnStop ()
		{
			_playerImageButton.Click -= ChooseNewPhoto;
			base.OnStop ();
		}
    }
}
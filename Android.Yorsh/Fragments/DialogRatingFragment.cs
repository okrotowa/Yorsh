using System;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Android.Graphics.Drawables;

namespace Android.Yorsh
{		
	public class DialogRatingFragment : DialogFragment
	{
	    private ISharedPreferencesEditor _editor;
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);
            dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            dialog.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen); 
            dialog.Window.SetBackgroundDrawableResource(Resource.Color.white);
            return dialog;
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.DialogRating, null);
			Button buttonEasy = view.FindViewById<Button>(Resource.Id.buttonEasy);
			Button buttonYester = view.FindViewById<Button>(Resource.Id.buttonYester);
			Button buttonNo = view.FindViewById<Button>(Resource.Id.buttonNo);

            var preferences = Activity.GetPreferences(FileCreationMode.Private);
            _editor = preferences.Edit();   
     
			buttonEasy.Click += (object sender, EventArgs e) => {
				var  url = Android.Net.Uri.Parse("https://itunes.apple.com/ua/app/ers/id604886527?mt=8");
                var intent = new Intent(Intent.ActionView, url);
                StartActivity(intent);
			    _editor.PutBoolean("isShow", false);
                this.Dismiss();
			};

			buttonYester.Click += (object sender, EventArgs e) => {
                _editor.PutBoolean("isShow", true);
                this.Dismiss();
			};

			buttonNo.Click += (object sender, EventArgs e) => {
                _editor.PutBoolean("isShow", false);
                this.Dismiss();
			};
            return view;
        }

        public override void OnCancel(IDialogInterface dialog)
        {
            _editor.PutBoolean("isShow", true);
            base.OnCancel(dialog);
        }

        public override void Dismiss()
        {
            //Activity.Recreate();
            _editor.Commit();
            base.Dismiss();
        }
	}
}


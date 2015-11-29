using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Android.Yorsh.Fragments
{
    public class ShareFragment : DialogFragment
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);
            dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            dialog.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            dialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
            return dialog;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			var view = inflater.Inflate(Resource.Layout.ShareSoc, null);
			ImageButton btnFb = view.FindViewById<ImageButton>(Resource.Id.buttonFb);
			ImageButton btnVk = view.FindViewById<ImageButton>(Resource.Id.buttonVk);
			ImageButton btnTw = view.FindViewById<ImageButton>(Resource.Id.buttonTw);

			btnFb.Click += (object sender, EventArgs e) => {
				var  url = Android.Net.Uri.Parse("https://facebook.com");
				var intent = new Intent(Intent.ActionView, url);
				StartActivity(intent);
				this.Dismiss();
			};

			btnVk.Click += (object sender, EventArgs e) => {
				var  url = Android.Net.Uri.Parse("https://vk.com");
				var intent = new Intent(Intent.ActionView, url);
				StartActivity(intent);
				this.Dismiss();
			};

			btnTw.Click += (object sender, EventArgs e) => {
				var  url = Android.Net.Uri.Parse("https://twitter.com");
				var intent = new Intent(Intent.ActionView, url);
				StartActivity(intent);
				this.Dismiss();
			};
			return view;
        }

		public override void OnCancel(IDialogInterface dialog)
		{
			base.OnCancel(dialog);
		}

		public override void Dismiss()
		{
			base.Dismiss();
		}
    }
}
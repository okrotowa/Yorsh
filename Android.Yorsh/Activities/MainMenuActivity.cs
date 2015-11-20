using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Content.PM;
using Android.Text;
using System;
using Android.Graphics;

namespace Android.Yorsh.Activities
{
	[Activity(Theme = "@android:style/Theme.NoTitleBar", MainLauncher=false,  ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainMenuActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
			SetContentView(Resource.Layout.MainMenu);
			SetLinkView ();
			var myriadProBoldCons = this.MyriadProFont(MyriadPro.BoldCondensed);
			SetButton (Resource.Id.StartGame, typeof(AddPlayersActivity), myriadProBoldCons);
			SetButton (Resource.Id.Rules, typeof(RulesActivity), myriadProBoldCons);
			SetButton (Resource.Id.PlusCards, typeof(StoreActivity), myriadProBoldCons);
			base.OnCreate(bundle);
        }

		private void SetButton(int resorceId, Type activityOnClick, Typeface font)
		{
			var startGameButton = FindViewById<Button>(resorceId);
			startGameButton.Touch += (sender, e) => this.OnTouchButtonDarker(startGameButton, e);
			startGameButton.Click += (sender, e) => StartActivity (new Intent (this, activityOnClick));
			startGameButton.SetTypeface (font, TypefaceStyle.Normal);
		}


		private void SetLinkView()
		{
			var boardGameButton = FindViewById<TextView>(Resource.Id.BoardGame);
			boardGameButton.SetTypeface (this.MyriadProFont (MyriadPro.BoldCondensed),
				TypefaceStyle.Normal);
			var webLinkText = @"<a href='http://www.spb.mosigra.ru/Face/Show/ersh'>"
				+GetString(Resource.String.BoardGameString)
				+"</a>";
			var textFormated = Html.FromHtml(webLinkText);
			boardGameButton.TextFormatted =  textFormated;//your html goes in responseText
			boardGameButton.Clickable = true;
			boardGameButton.Click += delegate {
				GetAlertDialog().Show ();
			};
		}


		private AlertDialog.Builder GetAlertDialog ()
		{
			var builder = new AlertDialog.Builder (this);
			builder.SetView (LayoutInflater.Inflate (Resource.Layout.Rules, null));
			builder.SetMessage (Resource.String.OpenSiteQuestionString);
			builder.SetTitle (Resource.String.GoToMosigraSite);
			builder.SetPositiveButton (GetString (Resource.String.YesString), delegate {
				var uri = Android.Net.Uri.Parse ("http://www.spb.mosigra.ru/Face/Show/ersh");
				var intent = new Intent (Intent.ActionView, uri);
				StartActivity (intent);
			});
			builder.SetNegativeButton (GetString (Resource.String.NoString), delegate {
			});
			return builder;
		}

    }
}


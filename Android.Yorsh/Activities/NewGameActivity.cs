
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Webkit;

namespace Android.Yorsh
{
	[Activity (Label = "NewGameActivity")]			
	public class NewGameActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		protected void OnCreate ()
		{

			Button buttonNewGame = FindViewById<Button>(Resource.Id.completeGameButton);
			Button buttonEasy = FindViewById<Button>(Resource.Id.buttonEasy);
			Button buttonYester = FindViewById<Button>(Resource.Id.buttonYester);
			Button buttonNo = FindViewById<Button>(Resource.Id.buttonNo);

			buttonNewGame.Click += (object sender, EventArgs e) => {
				SetContentView(Resource.Layout.DialogRating);
			};

			buttonEasy.Click += (object sender, EventArgs e) => {
				SetContentView(Resource.Layout.WebView);

				string url = "https://itunes.apple.com/ua/app/ers/id604886527?mt=8";
				var web = FindViewById<WebView>(Resource.Id.webView1);

				web.Settings.JavaScriptEnabled = true;
				web.LoadUrl(url);
			};

			buttonYester.Click += (object sender, EventArgs e) => {
				//hz sho
			};

			buttonNo.Click += (object sender, EventArgs e) => {
				SetContentView(Resource.Layout.ResultsGame);
			};
		}
	}
}


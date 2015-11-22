using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Yorsh.Helpers;

namespace Android.Yorsh.Activities
{
	[Activity(Theme = "@android:style/Theme.NoTitleBar",MainLauncher = true, NoHistory = true,ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : Activity
    {
		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.Splash);
			await this.CreateDataBaseAsync (50, 50);
			StartActivity(typeof(MainMenuActivity));
		}
    }    
}
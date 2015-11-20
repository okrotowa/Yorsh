using Android.App;
using Android.OS;
using Android.Content.PM;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/BuyString", ParentActivity = typeof(MainMenuActivity),ScreenOrientation = ScreenOrientation.Portrait)]
    public class StoreActivity : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Store);
        }
    }
}
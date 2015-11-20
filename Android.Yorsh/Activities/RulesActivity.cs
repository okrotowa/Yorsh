using Android.App;
using Android.OS;
using Android.Content.PM;
namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/RulesGameString", ParentActivity = typeof(MainMenuActivity),ScreenOrientation = ScreenOrientation.Portrait)]
    public class RulesActivity : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Rules);

    }
	}}
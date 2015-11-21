using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.App;
using Android.Content.PM;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/RulesGameString",MainLauncher = true, ParentActivity = typeof(MainMenuActivity),ScreenOrientation = ScreenOrientation.Portrait)]
	public class RulesActivity : BaseActivity
	{

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Rules);

			var adapter =
				new GenericFragmentPagerAdapter(SupportFragmentManager,
					new RuleFragment(Rules.ShortAboutGame)
					, new RuleFragment(Rules.HowToPlay)
					,new RuleFragment(Rules.Bear));
			var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewPager);
			viewPager.Adapter = adapter;
		}
	}
}
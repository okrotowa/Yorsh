using Android.App;
using Android.Graphics;
using Android.Widget;
using System;
using Android.Yorsh.Model;
using Android.Yorsh.Helpers;
using System.Linq;
using Android.Views;
using Android.OS;

namespace Android.Yorsh
{	
	public class BonusDialog : DialogFragment
	{
		public override void OnResume ()
		{
			base.OnResume ();
			var count = new Random ().Next (Rep.Instance.Bonuses.Count () - 1);
			View.FindViewById<TextView> (Resource.Id.bonusText).Text = Rep.Instance.Bonuses [count].BonusName;
		}

		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			var dialog =  base.OnCreateDialog (savedInstanceState);
			dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			dialog.Window.SetFlags(WindowManagerFlags.ForceNotFullscreen, WindowManagerFlags.ForceNotFullscreen); 
			//dialog.Window.SetBackgroundDrawableResource (Android.Resource.Color.Transparent);
			return dialog;
		}

//		public override void OnCreate (Android.OS.Bundle savedInstanceState)
//		{
//			base.OnCreate (savedInstanceState);
//			this.SetStyle (DialogFragmentStyle.NoTitle, Resource.Style.Theme_WithoutActionBar);
//		}

		public override Android.Views.View OnCreateView (Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.Bonus, null);
			view.Background.SetColorFilter (Activity.Resources.GetColor (Resource.Color.bonus_bg),PorterDuff.Mode.SrcAtop);
			view.FindViewById<TextView> (Resource.Id.congrats).SetTypeface (Activity.MyriadProFont (MyriadPro.SemiboldCondensed), TypefaceStyle.Normal);
			view.FindViewById<TextView> (Resource.Id.youHaveBonus).SetTypeface (Activity.MyriadProFont (MyriadPro.SemiboldCondensed), TypefaceStyle.Normal);	
			view.FindViewById<TextView> (Resource.Id.bonusText).SetTypeface (Activity.MyriadProFont (MyriadPro.Condensed), TypefaceStyle.Normal);
			view.FindViewById<ImageButton> (Resource.Id.continueButton).Click += (sender, e) => Dismiss();		
			return view;
		}
	
		}

	}




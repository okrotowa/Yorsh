using System;

using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;

namespace Android.Yorsh
{
	public class RuleFragment : Android.Support.V4.App.Fragment
	{
		private Rules _rule;
		public RuleFragment(Rules rule)
		{
			_rule = rule;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.FragmentOne, null);
			var imageBackgroundRules
			= view.FindViewById<ImageView> (Resource.Id.imageBackgroundRules);
			//imageBackgroundRules
			var arr = Resources.GetStringArray (Resources.GetIdentifier(GetRuleName(_rule),"string-array", Activity.PackageName));
			var imageId = Resources.GetIdentifier ("rules_" + arr [0] + "_page", "drawable", Activity.PackageName);
			imageBackgroundRules.SetImageDrawable(Resources.GetDrawable(imageId));
			var imageScrollId = Resources.GetIdentifier ("rules_" + arr [0] +"_sroll_page", "drawable", Activity.PackageName);
			var imageScroll = view.FindViewById<ImageView> (Resource.Id.imageScroll);
			imageScroll.SetImageDrawable (Resources.GetDrawable (imageScrollId));
			var textHeader = view.FindViewById<TextView> (Resource.Id.textHeader);
			textHeader.Text = arr [1];
			textHeader.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			var textContainer = view.FindViewById<TextView> (Resource.Id.textContainer);
			textContainer.Text = arr [2];
			textContainer.SetTypeface (this.Activity.MyriadProFont (MyriadPro.Condensed), Android.Graphics.TypefaceStyle.Normal);
			return view;
		}

		private string GetRuleName(Rules rule){
			switch (rule) {
			case Rules.ShortAboutGame:
				return "ShortAboutGame";
			case Rules.HowToPlay:
				return "HowToPlay";
			case Rules.Bear:
				return "AndSuddenly";
			default: throw new NotImplementedException();
			}
		}

	}

	public class FragmentOne : Android.Support.V4.App.Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.FragmentOne, null);
			var arr = Resources.GetStringArray (Resource.Array.OnePageRules);
			var textHeader = view.FindViewById<TextView> (Resource.Id.textHeader);
			textHeader.Text = arr [0];
			textHeader.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			var textContainer = view.FindViewById<TextView> (Resource.Id.textContainer);
			textContainer.Text = arr [1];
			textContainer.SetTypeface (this.Activity.MyriadProFont (MyriadPro.Condensed), Android.Graphics.TypefaceStyle.Normal);
			return view;
		}
	}

	public class FragmentTwo : Android.Support.V4.App.Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.FragmentTwo, null);
			var arr = Resources.GetStringArray (Resource.Array.TwoPageRules);
			var textHeader = view.FindViewById<TextView> (Resource.Id.textHeader);
			textHeader.Text = arr [0];
			textHeader.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			var textContainer = view.FindViewById<TextView> (Resource.Id.textContainer);
			textContainer.Text = arr [1];
			textContainer.SetTypeface (this.Activity.MyriadProFont (MyriadPro.Condensed), Android.Graphics.TypefaceStyle.Normal);
			return view;
		}
	}

	public class FragmentThree : Android.Support.V4.App.Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate (Resource.Layout.FragmentThree, null);
			var arr = Resources.GetStringArray (Resource.Array.ThreePageRules);
			var textHeader = view.FindViewById<TextView> (Resource.Id.textHeader);
			textHeader.Text = arr [0];
			textHeader.SetTypeface (this.Activity.MyriadProFont (MyriadPro.BoldCondensed), Android.Graphics.TypefaceStyle.Normal);
			var textContainer = view.FindViewById<TextView> (Resource.Id.textContainer);
			textContainer.Text = arr [1];
			textContainer.SetTypeface (this.Activity.MyriadProFont (MyriadPro.Condensed), Android.Graphics.TypefaceStyle.Normal);
			return view;
		}
	}
}
public enum  Rules
{
	ShortAboutGame,
	HowToPlay,
	Bear
}


using System;
using Android.Support.V4.App;
using Android.Widget;
using Android.App;

namespace Android.Yorsh
{
	public class GenericFragmentPagerAdapter : FragmentPagerAdapter
	{
		private readonly Android.Support.V4.App.Fragment[] _fragments;

		public GenericFragmentPagerAdapter(
			Android.Support.V4.App.FragmentManager fm,
			params Android.Support.V4.App.Fragment[] fragments) : base(fm)
		{
			_fragments = fragments;
			
		}

		public override int Count
		{
			get { return _fragments.Length; }
		}

		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
			return _fragments[position];
		}
	}
}
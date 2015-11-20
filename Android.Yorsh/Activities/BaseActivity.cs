using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace Android.Yorsh.Activities
{
	[Activity]
    public abstract class BaseActivity : Activity
    {
		View _actionButton = null;

        protected override void OnCreate(Bundle bundle)
        {
			base.OnCreate (bundle);
			var viewGroup = (ViewGroup)LayoutInflater.Inflate (Resource.Layout.YorshActionBar,null);
			var param = new ActionBar.LayoutParams (
				ViewGroup.LayoutParams.MatchParent, 
				ViewGroup.LayoutParams.MatchParent);
			var title = viewGroup.FindViewById<TextView> (Resource.Id.titleText);
			title.Text = Title;
			title.SetTypeface (this.MyriadProFont(MyriadPro.Bold),TypefaceStyle.Normal);
			ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));
			ActionBar.SetDisplayShowTitleEnabled(false);
			ActionBar.SetDisplayShowCustomEnabled(true);
			ActionBar.SetCustomView(viewGroup, param);
			SetHomeButtonEnabled (true);
		}

		protected void SetHomeButtonEnabled(bool enabled)
		{
			ActionBar.SetDisplayShowHomeEnabled(enabled);
			ActionBar.SetDisplayHomeAsUpEnabled(enabled);
			var title = ActionBar.CustomView.FindViewById<TextView> (Resource.Id.titleText);
			title.SetPadding (0, 0,enabled ? 100 : 0, 0);
		}

		protected View CreateActionButton(int resourceId)
		{
			var layout = ActionBar.CustomView.FindViewById<RelativeLayout> (Resource.Id.customActionButton);
			_actionButton = LayoutInflater.Inflate(resourceId,null);
			var textView = _actionButton as TextView;
			if (textView != null) textView.SetTypeface (this.MyriadProFont (MyriadPro.Condensed), TypefaceStyle.Normal);
			var param = new RelativeLayout.LayoutParams(
				ViewGroup.LayoutParams.WrapContent,
				ViewGroup.LayoutParams.MatchParent);
			param.AddRule (LayoutRules.CenterVertical);
			param.AddRule (LayoutRules.AlignParentRight);
			layout.AddView (_actionButton, param);
			return _actionButton;
		}

		protected View ActionButton
		{
			get { return _actionButton; }
		}
	}

}	
	


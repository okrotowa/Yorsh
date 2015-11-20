using System.Reflection;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;
using Android.Content.PM;

namespace Android.Yorsh.Activities
{
	[Activity(Theme = "@android:style/Theme.NoTitleBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ImageActivity : Activity
    {
        private int _taskId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ImageCard);
            _taskId = Intent.Extras.GetInt("taskId");
            var task = Rep.Instance.Tasks.GetTask(_taskId);
            var category = Rep.Instance.Tasks.GetCategory(task.CategoryId);

            var image = FindViewById<ImageView>(Resource.Id.imageCardView);
            var text = FindViewById<TextView>(Resource.Id.textCard);
			text.SetTypeface(this.BankirRetroFont(), TypefaceStyle.Normal);
            //TODO: Problem with often click
            using (var resourceStream = ResourceLoader.GetEmbeddedResourceStream(
                Assembly.GetAssembly(typeof(ResourceLoader)), category.ImageName))
            {
                image.SetImageBitmap(BitmapFactory.DecodeStream(resourceStream));
            }
            text.Text = task.TaskName;
            FindViewById(Resource.Id.contentFrameLayout).Click += (sender, args) => OnBackPressed();
        }

        public void SetResult()
        {
            var isBear = Rep.Instance.Tasks.GetTask(_taskId).IsBear;
            SetResult(isBear ? Result.Ok : Result.Canceled);
        }

        public override void OnBackPressed()
        {
            SetResult();
            base.OnBackPressed();
            Finish();
        }
    }
}
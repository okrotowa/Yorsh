using System.Reflection;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Android.Yorsh.Model;

namespace Android.Yorsh.Fragments
{
    public class ImageFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view =  inflater.Inflate(Resource.Layout.ImageCard, null, false);
            var taskId = Arguments.GetInt("taskId");
            var task = Rep.Instance.Tasks.GetTask(taskId);
            var category = Rep.Instance.Tasks.GetCategory(task.CategoryId);

            var image = view.FindViewById<ImageView>(Resource.Id.imageCardView);
            var text = view.FindViewById<TextView>(Resource.Id.textCard);

            using (var resourceStream = ResourceLoader.GetEmbeddedResourceStream(
                Assembly.GetAssembly(typeof(ResourceLoader)), category.ImageName))
            {
                image.SetImageBitmap(BitmapFactory.DecodeStream(resourceStream));
            }
            text.Text = task.TaskName;
            //view.FindViewById(Resource.Id.contentFrameLayout).Click += (sender, args) => OnBackPressed();

            return view;
        }
        
        public void Finish()
        {
            var isBear = Rep.Instance.Tasks.Current.IsBear;
            Activity.SetResult(isBear ? Result.Ok : Result.Canceled);
        }

        //public override void OnBackPressed()
        //{
        //    base.OnBackPressed();
        //    Finish();
        //}
    }
}
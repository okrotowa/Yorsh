using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Yorsh.Helpers;
using Xamarin.Media;

namespace Android.Yorsh.Fragments
{
    public class ChoosePhotoDialog : DialogFragment
    {
		
		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			var dialog =  base.OnCreateDialog (savedInstanceState);
			dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			dialog.Window.SetFlags(WindowManagerFlags.ForceNotFullscreen, WindowManagerFlags.ForceNotFullscreen); 
			dialog.Window.SetBackgroundDrawableResource (Android.Resource.Color.Transparent);
			dialog.Window.SetGravity (GravityFlags.CenterHorizontal);
			var param = dialog.Window.Attributes;
			param.Width = ViewGroup.LayoutParams.MatchParent;
			dialog.Window.Attributes = param;
			return dialog;
		}

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {			
            var view = inflater.Inflate(Resource.Layout.ChoosePhoto, container, false);
            view.FindViewById<Button>(Resource.Id.makePhotoButton).Click += MakePhoto_Click;
            view.FindViewById<Button>(Resource.Id.choosePhotoButton).Click += ChoosePhoto_Click;
            view.FindViewById<Button>(Resource.Id.cancelButton).Click += delegate { Dismiss(); };
            return view;

        }

        private void ChoosePhoto_Click(object sender, EventArgs e)
        {
            var picker = new MediaPicker(Activity);
            if (!picker.PhotosSupported)
            {
                ShowUnsupported();
                return;
            }
            var intent = picker.GetPickPhotoUI();
            StartActivityForResult(intent, 2);
        }

        private void MakePhoto_Click(object sender, EventArgs e)
        {
            var picker = new MediaPicker(Activity);
            if (!picker.IsCameraAvailable || !picker.PhotosSupported)
            {
                ShowUnsupported();
                return;
            }
            var store = new StoreCameraMediaOptions()
            {
                Directory = "Yoursh",
                Name = "photo1.jpg"
            };
            var intent = picker.GetTakePhotoUI(store);
            StartActivityForResult(intent, 2);
        }

        private void ShowUnsupported()
        {
            var unsupportedToast = Toast.MakeText(Activity, Resources.GetString(Resource.String.NotSupportCameraString),
                ToastLength.Long);
            unsupportedToast.Show();
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled) { Dismiss(); return; }
            if (requestCode == 2)
            {
                data.GetMediaFileExtraAsync(Activity).ContinueWith(
                    t => OnBitmapLoaded(t.Result.Path),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public event EventHandler<BitmapLoadedEventArgs> BitmapLoaded;

        protected virtual void OnBitmapLoaded(string path)
        {
            var handler = BitmapLoaded;
            if (handler != null) handler(this, new BitmapLoadedEventArgs(path));
            Dismiss();
        }
    }
}
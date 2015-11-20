using Android.App;
using Java.Lang;
using System;
using Android.Runtime;


namespace Android.Yorsh
{
	[Application]
	public class YourshApplication : Application
	{
		public YourshApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
		{

		}

		public override void OnCreate ()
		{
			base.OnCreate ();
			AppDomain currentDomain = AppDomain.CurrentDomain;
//			currentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

//		void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
//		{
//			var allert = new AlertDialog.Builder(this);
//			var exc = (Java.Lang.Exception)e.ExceptionObject;
//			allert.SetTitle(exc+"\\n"+exc.InnerException.Message);
//			allert.Show ();
//		}

	}
}


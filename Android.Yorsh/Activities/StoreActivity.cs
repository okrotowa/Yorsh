using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Widget;
using ClickPizza.Android.Adapters;
using Xamarin.InAppBilling;
using System;

namespace Android.Yorsh.Activities
{
	[Activity(Label = "@string/BuyString", ParentActivity = typeof(MainMenuActivity),ScreenOrientation = ScreenOrientation.Portrait)]
    public class StoreActivity : BaseActivity
    {
		string key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAr1wsRhQz/aFBC2HGRbIJ0Dx372QXWoSYBZT8nESCUl9dxhILH+dSS2UN43ovQsqAfb1WNQ+qvc8aVwNq0BXiVmNecqLYbsL2jZSGS7ox/Y8Xl03wsQfVHoOY6LDORvYN+ZqJxEuS1dqcUul4TiUqbemZKOAqNQeRUmMz/QmCnNiINuaOv3vOQKMyJsYPwqg+9oei/9bYSIPdkXiyHATbEIiMULheQrS0w4fMu8Fk6nqAMmp8RAXX/fCDCIoIkJ42kIpiUY9k8qeqHeZkrTVMi//AbuWK7Hx+4hhf6gIR4U4Mz544aXX93w56OTgnC32Ngqoa8RkzHpQPoS5dvLUMMwIDAQAB";
		InAppBillingServiceConnection _serviceConnection;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Store);
            var taskListView = FindViewById<ListView>(Resource.Id.taskListView);
            //taskListView.Adapter = new MultiItemRowListAdapter(this, new StoreListAdapter(), 3, 1);
			char[] charArray = key.ToCharArray();
			Array.Reverse( charArray );
			var s = new string (charArray);
//			_serviceConnection.OnConnected += () => {
//				};
//			_serviceConnection.BillingHandler.QueryInventoryAsync(
//				new System.Collections.Generic.List<string> {
//					ReservedTestProductIDs.Purchased,
//					ReservedTestProductIDs.Canceled,
//					ReservedTestProductIDs.Refunded,
//					ReservedTestProductIDs.Unavailable
//				}, ItemType.Product);
//			// Attempt to connect to the service
//			_serviceConnection.Connect ();
        }

		private void Unify(string[] invert, int[] position)
		{
			
		}
        private class StoreListAdapter : BaseAdapter<StoreItem>
        {
            public override StoreItem this[int position]
            {
                get { throw new System.NotImplementedException(); }
            }

            
            public override int Count
            {
                get { throw new System.NotImplementedException(); }
            }

            public override long GetItemId(int position)
            {
                throw new System.NotImplementedException();
            }

            public override Views.View GetView(int position, Views.View convertView, Views.ViewGroup parent)
            {
                throw new System.NotImplementedException();
            }
        }

        private class StoreItem
        {
            public StoreItem()
            { 
                
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Yorsh.Model;
using SQLite;
using File = System.IO.File;
using Stream = System.IO.Stream;
using Android.Graphics;
using Android.Widget;
using Android.Views;
using System.Linq;

namespace Android.Yorsh.Helpers
{
    public static class ActivityExtensions
    {
        public static void StartActivityWithoutBackStack(this Activity mainActivity, Intent newActivity,
            int fragment = -1)
        {
            if (fragment > 0) newActivity.PutExtra("fragment", fragment);
            mainActivity.StartActivity(newActivity);
            mainActivity.Finish();
        }

		public static async Task StubInitialize(this Activity activity)
		{
			await activity.CreateDataBaseAsync(10, 10);
			Rep.Instance.Players.Add("Olga", BitmapFactory.DecodeResource(activity.Resources, Resource.Drawable.photo_default), true);
			Rep.Instance.Players.Add("Marina", BitmapFactory.DecodeResource(activity.Resources, Resource.Drawable.photo_default), true);
		}

		public static Typeface MyriadProFont (this Activity activity, MyriadPro myriadPro)
		{
			string name;
			switch (myriadPro) {
			case MyriadPro.BoldCondensed:
				name = "MyriadProBoldCondensed.ttf";
				break;
			case MyriadPro.Condensed:
				name = "MyriadProCondensed.ttf";
				break;
			case MyriadPro.Bold:
				name = "MyriadProBold.ttf";
				break;
			case MyriadPro.Regular:
				name = "MyriadProRegular.ttf";
				break;
			case MyriadPro.SemiboldCondensed:
				name = "MyriadProSemiboldCond.otf";
				break;
			case MyriadPro.LightCondensed:
				name = "MyriadProLightCond.otf";
				break;
			default:
				throw new NotImplementedException (); 
			}
			return Typeface.CreateFromAsset (activity.Assets, name);
		}

		public static Typeface BankirRetroFont(this Activity activity)
		{
			return Typeface.CreateFromAsset (activity.Assets, "BankirRetro.ttf");
		}

        //TODO:remove method
        public static async Task CreateDataBaseAsync(this Activity context, int taskCount, int bonusCount)
        {
            var path = Rep.Instance.DataBaseFile;
            var connection = new SQLiteAsyncConnection(path);
            File.Delete(path);
            if (!File.Exists(path))
            {
                var results = await connection.CreateTablesAsync<TaskTable, BonusTable, CategoryTable>();
                if (results.Results.Count == 3)
                {
                    var tasks = GetTasks(context.Assets.Open("Task.csv"));
                    var bonuses = GetBonus(context.Assets.Open("Bonus.csv"));
                    var category = GetCategory(context.Assets.Open("Category.csv"));

                    await connection.InsertAllAsync(tasks);
                    await connection.InsertAllAsync(bonuses);
                    await connection.InsertAllAsync(category);
                }
            }

            await Rep.Instance.TaskGenerateAsync(taskCount);
            await Rep.Instance.BonusGenerateAsync(bonusCount);
        }

        private static IEnumerable<TaskTable> GetTasks(Stream stream)
        {
            var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            var list = new List<TaskTable>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = line.Split(';');
                list.Add(new TaskTable(int.Parse(values[0]), values[1], int.Parse(values[2])));
            }
            list.Shuffle();
            return list;
        }

        private static IEnumerable<CategoryTable> GetCategory(Stream stream)
        {
            var reader = new StreamReader(stream);
            var list = new List<CategoryTable>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = line.Split(';');
                list.Add(new CategoryTable(int.Parse(values[0]), values[1],values[2]));
            }
            return list;
        }

        private static IEnumerable<BonusTable> GetBonus(Stream stream)
        {
            var reader = new StreamReader(stream);
            var list = new List<BonusTable>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = line.Split(';');
                list.Add(new BonusTable(values[0], int.Parse(values[1])));
            }
            return list;
        }

		public static void OnTouchButtonDarker(this Activity activity, Button sender, View.TouchEventArgs e)
		{
			var button = sender;
			switch (e.Event.Action) 
			{
				case MotionEventActions.Down:
				{
					button.Background.SetColorFilter(activity.Resources.GetColor(Resource.Color.button_shadow_gray), PorterDuff.Mode.SrcAtop);
					button.SetTextColor (GetColorWithOpacity(activity, Resource.Color.white, Resource.Color.button_shadow_gray));
					button.Invalidate();
					e.Handled = false;
					break;
				}
				case MotionEventActions.Up: 
				{
					button.Background.ClearColorFilter();
					button.SetTextColor (activity.Resources.GetColor(Android.Resource.Color.White));
					button.Invalidate();
					e.Handled = false;
					break;
				}
			};
		}

		public static Color GetColorWithOpacity(this Activity activity, int normalColor, int opacityColor)
		{
			var bitmap = Bitmap.CreateBitmap(1, 1, Bitmap.Config.Argb8888); //make a 1-pixel Bitmap
			var canvas = new Canvas(bitmap);
			canvas.DrawColor(activity.Resources.GetColor(normalColor)); //color we want to apply filter to
			canvas.DrawColor(activity.Resources.GetColor(opacityColor), PorterDuff.Mode.SrcAtop); //apply filter
			var index = bitmap.GetPixel(0,  0);
			return Color.Argb(index, index,index,index);
		}

		public static void MakeButtonEnabled(this Activity activity, Button button, bool enabled)
		{
			button.Enabled = enabled;
			button.Background.Alpha = enabled ? 255 : 50;
			button.SetTextColor(activity.Resources.GetColor(enabled 
				? Resource.Color.white 
				: Resource.Color.pressed_text_color));
		}
    }
}

public enum MyriadPro
{
	BoldCondensed,
	Condensed,
	Bold,
	Regular,
	SemiboldCondensed,
	LightCondensed
}
using Android.Content.Res;
using TimeLoggingApp.Dependencies;
using TimeLoggingApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ScreenSizeCalculator))]
namespace TimeLoggingApp.Droid
{
	public class ScreenSizeCalculator : IScreenSizeCalculator
	{
		public ScreenSizeCalculator()
		{
			var activity = MainActivity.Instance;
			
			var metrics = activity.Resources.DisplayMetrics;
			widthInDP = ConvertPixelsToDp(metrics.WidthPixels, activity.Resources);
			heightInDP = ConvertPixelsToDp(metrics.HeightPixels, activity.Resources);
		}

		public float widthInDP { get; }
		public float heightInDP { get; }

		private int ConvertPixelsToDp(float pixelValue, Resources resources)
		{
			var dp = (int)((pixelValue) / resources.DisplayMetrics.Density);
			return dp;
		}
	}
}
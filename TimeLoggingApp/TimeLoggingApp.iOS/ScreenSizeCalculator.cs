using TimeLoggingApp.Dependencies;
using TimeLoggingApp.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ScreenSizeCalculator))]
namespace TimeLoggingApp.iOS
{
	public class ScreenSizeCalculator : IScreenSizeCalculator
	{
		public ScreenSizeCalculator()
		{
			var bounds = UIScreen.MainScreen.Bounds;
			widthInDP = (float)bounds.Width;
			heightInDP = (float)bounds.Height;
		}

		public float widthInDP { get; }
		public float heightInDP { get; }
	}
}
using Xamarin.Forms;

namespace TimeLoggingApp.Utility
{
	public static class ColorUtility
	{
		public static Color GetContrastingColor(Color color)
		{
			if (color != Color.Default)
			{
				// Standard luminance calculation.
				double luminance = 0.30 * color.R +
				                   0.59 * color.G +
				                   0.11 * color.B;
				return luminance > 0.5 ? Color.Black : Color.White;
			}
			else
			{
				return Color.Default;
			}
		}
	}
}
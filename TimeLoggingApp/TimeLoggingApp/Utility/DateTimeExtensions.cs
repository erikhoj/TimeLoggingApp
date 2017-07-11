using System;

namespace TimeLoggingApp.Utility
{
	public static class DateTimeExtensions
	{
		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = dt.DayOfWeek - startOfWeek;
			if (diff < 0)
			{
				diff += 7;
			}
			return dt.AddDays(-1 * diff).Date;
		}

		public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = Math.Abs(dt.DayOfWeek - startOfWeek);

			diff = diff == 0 ? 7 : 0;

			return dt.AddDays(diff).Date;
		}
	}
}
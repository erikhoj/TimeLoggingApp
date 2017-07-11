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
			var invDiff = 7 - diff;

			return dt.AddDays(invDiff).Date;
		}
	}
}
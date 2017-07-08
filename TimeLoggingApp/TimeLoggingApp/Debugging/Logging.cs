namespace TimeLoggingApp.Debugging
{
	public class Logging
	{
		public static ILogger logger = new DebugLogger();
	}
}
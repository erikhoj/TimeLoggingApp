using System.Diagnostics;

namespace TimeLoggingApp.Debugging
{
	public class DebugLogger : ILogger
	{
		private bool shouldLog = true;

		public void WriteMessage(object message)
		{
			if (!shouldLog) return;

			Debug.WriteLine(message);
		}

		public void WriteError(object error)
		{
			if (!shouldLog) return;
			
			Debug.WriteLine(error);
		}
	}
}
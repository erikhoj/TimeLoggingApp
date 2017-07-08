namespace TimeLoggingApp.Debugging
{
	public interface ILogger
	{
		void WriteMessage(object message);
		void WriteError(object error);
	}
}
namespace TimeLoggingApp
{
	public interface IActionProvider
	{
		ActionLog GetActionLog();
		Actions GetActions();
		void WriteState();
	}
}
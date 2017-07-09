using TimeLoggingApp.Debugging;
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public partial class App : Application
	{
		public readonly ActionLog actionLog;
		public readonly Actions actions;

		private IActionProvider _actionProvider;

		public App()
		{
			_actionProvider = new PropertyActionProvider(this);

			actions = _actionProvider.GetActions();
			actionLog = _actionProvider.GetActionLog();
			Logging.logger.WriteMessage(actionLog);
			
			InitializeComponent();

			MainPage = new NavigationPage(new TimeLoggingApp.MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			_actionProvider.WriteState();
		}

		protected override void OnResume()
		{
		}
	}
}

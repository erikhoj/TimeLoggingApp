
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public partial class App : Application
	{
		public readonly ActionLog actionLog;
		public readonly Actions actions;

		public App()
		{
			actions = new Actions();
			actionLog = new ActionLog(actions);
			
			InitializeComponent();

			MainPage = new NavigationPage(new TimeLoggingApp.MainPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

using System;
using TimeLoggingApp.Utility;
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public partial class MainPage : ContentPage
	{
		private App _app;

		private int _buttonStackStartChildren;

		public MainPage()
		{
			_app = (App)Application.Current;

			InitializeComponent();

			StopButton.Clicked += OnStopButtonClicked;
			EditActionsButton.Clicked += (sender, info) => Navigation.PushAsync(new ActionOverviewPage());
			ViewGraphButton.Clicked += (sender, info) => Navigation.PushAsync(new TimeGraphPage());

			_buttonStackStartChildren = ButtonStack.Children.Count;
		}

		protected override void OnAppearing()
		{
			SetupActionButtons();
			UpdateCurrentAction();
		}

		private void OnStopButtonClicked(object sender, EventArgs eventArgs)
		{
			_app.actionLog.StopCurrentAction();

			UpdateCurrentAction();
		}

		private void UpdateCurrentAction()
		{
			if (!_app.actionLog.IsPerformingAction())
			{
				CurrentActionLabel.Text = "None";
				CurrentActionLabel.TextColor = Color.Black;
				StopButton.IsEnabled = false;
			}
			else
			{
				var currentAction = _app.actions.GetAction(_app.actionLog.GetCurrentAction());
				CurrentActionLabel.Text = currentAction.name;
				CurrentActionLabel.TextColor = currentAction.color;
				StopButton.IsEnabled = true;
			}
		}

		private void SetupActionButtons()
		{
			// Remove all but the original children of buttonstack
			while (ButtonStack.Children.Count > _buttonStackStartChildren)
			{
				ButtonStack.Children.RemoveAt(_buttonStackStartChildren);
			}

			var horizontal = CreateHorizontalStack();
			ButtonStack.Children.Add(horizontal);
			
			int i = 0;
			foreach (var action in _app.actions.GetActions())
			{
				var newButton = new Button()
				{
					Text = action.name,
					TextColor = ColorUtility.GetContrastingColor(action.color),
					BackgroundColor = action.color,
					HorizontalOptions = LayoutOptions.FillAndExpand
				};

				newButton.Clicked += (sender, info) => OnActionButtonClicked(action);

				horizontal.Children.Add(newButton);

				i++;
				if (i % 2 == 0)
				{
					horizontal = CreateHorizontalStack();
					ButtonStack.Children.Add(horizontal);
				}
			}
		}

		private void OnActionButtonClicked(Action action)
		{
			_app.actionLog.StartAction(action);

			UpdateCurrentAction();
		}

		private StackLayout CreateHorizontalStack()
		{
			return new StackLayout()
			{
				Orientation = StackOrientation.Horizontal
			};
		}
	}
}


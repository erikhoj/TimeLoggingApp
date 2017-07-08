
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLoggingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActionOverviewPage : ContentPage
	{
		private const string EDIT_BUTTON_ICON = "pencil32.png";
		private const string DELETE_BUTTON_ICON = "minus32.png";
		private const float BUTTON_SIZE = 20;

		private App _app;

		private int _initialContainerChildren;

		public ActionOverviewPage()
		{
			_app = (App) Application.Current;

			InitializeComponent();
			_initialContainerChildren = ActionContainer.Children.Count;
		}

		protected override void OnAppearing()
		{
			SetupActions();
		}

		private void SetupActions()
		{
			while (ActionContainer.Children.Count > _initialContainerChildren)
			{
				ActionContainer.Children.RemoveAt(_initialContainerChildren);
			}

			foreach (var action in _app.actions)
			{
				ActionContainer.Children.Add(CreateActionContainer(action));
			}
		}

		private View CreateActionContainer(Action action)
		{
			var stackLayout = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
			};

			var frame = new Frame()
			{
				Margin = 2,
				Content	= stackLayout
			};

			var nameLabel = new Label()
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = action.name,
				TextColor = action.color
			};

			var editButton = CreateImageButton(
				(_, __) => OnEditButtonTapped(action), 
				EDIT_BUTTON_ICON);

			var deleteButton = CreateImageButton(
				(_, __) => OnDeleteButtonTapped(action, frame),
				DELETE_BUTTON_ICON);

			stackLayout.Children.Add(nameLabel);
			stackLayout.Children.Add(editButton);
			stackLayout.Children.Add(deleteButton);

			return frame;
		}

		private void OnDeleteButtonTapped(Action action, Frame frame)
		{
			ActionContainer.Children.Remove(frame);
			_app.actions.Remove(action);
		}

		private Image CreateImageButton(EventHandler onTapped, string image)
		{
			var button = new Image()
			{
				Source = image,
				HeightRequest = BUTTON_SIZE,
				WidthRequest = BUTTON_SIZE,
				HorizontalOptions = LayoutOptions.End
			};

			var editTapRecognizer = new TapGestureRecognizer();
			editTapRecognizer.Tapped += onTapped;
			button.GestureRecognizers.Add(editTapRecognizer);

			return button;
		}

		private void OnEditButtonTapped(Action action)
		{
			var createPage = new CreateActionPage();
			createPage.EditAction(action);
			Navigation.PushAsync(createPage);
		}
	}
}
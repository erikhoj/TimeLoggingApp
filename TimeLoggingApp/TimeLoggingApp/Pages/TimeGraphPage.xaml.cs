using System.Collections.Generic;
using TimeLoggingApp.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLoggingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeGraphPage : ContentPage
	{
		private const float MINIMUM_PILLAR_SIZE = 50;
		private const float ROW_HEIGHT = 30;

		private App _app;

		public TimeGraphPage ()
		{
			_app = (App) Application.Current;
			InitializeComponent();

			var actions = _app.actions.GetActions();
			for (int row = 0; row < actions.Length; row++)
			{
				var action = actions[row];
				GraphGrid.RowDefinitions.Add(new RowDefinition()
				{
					Height = new GridLength(ROW_HEIGHT)
				});

				var column = 0;
				foreach (var view in GetViews(action))
				{
					GraphGrid.Children.Add(view, column, row);
					column++;
				}
			}
		}

		private IEnumerable<View> GetViews(Action action)
		{
			var nameLabel = new Label()
			{
				VerticalTextAlignment = TextAlignment.Center,
				Text = action.name
			};

			yield return nameLabel;

			var time = "10H 15M";

			var timeLabel = new Label()
			{
				Text = time,
				HorizontalTextAlignment = TextAlignment.End,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				TextColor = ColorUtility.GetContrastingColor(action.color)
			};

			var frame = new Frame()
			{
				Margin = 2,
				Padding = 3,
				Content = timeLabel,
				BackgroundColor = action.color
			};

			yield return frame;
		}
	}
}
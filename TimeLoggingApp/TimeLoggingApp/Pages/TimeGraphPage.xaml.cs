using System;
using System.Collections.Generic;
using TimeLoggingApp.Debugging;
using TimeLoggingApp.Dependencies;
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

		private DateTime intervalStart;
		private DateTime intervalEnd;
		private int totalTimeSpent;

		public TimeGraphPage ()
		{
			_app = (App) Application.Current;
			InitializeComponent();

			intervalStart = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
			intervalEnd = DateTime.Now.EndOfWeek(DayOfWeek.Monday);
			Logging.logger.WriteMessage("Week Start: " + intervalStart + " Week end: " + intervalEnd);
			totalTimeSpent = GetTotalMinutesSpent();

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

		private int GetTotalMinutesSpent()
		{
			var total = 0;
			foreach (var action in _app.actions.GetActions())
			{
				total += _app.actionLog.GetMinutesSpentOnAction(action.id, intervalStart, intervalEnd);
			}

			return total;
		}

		private IEnumerable<View> GetViews(Action action)
		{
			var nameLabel = new Label()
			{
				VerticalTextAlignment = TextAlignment.Center,
				Text = action.name
			};

			yield return nameLabel;

			var time = _app.actionLog.GetMinutesSpentOnAction(action.id, intervalStart, intervalEnd);

			var fraction = 1f;
			if (totalTimeSpent != 0)
			{
				fraction = time / (float)totalTimeSpent;
			}
			
			Logging.logger.WriteMessage("Fraction: " + fraction);

			var screenWidth = DependencyService.Get<IScreenSizeCalculator>().widthInDP;
			var extensibleWidth = screenWidth - 100 - MINIMUM_PILLAR_SIZE;
			var totalSize = MINIMUM_PILLAR_SIZE + extensibleWidth * fraction;

			Logging.logger.WriteMessage("Totalsize: " + totalSize);
			
			var timeLabel = new Label()
			{
				Text = time + "M",
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
				BackgroundColor = action.color,
				WidthRequest = totalSize,
				HorizontalOptions = LayoutOptions.Start
			};

			yield return frame;
		}
	}
}
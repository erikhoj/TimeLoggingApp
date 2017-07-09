
using System;
using System.Collections.Generic;
using System.Reflection;
using TimeLoggingApp.Debugging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLoggingApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColorPickerPage : ContentPage
	{
		public Color pickedColor;
		
		public ColorPickerPage()
		{
			InitializeComponent();

			ConfirmButton.Clicked += (sender, args) => ConfirmColorPick();

			var column = 0;
			var row = -1;
			foreach (var color in GetColors())
			{
				if (column % 4 == 0)
				{
					ColorGrid.RowDefinitions.Add(new RowDefinition()
					{
						Height = new GridLength((double)Resources["colorBoxSize"])
					});

					row++;
					column = 0;
				}

				Logging.logger.WriteMessage("Color: " + color);

				var box = new BoxView()
				{
					Color = color
				};

				var onColorTapped = new TapGestureRecognizer();
				onColorTapped.Tapped += (sender, args) => OnColorBoxClicked(color);
				box.GestureRecognizers.Add(onColorTapped);

				ColorGrid.Children.Add(box, column, row);

				column++;
			}
		}

		private void OnColorBoxClicked(Color color)
		{
			pickedColor = color;
			CurrentColorBox.Color = color;
		}

		protected override void OnAppearing()
		{
			CurrentColorBox.Color = pickedColor;
		}
		
		private void ConfirmColorPick()
		{
			pickedColor = CurrentColorBox.Color;
			Navigation.PopAsync();
		}

		private IEnumerable<Color> GetColors()
		{
			// Loop through the Color structure fields.
			foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
			{
				// Skip the obsolete (i.e. misspelled) cplors.
				if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
					continue;

				if (info.IsPublic &&
					info.IsStatic &&
					info.FieldType == typeof(Color))
				{
					yield return (Color) info.GetValue(null);
				}
			}

			// Loop through the Color structure properties.
			foreach (PropertyInfo info in typeof(Color).GetRuntimeProperties())
			{
				MethodInfo methodInfo = info.GetMethod;

				if (methodInfo.IsPublic &&
					methodInfo.IsStatic &&
					methodInfo.ReturnType == typeof(Color))
				{
					yield return (Color) info.GetValue(null);
				}
			}
		}
	}
}
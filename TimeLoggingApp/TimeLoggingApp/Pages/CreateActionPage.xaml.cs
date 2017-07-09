
using System;
using TimeLoggingApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLoggingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateActionPage : ContentPage
	{
		private Action _actionBeingEdited;

		private App _app;

		private ColorPickerPage _colorPicker;

		public CreateActionPage ()
		{
			_app = (App) Application.Current;
			_colorPicker = new ColorPickerPage();
			_colorPicker.pickedColor = Color.DarkGray;

			InitializeComponent();

			ConfirmButton.Clicked += OnConfirmButtonClicked;

			var onColorTapped = new TapGestureRecognizer();
			onColorTapped.Tapped += (sender, args) => Navigation.PushAsync(_colorPicker);
			ColorBox.GestureRecognizers.Add(onColorTapped);
		}

		protected override void OnAppearing()
		{
			ColorBox.Color = _colorPicker.pickedColor;
		}

		public void EditAction(Action action)
		{
			NameField.Text = action.name;

			_actionBeingEdited = action;
			_colorPicker.pickedColor = action.color;
		}

		protected override void OnDisappearing()
		{
			_actionBeingEdited = null;
		}

		private void OnConfirmButtonClicked(object sender, EventArgs args)
		{
			if (_actionBeingEdited != null)
			{
				_actionBeingEdited.name = NameField.Text;
			}
			else
			{
				_app.actions.Add(NameField.Text, _colorPicker.pickedColor);
			}

			Navigation.PopAsync();
		}
	}
}
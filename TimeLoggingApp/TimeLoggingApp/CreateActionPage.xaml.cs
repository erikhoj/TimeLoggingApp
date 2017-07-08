
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeLoggingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateActionPage : ContentPage
	{
		private Action _actionBeingEdited;

		private App _app;

		public CreateActionPage ()
		{
			_app = (App) Application.Current;

			InitializeComponent();

			ConfirmButton.Clicked += OnConfirmButtonClicked;
		}

		public void EditAction(Action action)
		{
			NameField.Text = action.name;

			_actionBeingEdited = action;
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
				_app.actions.Add(NameField.Text, Color.Green);
			}

			Navigation.PopAsync();
		}
	}
}
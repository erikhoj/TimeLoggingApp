using BitBreak.Utility;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public class Action
	{
		public int id { get; }

		[JsonProperty] private Color _color = Color.Default;
		public Color color
		{
			get { return _color; }
			set
			{
				_color = value;
				actionEdited.SafeInvoke();
			}
		}

		[JsonProperty] private string _name;

		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				actionEdited.SafeInvoke();
			}
		}

		public event System.Action actionEdited;

		public Action(int id, string name)
		{
			this.id = id;
			_name = name;
		}
	}
}
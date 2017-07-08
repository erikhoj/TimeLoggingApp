using Xamarin.Forms;

namespace TimeLoggingApp
{
	public class Action
	{
		public int id { get; }
		public Color color = Color.Default;
		public string name;

		public Action(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
	}
}
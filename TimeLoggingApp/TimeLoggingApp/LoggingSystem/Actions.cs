using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public class Actions : IEnumerable<Action>
	{
		private List<Action> _actions = new List<Action>();

		public Actions()
		{
			AddAction("Work", Color.Blue);
			AddAction("Play", Color.Teal);
		}

		public Action GetAction(int actionId)
		{
			return _actions.Find(a => a.id == actionId);
		}

		public void AddAction(string actionName, Color actionColor)
		{
			var newAction = new Action(GetNextActionId(), actionName);
			newAction.color = actionColor;
			_actions.Add(newAction);
		}

		private int GetNextActionId()
		{
			if (_actions.Count == 0) return 1;

			var maxId = _actions.Select(a => a.id).Max();
			return maxId + 1;
		}

		public IEnumerator<Action> GetEnumerator()
		{
			return _actions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
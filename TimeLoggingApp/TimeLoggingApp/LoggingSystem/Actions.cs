using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BitBreak.Utility;
using TimeLoggingApp.Debugging;
using Xamarin.Forms;

namespace TimeLoggingApp
{
	public class Actions
	{
		[JsonProperty]
		private List<Action> _actions = new List<Action>();

		public event Action<Action> actionDeleted;
		public event Action<Action> actionCreated;
		public event System.Action actionEdited;
	
		public Action GetAction(int actionId)
		{
			return _actions.Find(a => a.id == actionId);
		}

		public Action[] GetActions()
		{
			return _actions.ToArray();
		}

		public void Add(string actionName, Color actionColor)
		{
			var newAction = new Action(GetNextActionId(), actionName);
			newAction.color = actionColor;
			_actions.Add(newAction);
			newAction.actionEdited += OnActionEdited;
			actionCreated.SafeInvoke(newAction);
		}

		public void Remove(Action action)
		{
			_actions.Remove(action);
			action.actionEdited -= OnActionEdited;
			actionDeleted.SafeInvoke(action);
		}

		private int GetNextActionId()
		{
			if (_actions.Count == 0) return 1;

			var maxId = _actions.Select(a => a.id).Max();
			return maxId + 1;
		}

		[OnDeserialized]
		private void AttachListeners(StreamingContext context)
		{
			foreach (var action in _actions)
			{
				action.actionEdited += OnActionEdited;
			}
		}

		private void OnActionEdited()
		{
			actionEdited.SafeInvoke();
		}
	}
}
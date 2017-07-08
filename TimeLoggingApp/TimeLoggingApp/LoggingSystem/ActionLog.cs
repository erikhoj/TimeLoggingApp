using System;
using System.Collections.Generic;

namespace TimeLoggingApp
{
	public class ActionLog
	{
		public const int STOP_ACTION_ID = 0;

		private readonly Actions _actions;

		private readonly Stack<ActionTime> _log = new Stack<ActionTime>();

		public ActionLog(Actions actions)
		{
			_actions = actions;
		}

		public void StartAction(Action action)
		{
			StopCurrentAction();

			var actionTime = new ActionTime(DateTime.Now, action.id);
			_log.Push(actionTime);
		}

		public void StopCurrentAction()
		{
			if (IsPerformingAction())
			{
				_log.Push(new ActionTime(DateTime.Now, STOP_ACTION_ID));
			}
		}

		public bool IsPerformingAction()
		{
			if (_log.Count == 0) return false;

			if (_log.Peek().actionId == STOP_ACTION_ID)
			{
				return false;
			}

			return true;
		}

		public Action GetCurrentAction()
		{
			if (!IsPerformingAction()) return null;

			return _actions.GetAction(_log.Peek().actionId);
		}

		private class ActionTime
		{
			public ActionTime(DateTime time, int actionId)
			{
				this.time = time;
				this.actionId = actionId;
			}

			public DateTime time { get; private set; }
			public int actionId { get; private set; }
		}
	}
}
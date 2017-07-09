using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeLoggingApp.Debugging;

namespace TimeLoggingApp
{
	public class ActionLog
	{
		public const int STOP_ACTION_ID = 0;
		
		[JsonProperty]
		private readonly List<ActionTime> _log = new List<ActionTime>();
		
		public void StartAction(Action action)
		{
			StopCurrentAction();

			var actionTime = new ActionTime(DateTime.Now, action.id);
			_log.Add(actionTime);
		}

		public void StopCurrentAction()
		{
			if (IsPerformingAction())
			{
				_log.Add(new ActionTime(DateTime.Now, STOP_ACTION_ID));
			}
		}

		public bool IsPerformingAction()
		{
			if (_log.Count == 0) return false;

			if (_log.Last().actionId == STOP_ACTION_ID)
			{
				return false;
			}

			return true;
		}

		public int GetCurrentAction()
		{
			if (!IsPerformingAction()) return 0;

			return _log.Last().actionId;
		}

		public void OnActionRemoved(Action action)
		{
			_log.RemoveAll(a => a.actionId == action.id);
		}

		public int GetMinutesSpentOnAction(int actionId, DateTime startTime, DateTime endTime)
		{
			var timeSpent = 0;
			for (int i = 0; i < _log.Count; i++)
			{
				var actionTime = _log[i];

				if (actionTime.actionId != actionId) continue;
				if (!(DateTime.Compare(actionTime.time, startTime) >= 0)) continue;
				if (!(DateTime.Compare(actionTime.time, endTime) <= 0)) continue;

				var actionEnd = i < _log.Count - 1 ? _log[i + 1].time : DateTime.Now;
				actionEnd = DateTime.Compare(actionEnd, endTime) <= 0 ? actionEnd : endTime;

				Logging.logger.WriteMessage("ActionStart: " + actionTime.time + " ActionEnd: " + actionEnd);

				timeSpent += (int)(actionEnd - actionTime.time).TotalMinutes;
			}

			return timeSpent;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendLine("[ActionLog, total events: " + _log.Count);
			var logArray = _log.ToArray();
			foreach (var actionTime in logArray)
			{
				builder.AppendLine(String.Format("ID: {0} | START: {1}", actionTime.actionId, actionTime.time));
			}

			builder.Append("]");
			return builder.ToString();
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
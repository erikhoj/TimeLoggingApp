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
		private readonly Stack<ActionTime> _log = new Stack<ActionTime>();
		
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

		public int GetCurrentAction()
		{
			if (!IsPerformingAction()) return 0;

			return _log.Peek().actionId;
		}

		public int GetMinutesSpentOnAction(int actionId, DateTime startTime, DateTime endTime)
		{
			var logArray = _log.Reverse().ToArray();

			var timeSpent = 0;
			for (int i = 0; i < logArray.Length; i++)
			{
				var actionTime = logArray[i];

				if (actionTime.actionId != actionId) continue;
				if (!(DateTime.Compare(actionTime.time, startTime) >= 0)) continue;
				if (!(DateTime.Compare(actionTime.time, endTime) <= 0)) continue;

				var actionEnd = i < logArray.Length - 1 ? logArray[i + 1].time : DateTime.Now;
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
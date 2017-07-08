using BitBreak.Utility;
using Newtonsoft.Json;
using TimeLoggingApp.Debugging;

namespace TimeLoggingApp
{
	public class PropertyActionProvider : IActionProvider
	{
		private const string ACTION_LOG_KEY = "ActionLog";
		private const string ACTIONS_KEY = "Actions";

		private readonly ActionLog _actionLog;
		private readonly Actions _actions;

		private readonly App _app;

		public PropertyActionProvider(App app)
		{
			_app = app;

			_actions = LoadFromProperties<Actions>(ACTIONS_KEY);
			_actionLog = LoadFromProperties<ActionLog>(ACTION_LOG_KEY);
		}

		private T LoadFromProperties<T>(string key) where T : new()
		{
			object json;
			if (_app.Properties.TryGetValue(key, out json))
			{
				var jsonString = (string) json;
				
				Logging.logger.WriteMessage("Read value for key " + key + ": " + jsonString);
				var result = JsonConvert.DeserializeObject<T>(jsonString);
				return result;
			}

			return new T();
		}

		public void WriteState()
		{
			WriteObject(ACTION_LOG_KEY, _actionLog);
			WriteObject(ACTIONS_KEY, _actions);
		}

		private void WriteObject(string key, object value)
		{
			var json = JsonConvert.SerializeObject(value);

			Logging.logger.WriteMessage("Writing value for key " + key + ": " + json);

			_app.Properties.AddOrReplace(key, json);
		}
		
		public ActionLog GetActionLog()
		{
			return _actionLog;
		}

		public Actions GetActions()
		{
			return _actions;
		}
	}
}
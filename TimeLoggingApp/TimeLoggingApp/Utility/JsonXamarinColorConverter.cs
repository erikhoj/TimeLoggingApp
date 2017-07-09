using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TimeLoggingApp.Utility
{
	public class JsonXamarinColorConverter : JsonCreationConverter<Color>
	{
		protected override Color Create(Type objectType, JObject jObject)
		{
			var r = jObject["R"].Value<float>();
			var g = jObject["G"].Value<float>();
			var b = jObject["B"].Value<float>();
			var a = jObject["A"].Value<float>();

			// Only Color.Default has negative R, G, B values
			if (Math.Abs(r - (-1f)) < 0.01f)
			{
				return Color.Default;
			}

			return new Color(r, g, b, a);
		}
	}
}
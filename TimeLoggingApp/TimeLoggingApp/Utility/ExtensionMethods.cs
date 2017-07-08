using System;
using System.Collections.Generic;

namespace BitBreak.Utility
{
	/// <summary>
	/// Miscellaneous extension methods.
	/// </summary>
	public static class MiscExtensions
	{
		public static string RemoveCloneSuffix(this string originalString)
		{
			return originalString.RemoveSuffix("(Clone)");
		}

		public static string RemoveSuffix(this string originalString, string suffix)
		{
			return originalString.Substring(0, originalString.Length - suffix.Length);
		}
		
		/// <summary>
		/// Converts a string to an enum, returns defaultValue if it fails.
		/// </summary>
		public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
		{
			if (strEnumValue == null || !Enum.IsDefined(typeof(TEnum), strEnumValue))
			{
				return defaultValue;
			}

			return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
		}

		/// <summary>
		/// Invokes a Func if it is not null, otherwise it does nothing.
		/// </summary>
		public static T SafeInvoke<T>(this Func<T> f)
		{
			return f != null ? f() : default(T);
		}

		/// <summary>
		/// Invokes an action if it is not null, otherwise it does nothing.
		/// </summary>
		public static void SafeInvoke(this Action action)
		{
			if (action == null) return;
			action();
		}

		/// <summary>
		/// Invokes an action if it is not null, otherwise it does nothing.
		/// </summary>
		public static void SafeInvoke<T>(this Action<T> action, T argument)
		{
			if (action == null) return;
			action(argument);
		}

		/// <summary>
		/// Invokes an action if it is not null, otherwise it does nothing.
		/// </summary>
		public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 argument1, T2 argument2)
		{
			if (action == null) return;
			action(argument1, argument2);
		}

		/// <summary>
		/// Invokes an action if it is not null, otherwise it does nothing.
		/// </summary>
		public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 argument1, T2 argument2, T3 argument3)
		{
			if (action == null) return;
			action(argument1, argument2, argument3);
		}

		/// <summary>
		/// Invokes an action if it is not null, otherwise it does nothing.
		/// </summary>
		public static void SafeInvoke<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 argument1, T2 argument2, T3 argument3, T4 argument4)
		{
			if (action == null) return;
			action(argument1, argument2, argument3, argument4);
		}

		/// <summary>
		/// Adds key with value to the dictionary if key doesn't already exists. Otherwise, replaces key with value
		/// </summary>
		public static void AddOrReplace<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key, T2 value)
		{
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] = value;
			}
			else
			{
				dictionary.Add(key, value);
			}
		}

		/// <summary>
		/// Returns value for key if one exists, otherwise returns default value.
		/// </summary>
		public static T2 GetIfExists<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key)
		{
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			else
			{
				return default(T2);
			}
		}
	}
}

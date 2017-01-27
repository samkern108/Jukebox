using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;

public static class Extensions {

	public static float Map (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	public static void DrawCircle(this LineRenderer line, float radius, int numPositions) {
		float x, y;
		float delta = (2 * Mathf.PI) / numPositions;
		line.numPositions = numPositions + 1;
		for (int i = 0; i < (numPositions + 1); i++) 
		{
			x = radius * Mathf.Cos (delta * i);
			y = radius * Mathf.Sin (delta * i);
			line.SetPosition (i, line.transform.position + new Vector3(x, y, 0));
		}
	}
}

/// <summary>
/// http://forum.unity3d.com/threads/change-the-value-of-a-toggle-without-triggering-onvaluechanged.275056/#post-2348336
///
/// Problem:
///     When setting a Unity UI Toggle field isOn, it automatically fires the onchanged event.
///
/// This class allows you to set the Toggle, Slider, Scrollbar and Dropdown's value without invoking the onchanged event.
/// It mostly does this by invoking the private method ('Set(value, sendCallback)') contained in some of the Unity UI elements
/// </summary>
public static class UISetExtensions
{
	private static readonly MethodInfo toggleSetMethod;
	private static readonly MethodInfo sliderSetMethod;
	private static readonly MethodInfo scrollbarSetMethod;

	private static readonly FieldInfo dropdownValueField;
	private static readonly MethodInfo dropdownRefreshMethod;  // Unity 5.2 <= only

	static UISetExtensions()
	{
		// Find the Toggle's set method
		toggleSetMethod = FindSetMethod(typeof (Toggle));

		// Find the Slider's set method
		sliderSetMethod = FindSetMethod(typeof (Slider));

		// Find the Scrollbar's set method
		scrollbarSetMethod = FindSetMethod(typeof (Scrollbar));

		// Find the Dropdown's value field and its' Refresh method
		dropdownValueField = (typeof (Dropdown)).GetField("m_Value", BindingFlags.NonPublic | BindingFlags.Instance);
		dropdownRefreshMethod = (typeof (Dropdown)).GetMethod("Refresh", BindingFlags.NonPublic | BindingFlags.Instance);  // Unity 5.2 <= only
	}

	public static void Set(this Toggle instance, bool value, bool sendCallback = false)
	{
		toggleSetMethod.Invoke(instance, new object[] {value, sendCallback});
	}

	public static void Set(this Slider instance, float value, bool sendCallback = false)
	{
		sliderSetMethod.Invoke(instance, new object[] {value, sendCallback});
	}

	public static void Set(this Scrollbar instance, float value, bool sendCallback = false)
	{
		scrollbarSetMethod.Invoke(instance, new object[] {value, sendCallback});
	}

	public static void Set(this Dropdown instance, int value)
	{
		dropdownValueField.SetValue(instance, value);
		dropdownRefreshMethod.Invoke(instance, new object[] {}); // Unity 5.2 <= only

		/* In Unity v. 5.3 and above, they removed the private "Refresh" method and now instead you need to call instance.RefreshShownValue(); instead. */
		// instance.RefreshShownValue(); // Unity 5.3 >= only
	}

	private static MethodInfo FindSetMethod(System.Type objectType)
	{
		var methods = objectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
		for (var i = 0; i < methods.Length; i++)
		{
			if (methods[i].Name == "Set" && methods[i].GetParameters().Length == 2)
			{
				return methods[i];
			}
		}

		return null;
	}
}

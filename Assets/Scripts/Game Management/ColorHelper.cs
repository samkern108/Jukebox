using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour {

	private static Color purple = new Color(149, 45, 149);
	private static Color yellow = new Color(1, 1, 0);
	private static Color orange = new Color(255, 155, 0);

	public static Color CombineColors(Color one, Color two) {
		Color newColor = new Color();

		newColor.r = Mathf.Clamp(one.r + two.r, 0, 1);
		newColor.g = Mathf.Clamp(one.g + two.g, 0, 1);
		newColor.b = Mathf.Clamp(one.b + two.b, 0, 1);
		newColor.a = 1;

		return ConvertColor (newColor);
	}

	private static Color ConvertColor(Color color) {

		if (color == Color.white)
			return Color.white;
		//(1,0,0)
		if(color == Color.red)
			return color;
		//(0,0,1)
		if(color == Color.blue)
			return color;
		//(0,1,0)
		if(color == Color.green)
			return Color.yellow;
		//(0,1,1)
		if(color == Color.cyan)
			return Color.green;
		//(1,0,1)
		if(color == Color.magenta)
			return purple;
		//(1,1,0)
		if(color == yellow)
			return orange;

		//Default to black.
		return Color.black;
	}
}

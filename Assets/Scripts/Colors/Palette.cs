using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Palette {

	public static Color red = new Color (0.96f, 0.25f, 0.25f, 1.0f);
	public static Color orange = new Color(0.98f, 0.81f, 0.30f, 1.0f);
	public static Color yellow = new Color(0.94f, 0.94f, 0.25f, 1.0f);
	public static Color green = new Color(0.50f, 0.94f, 0.41f, 1.0f);
	public static Color blue = new Color(0.45f, 0.84f, 1.0f, 1.0f);
	public static Color purple = new Color(0.79f, 0.41f, 0.94f, 1.0f);
	public static Color black = new Color(0f, 0f, 0f, 1.0f);
	public static Color white = new Color(1f, 1f, 1f, 1.0f);

	public static Dictionary<string, Color> colorNames = new Dictionary<string, Color> () {
		{"red", red}, {"orange", orange}, {"yellow", yellow},
		{"green", green}, {"blue", blue}, {"purple", purple},
		{"white", white}, {"black", black}};

	public static Dictionary<Color, Func<Color, Color>> MixColor = new Dictionary<Color, Func<Color, Color>>() {
		{red, color => CombineRed((Color)color)},
		{orange, color => CombineOrange((Color)color)},
		{yellow, color => CombineYellow((Color)color)},
		{green, color => CombineGreen((Color)color)},
		{blue, color => CombineBlue((Color)color)},
		{purple, color => CombinePurple((Color)color)},
		{white, color => CombineWhite((Color)color)},
		{black, color => CombineBlack((Color)color)}};

	//{red, (Color color)=>CombineRed}, {orange, (Color color)=>CombineOrange(Color)}};

	private static Color CombineRed(Color color) {

		Debug.Log ("CombineRed " + color);
		if (color == Palette.orange)
			return orange;
		if (color == Palette.yellow)
			return orange;
		if (color == Palette.green)
			return green;
		if (color == Palette.blue)
			return purple;
		if (color == Palette.purple)
			return purple;
		
		return red;
	}

	private static Color CombineOrange(Color color) {

		if (color == Palette.green)
			return green;
		if (color == Palette.blue)
			return blue;
		if (color == Palette.purple)
			return purple;

		return orange;
	}

	public static Color CombineYellow(Color color) {

		if (color == Palette.red)
			return orange;
		if (color == Palette.orange)
			return orange;
		if (color == Palette.green)
			return green;
		if (color == Palette.blue)
			return green;
		if (color == Palette.purple)
			return purple;

		return yellow;
	}

	public static Color CombineGreen(Color color) {

		if (color == Palette.red)
			return red;
		if (color == Palette.orange)
			return orange;
		if (color == Palette.purple)
			return purple;

		return green;
	}

	public static Color CombineBlue(Color color) {

		Debug.Log ("CombineBlue " + color);
		if (color == Palette.red)
			return purple;
		if (color == Palette.orange)
			return orange;
		if (color == Palette.yellow)
			return green;
		if (color == Palette.green)
			return green;
		if (color == Palette.purple)
			return purple;

		return blue;
	}

	public static Color CombinePurple(Color color) {

		if (color == Palette.orange)
			return orange;
		if (color == Palette.yellow)
			return yellow;
		if (color == Palette.green)
			return green;

		return purple;
	}

	public static Color CombineWhite(Color color) {
		return color;
	}

	public static Color CombineBlack(Color color) {
		return color;
	}
}
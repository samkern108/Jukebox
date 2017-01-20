using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoEditorPanel : MonoBehaviour {

	private static GameObject p_ColorPanel;

	public static void InitializeStereoColors (float[][] stereoColors)
	{
		p_ColorPanel = ResourceLoader.LoadPrefab(ResourceNamePrefab.ColorPanel);
		GameObject self = GameObject.Find ("EditStereoPanel"); //hacky :)

		Color c;
		GameObject colorPanel;
		float theta = 0;
		foreach (float[] color in stereoColors) {
			c = new Color (color[0], color[1], color[2], 1);
			colorPanel = Instantiate (p_ColorPanel);
			colorPanel.GetComponent <SpriteRenderer>().color = c;

			colorPanel.transform.SetParent (self.transform, false);
			colorPanel.transform.position = new Vector2 (3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta));
			theta += ((2 * Mathf.PI) / stereoColors.Length);
		}

		self.SetActive (false);
	}
}

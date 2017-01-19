using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoEditorPanel : MonoBehaviour {

	private static GameObject p_ColorPanel;
	private static StereoEditorPanel self;

	public void Awake() {
		p_ColorPanel = ResourceLoader.LoadPrefab(ResourceNamePrefab.ColorPanel);
		self = this;
		gameObject.SetActive (false);
	}

	public static void InitializeStereoColors (float[][] stereoColors)
	{
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
	}
}

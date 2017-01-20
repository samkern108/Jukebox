using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoEditorPanel : MonoBehaviour {

	private static GameObject p_ColorPanel;
	private static Stereo activeStereo;
	private static GameObject colorsPanel;

	private static Slider[] sliders;

	private static Canvas canvas;

	public void Awake()
	{
		colorsPanel = transform.Find ("Colors").gameObject;
		canvas = this.gameObject.GetComponent<Canvas>();
		sliders = transform.GetComponentsInChildren<Slider>();

		for(int i = 0; i < sliders.Length; i++) {
			sliders[i].onValueChanged.AddListener(delegate{UpdateStereoBeat();}); 
		}
	}

	public static void InitializeStereoColors (float[][] stereoColors)
	{
		p_ColorPanel = ResourceLoader.LoadPrefab(ResourceNamePrefab.ColorPanel);

		Color c;
		GameObject colorPanel;
		float theta = Mathf.PI/2;
		foreach (float[] color in stereoColors) {
			c = new Color (color[0], color[1], color[2], 1);

			colorPanel = Instantiate (p_ColorPanel);
			colorPanel.GetComponent <Image>().color = c;

			colorPanel.transform.SetParent (colorsPanel.transform, false);
			colorPanel.transform.position = new Vector2 (3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta));
			theta += ((2 * Mathf.PI) / stereoColors.Length);

			colorPanel.GetComponent<Button>().onClick.AddListener(() => { ChangeStereoColor(c); }); 
		}
	}

	public static void EditorModeOn(Stereo stereo)
	{
		activeStereo = stereo;
		//colorsPanel.transform.position = stereo.transform.position;
		canvas.transform.position = stereo.transform.position;

		for(int i = 0; i < sliders.Length; i++) {
			sliders [i].value = stereo.beatValues [i];
		}

		// Set slider values to be the stereo's beat values.
	}

	public static void EditorModeOff()
	{
		//activeStereo = null;
	}

	public static void ChangeStereoColor(Color color)
	{
		activeStereo.SetColor (color);
		EditorModeOff ();
	}

	public static void UpdateStereoBeat()
	{
		for (int i = 0; i < sliders.Length; i++) {
			activeStereo.SetBeatValue (i, (int)(sliders [i].value));
		}
	}
}

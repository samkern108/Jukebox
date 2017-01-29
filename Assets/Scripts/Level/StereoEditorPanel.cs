﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoEditorPanel : MonoBehaviour {

	public static bool active = false;

	private static GameObject p_ColorPanel;
	private static GameObject controlsPanel;

	private static Slider[] sliders;

	private static Canvas canvas;

	public void Awake()
	{
		controlsPanel = transform.Find ("Controls").gameObject;
		canvas = this.gameObject.GetComponent<Canvas>();
		sliders = GetComponentsInChildren<Slider>();

		for(int i = 0; i < sliders.Length; i++) {
			sliders[i].onValueChanged.AddListener(delegate{UpdateStereoBeat();}); 
		}

		controlsPanel.SetActive (false);
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

			colorPanel.transform.SetParent (controlsPanel.transform, false);
			colorPanel.transform.position = new Vector2 (3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta));
			theta += ((2 * Mathf.PI) / stereoColors.Length);

			colorPanel.GetComponent<Button>().onClick.AddListener(() => { ChangeStereoColor(c); }); 
		}
	}

	public static void EditorModeOn(Stereo stereo)
	{
		for(int i = 0; i < sliders.Length; i++) {
			sliders [i].Set(stereo.beatValues [i], false);
		}
	
		canvas.transform.position = stereo.transform.position;

		controlsPanel.SetActive (true);
		active = true;
	}

	public static void EditorModeOff()
	{
		controlsPanel.SetActive (false);
		for(int i = 0; i < sliders.Length; i++) {
			sliders [i].Set(0, false);
		}
		active = false;
	}

	public static void ChangeStereoColor(Color color)
	{
		StereoManager.selectedStereo.SetColor (color);
	}

	// This should ONLY update when dragged... not when changed programmatically.
	public static void UpdateStereoBeat()
	{
		if (StereoManager.selectedStereo == null)
			return;

		for (int i = 0; i < sliders.Length; i++)
			StereoManager.selectedStereo.SetBeatValue (i, (int)(sliders [i].value));
	}
}
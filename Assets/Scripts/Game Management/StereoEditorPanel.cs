using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoEditorPanel : MonoBehaviour {

	public static bool active = false;

	private static GameObject p_ColorPanel;
	private static GameObject controlsPanel;
	private static Transform highlight;

	private static LineRenderer lineRenderer;

	private static Slider[] sliders;

	private static Canvas canvas;

	public void Awake()
	{
		controlsPanel = transform.Find ("Controls").gameObject;
		highlight = controlsPanel.transform.Find ("Highlight");
		canvas = this.gameObject.GetComponent<Canvas>();
		sliders = GetComponentsInChildren<Slider>();

		for(int i = 0; i < sliders.Length; i++) {
			sliders[i].onValueChanged.AddListener(delegate{UpdateStereoBeat();}); 
		}

		lineRenderer = GetComponent <LineRenderer>();

		controlsPanel.SetActive (false);
	}

	public static void InitializeStereoColors (string[] stereoColors)
	{
		p_ColorPanel = ResourceLoader.LoadPrefab(ResourceNamePrefab.ColorPanel);

		float theta = Mathf.PI/2;

		foreach (string color in stereoColors) {
			Color c;
			GameObject colorPanel;
			c = Palette.colorNames[color];

			colorPanel = Instantiate (p_ColorPanel);
			colorPanel.GetComponent <Image>().color = c;

			colorPanel.transform.SetParent (controlsPanel.transform, false);
			colorPanel.transform.position = new Vector2 (3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta));
			colorPanel.transform.Rotate (new Vector3(0, 0, (theta - Mathf.PI/2) * (180/Mathf.PI)));

			theta += ((2 * Mathf.PI) / stereoColors.Length);

			colorPanel.GetComponent<Button>().onClick.AddListener(() => { ChangeStereoColor(c); }); 
		}
	}

	public static void EditorModeOn(Stereo stereo)
	{
		for(int i = 0; i < sliders.Length; i++) {
			sliders [i].Set(stereo.beatValues [i], false);
			lineRenderer.SetPosition (i, sliders[i].transform.position);
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

		if(StereoManager.selectedStereo && StereoManager.selectedStereo.deactivated) {
			Destroy (StereoManager.selectedStereo.gameObject);
			StereoManager.selectedStereo = null;
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

		for (int i = 0; i < sliders.Length; i++) {
			StereoManager.selectedStereo.SetBeatValue (i, (int)(sliders [i].value));
			lineRenderer.SetPosition (i, sliders[i].transform.position);
		}
	}

	public void Tick(int beat) {
		highlight.position = sliders [beat].transform.position;
	}

	public void Pause(bool pause) {
		if (pause) {
			EditorModeOff ();
			highlight.gameObject.SetActive (false);
		} else
			Invoke ("ActivateTickOnBeat",BeatMaster.timeBetweenBeats);
	}

	private void ActivateTickOnBeat() {
		highlight.gameObject.SetActive (true);
	}
}

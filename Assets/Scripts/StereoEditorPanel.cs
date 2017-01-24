using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoEditorPanel : MonoBehaviour {

	public static bool active = false;

	private static GameObject p_ColorPanel;
	private static Stereo activeStereo;
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

	public void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("UI") | 1 << LayerMask.NameToLayer ("Stereo"));

			if(!hit.collider) {
				StereoEditorPanel.EditorModeOff ();
			}
			else if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Stereo")) {
				Stereo stereo = hit.collider.gameObject.GetComponent <Stereo> ();
				if (activeStereo != stereo) {
					EditorModeOn (stereo);
				}
			} else if (hit.collider.gameObject.layer != LayerMask.NameToLayer ("UI")) {
				StereoEditorPanel.EditorModeOff ();
			}
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

			colorPanel.transform.SetParent (controlsPanel.transform, false);
			colorPanel.transform.position = new Vector2 (3 * Mathf.Cos(theta), 3 * Mathf.Sin(theta));
			theta += ((2 * Mathf.PI) / stereoColors.Length);

			colorPanel.GetComponent<Button>().onClick.AddListener(() => { ChangeStereoColor(c); }); 
		}
	}

	public static void EditorModeOn(Stereo stereo)
	{
		activeStereo = stereo;

		for(int i = 0; i < sliders.Length; i++) {
			sliders [i].value = stereo.beatValues [i];
		}
	
		canvas.transform.position = stereo.transform.position;

		controlsPanel.SetActive (true);
		active = true;
	}

	public static void EditorModeOff()
	{
		controlsPanel.SetActive (false);
		active = false;
	}

	public static void ChangeStereoColor(Color color)
	{
		activeStereo.SetColor (color);
	}

	public static void UpdateStereoBeat()
	{
		for (int i = 0; i < sliders.Length; i++) {
			activeStereo.SetBeatValue (i, (int)(sliders [i].value));
		}
	}
}

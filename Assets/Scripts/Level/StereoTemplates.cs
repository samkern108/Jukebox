using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StereoTemplates : MonoBehaviour {

	public static StereoTemplates self;

	private static GameObject p_stereoTemplate;
	private static float width;
	private static float padding = 18f;
	private static int numStereos = 0;

	public void Start() {
		self = this;
		p_stereoTemplate = ResourceLoader.LoadPrefab (ResourceNamePrefab.StereoTemplate);
		width = p_stereoTemplate.GetComponent <RectTransform> ().sizeDelta.y;
	}

	public void AddStereoTemplate(int key, Color color) {
		GameObject stereoTemplate = GameObject.Instantiate(p_stereoTemplate);
		stereoTemplate.transform.SetParent (self.transform, false);
		stereoTemplate.transform.position += new Vector3(numStereos * (padding + width),0,0);
		stereoTemplate.GetComponentInChildren <Image>().color = color;
		stereoTemplate.GetComponentInChildren <Text>().text = key + "";
		numStereos++;
	}
}

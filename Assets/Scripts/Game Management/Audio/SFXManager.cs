using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	private AudioSource source;
	public static float volume = 1f;
	private bool playing = false;

	public void Start()
	{
		source = GetComponent <AudioSource>();
	}

	public void PlayClip(string name)
	{
		if (playing)
			return;
		
		AudioClip clip = ResourceLoader.LoadSFX (name);
		source.PlayOneShot (clip, volume);
		playing = true;
		Invoke ("SafeToPlay", clip.length + .5f);
	}

	public void SafeToPlay()
	{
		playing = false;
	}
}

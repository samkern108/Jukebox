using UnityEngine;
using System.Collections;
using System.IO;

//The names of all resources that are used by the game. This keeps them in a centralized place.
public enum ResourceNamePrefab {Dot, Stereo, StereoShadow, PulseWaveCircle, PulseWaveRect, PulseWaveCone, Enemy, StereoTemplate, ColorPanel, Path, Spawner};
public enum ResourceNameIcon { Arrow, Spacebar, SelectionCircle, Mouse }
public enum ResourceNameSprite {};
public enum ResourceNameMusic {};
public enum SFXInstrument { Synth, Guitar, Thrash };

public class ResourceLoader : MonoBehaviour {

	private static string pathToMusic = "Audio/Music/";
	private static string pathToPrefabs = "Prefabs/";
	private static string pathToIconPrefabs = "Prefabs/Icons/";
	private static string pathToSprites = "Sprites/";

	private static string[] fileNamesAudio;

	public static GameObject LoadPrefab(ResourceNamePrefab prefabName)
	{
		return Resources.Load <GameObject>(pathToPrefabs + prefabName.ToString());
	}

	public static GameObject LoadIcon(ResourceNameIcon iconName)
	{
		return Resources.Load <GameObject>(pathToIconPrefabs + iconName.ToString());
	}

	public static Sprite LoadSprite(ResourceNameSprite name)
	{
		return Resources.Load <Sprite> (pathToSprites + name);
	}

	public static AudioClip LoadMusic(ResourceNameMusic name)
	{
		return Resources.Load <AudioClip> (pathToMusic + name);
	}

	private static string pathToSFX = "Audio/SFX/";
	private static string[] sfxThrash = new string[0], sfxGuitar = new string[0], sfxSynth = new string[0];

	public static AudioClip LoadSFX(string name)
	{
		return Resources.Load <AudioClip> (pathToSFX + name);
	}

	// Note: SFX and Audio are NOT under source control (too expensive).
	public static string GetRandomSFXName(SFXInstrument instr)
	{
		if (sfxThrash.Length == 0) {
			sfxGuitar = System.IO.Directory.GetFiles (Application.dataPath + "/Resources/" + pathToSFX + "GuitarNice1", "*.wav", SearchOption.AllDirectories);
			sfxSynth = System.IO.Directory.GetFiles (Application.dataPath + "/Resources/" + pathToSFX + "SynthNice1", "*.wav", SearchOption.AllDirectories);
			sfxThrash = System.IO.Directory.GetFiles (Application.dataPath + "/Resources/" + pathToSFX + "ThrashNice1", "*.wav", SearchOption.AllDirectories);
		}

		switch(instr) {
		case SFXInstrument.Synth:
			return sfxSynth [Random.Range (0, sfxSynth.Length)];
		case SFXInstrument.Guitar:
			return sfxGuitar [Random.Range (0, sfxGuitar.Length)];
		case SFXInstrument.Thrash:
			return sfxThrash [Random.Range (0, sfxThrash.Length)];
		}

		return "unknown";
	}

	public void InitLevel() {
		Destroy (this.gameObject);
	}
}
using UnityEngine;
using System.Collections;
using System.IO;

public class ResourceLoader : MonoBehaviour {

	private static string pathToSFX = "Audio/SFX/";
	private static string pathToMusic = "Audio/Music/";
	private static string pathToPrefabs = "Prefabs/";
	private static string pathToSprites = "Sprites/";

	public static GameObject LoadPrefab(ResourceNamePrefab prefabName)
	{
		return Resources.Load <GameObject>(pathToPrefabs + prefabName.ToString());
	}

	public static Sprite LoadSprite(string name)
	{
		return Resources.Load <Sprite> (pathToSprites + name);
	}

	public static AudioClip LoadMusic(string name)
	{
		return Resources.Load <AudioClip> (pathToMusic + name);
	}

	public static AudioClip LoadSFX(string name)
	{
		return Resources.Load <AudioClip> (pathToSFX + name);
	}

	//The names of all resources that are used by the game. This keeps them in a centralized place.
	public enum ResourceNamePrefab {Dot, Stereo, PulseWave, Enemy};
	public enum ResourceNameSprite {};
	public enum ResourceNameMusic {};
	public enum ResourceNameAudioClip {};
}

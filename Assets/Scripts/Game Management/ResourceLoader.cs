using UnityEngine;
using System.Collections;
using System.IO;

//The names of all resources that are used by the game. This keeps them in a centralized place.
public enum ResourceNamePrefab {Dot, Stereo, StereoShadow, PulseWave, Enemy, StereoTemplate, ColorPanel, Path, Spawner};
public enum ResourceNameSprite {};
public enum ResourceNameMusic {};
public enum ResourceNameAudioClip {FSharp, G, BFlat};

public class ResourceLoader : MonoBehaviour {

	private static string pathToSFX = "Audio/SFX/";
	private static string pathToMusic = "Audio/Music/";
	private static string pathToPrefabs = "Prefabs/";
	private static string pathToSprites = "Sprites/";

	public static GameObject LoadPrefab(ResourceNamePrefab prefabName)
	{
		return Resources.Load <GameObject>(pathToPrefabs + prefabName.ToString());
	}

	public static Sprite LoadSprite(ResourceNameSprite name)
	{
		return Resources.Load <Sprite> (pathToSprites + name);
	}

	public static AudioClip LoadMusic(ResourceNameMusic name)
	{
		return Resources.Load <AudioClip> (pathToMusic + name);
	}

	public static AudioClip LoadSFX(ResourceNameAudioClip name)
	{
		return Resources.Load <AudioClip> (pathToSFX + name);
	}

	public static ResourceNameAudioClip GetRandomSFX()
	{
		System.Array sfx = ResourceNameAudioClip.GetValues(typeof(ResourceNameAudioClip));

		return (ResourceNameAudioClip)sfx.GetValue(Random.Range(0, sfx.Length));
	}
}

using UnityEngine;
using System.Collections;
using System.IO;

public class ResourceLoader : MonoBehaviour {

	private static string pathToSFX = "Audio/SFX/";
	private static string pathToMusic = "Audio/Music/";
	private static string pathToObjects = "Objects/";
	private static string pathToSprites = "Sprites/";

	public static GameObject LoadGameObject(string name)
	{
		return Resources.Load <GameObject>(pathToObjects + name);
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
}

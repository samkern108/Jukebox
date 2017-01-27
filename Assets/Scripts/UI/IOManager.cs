using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class IOManager {

	private static string pathToDataFolder;

	public static void Initialize()
	{
		pathToDataFolder = Application.dataPath + "/Resources/";
	}

	private static string ReadFromFile(string filepath)
	{
		StreamReader streamReader = new StreamReader(pathToDataFolder + filepath);
		string data = streamReader.ReadToEnd ();
		streamReader.Close();
		return data;
	}

	public static LevelJSON LoadLevel(string levelName)
	{
		string data = ReadFromFile ("Levels/"+levelName);
		return JsonConvert.DeserializeObject <LevelJSON>(data);
	}
		
	public static void SerializeAndSave(object obj, string filename)
	{
		string serialized = JsonConvert.SerializeObject(obj);

		StreamWriter streamWriter = new StreamWriter (pathToDataFolder + filename);

		streamWriter.Write (serialized);
		streamWriter.Flush ();
		streamWriter.Close ();
	}
}

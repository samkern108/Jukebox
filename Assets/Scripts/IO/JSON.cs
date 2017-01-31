using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelJSON
{
	public string name { get; set; }
	public int lives { get; set; }
	public string[] stereoColors { get; set; }
	public GridJSON grid { get; set; }
	public PathJSON[] paths { get; set; }
}
	
[System.Serializable]
public class GridJSON
{
	public int w { get; set; }
	public int h { get; set; }
}

[System.Serializable]
public class PathJSON
{
	public SpawnerJSON spawner { get; set; }
	public int[][] points { get; set; }
	public string endColor { get; set; }
}

[System.Serializable]
public class SpawnerJSON
{
	public int numEnemies { get; set; }
	public int beatsBetweenSpawn { get; set; }
	public string startColor { get; set; }
}
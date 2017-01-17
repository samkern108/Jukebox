using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelJSON
{
	public string name { get; set; }
	public int lives { get; set; }
	public StereoJSON[] stereos { get; set; }
	public GridJSON grid { get; set; }
	public PathJSON[] paths { get; set; }
}

[System.Serializable]
public class StereoJSON
{
	public float[] color { get; set; }
	public float radius { get; set; }
	public float strength { get; set; }
	public int beatsBetweenPulses { get; set; }
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
	public int[][] points { get; set; }
}
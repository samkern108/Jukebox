﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMusicManager : MonoBehaviour {

	public static AudioSource player;

	public void Start()
	{
		player = GetComponent<AudioSource>();
	}

	public static void AdjustVolume(float volume)
	{
		player.volume = volume;
	}

}

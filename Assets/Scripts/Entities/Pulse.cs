using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse {

	public float radius;
	public float speed;
	public float strength;
	public int beatsBetweenPulses;
	public Color pulseColor;
	public string sfxName;

	// Used by Dot
	public Vector2 position;
	public float lifeTime;

	public Pulse() {
		this.radius = 0;
		this.speed = 0f;
		this.strength = 0f;
		this.beatsBetweenPulses = 0;
		this.pulseColor = Color.white;
		ChangeColor (Color.white);
		this.lifeTime = 0f;
	}

	public void ChangeColor(Color color) {
		color.a = 1;
		pulseColor = color; 

		SFXInstrument instr = SFXInstrument.Synth;
		if (color == Color.red)
			instr = SFXInstrument.Synth;
		else if (color == Color.blue)
			instr = SFXInstrument.Guitar;
		else if (color == Color.green)
			instr = SFXInstrument.Thrash;
			
		string name = ResourceLoader.GetRandomSFXName(instr);

		// TODO(samkern): Probably should make this less janky.
		string[] substring = name.Split ('/');

		this.sfxName = substring[substring.Length - 2] + "/" + substring[substring.Length - 1];
		//removing .wav extension
		this.sfxName = sfxName.Substring (0, sfxName.Length - 5);
	}
}
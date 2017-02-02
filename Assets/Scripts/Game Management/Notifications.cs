using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifications : MonoBehaviour {

	public static Notifications self;

	public void Start() {
		self = this;
		LevelMaster.OkToLoad ();
	}

	public static void SendRestartNotification() {
		self.BroadcastMessage ("InitLevel");
	}

	public static void SendPauseNotification(bool pause) {
		self.BroadcastMessage ("Pause", pause);
	}
}

﻿using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public static ChangeScene control;

	// Use this for initialization
	void Awake () {
		if(control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)) {
			Application.LoadLevel("battle");
		}
	}
}
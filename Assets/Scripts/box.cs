﻿using UnityEngine;
using System.Collections;

public class box : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown(){
		print ("box is clicked");
		Cut ();
	}
	public void Cut (){
		Destroy (transform.parent.transform. parent.gameObject);
		//	StartCoroutine ("Disappearing");
	}
	void Update () {
	
	}


}

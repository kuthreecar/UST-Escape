﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogSystem : MonoBehaviour
{
		bool dialogisOn = false;
		public GameObject dialogpf;
		GameObject dialogbg;
		bool waitForClick = false;
		bool isClick = false;
		static public bool toCreateDialog = false;
		static public string[]nameString;
		static public string[]dialogString;
	public Queue<string>cnameString = new Queue<string> ();
	public Queue<string> cdialogString = new Queue<string> ();

		// Use this for initialization
		void Start ()
		{

				//test
				toCreateDialog = true;
				nameString = new string[]{"principal","principal"};
				dialogString = new string[]{"fuck\nfuck","you"};
		}
	
		// Update is called once per frame
		void Update ()
		{
		if (!dialogisOn) {
						if (toCreateDialog) {
								CreateDialog ();
				toCreateDialog = false;
				dialogisOn = true;
						}
					
				}
		if (dialogisOn) {
			if (waitForClick)
			if (Input.GetMouseButtonDown (0)){
					isClick = true;
	}
			if (isClick) {
				if (cdialogString.Count > 0) {
					dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text = cnameString.Dequeue ();
					dialogbg.transform.GetChild (2).GetComponent<TextMesh> ().text = cdialogString.Dequeue ();
					waitForClick = true;
					isClick = false;
				}
				else{
					Destroy(dialogbg);
					dialogisOn=false;
				}
			}
			
		}

		}

		void CreateDialog ()
		{

				foreach (string s in nameString)
						cnameString.Enqueue (s);
				foreach (string s in dialogString)
			cdialogString.Enqueue (s);

				dialogbg = Instantiate (dialogpf, new Vector3 (0, -3f, -30), Quaternion.identity)as GameObject;
		isClick = true;
				/*
		for (int i=0; i<num; i++) {
						if (isClick) {
								dialogbg = Instantiate (dialogpf, new Vector3(0,0,-30), Quaternion.identity)as GameObject;
								dialogbg.transform.GetChild (1).GetComponent<TextMesh> ().text = name;
								dialogbg.transform.GetChild (2).GetComponent<TextMesh> ().text = alldialog [0];
								waitForClick = true;
								isClick=false;
						}
				}*/
				dialogisOn = false;
		waitForClick = true;
		}
}

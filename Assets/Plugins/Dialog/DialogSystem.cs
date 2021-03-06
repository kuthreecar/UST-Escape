﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DialogSystem : MonoBehaviour
{
		GameObject dialogInstance;
		bool dialogisOn = false;
		bool waitForClick = false;
		bool isClick = false;
		static public bool toCreateDialog = false;
		static public character[]	nameString;
		static public string[]dialogString;
		character[] pic = {character.NOPIC,character.NOPIC}; //[0] is the left pic, [1] is the right
		int dialog_counter = 0;
		int myId = -1;
		bool option_Mode = false;
		bool playerOption = false;
		bool bigIcon_Mode = false;
		DialogInterface curDi = null;
		Sprite bigIconSprite = null;
		public GameObject dialogPrefab;
		public Queue<string>cnameString = new Queue<string> ();
		public Queue<string> cdialogString = new Queue<string> ();
		public Sprite dialogPrincipal ;
		public Sprite dialogPlayer;
		public Sprite dialogDembeater_Flag;
		public Sprite dialogDembeater_Leg;
		public Sprite dialogVoldemort;
		public Sprite dialogCleaningLady;
		public Sprite dialogDimjack;
		public Sprite dialogGirlGod;
		bool occupied = false;

		struct DialogGroup
		{
				public int type;
				public character[] nameString;
				public		string[] contentString;
				public	DialogInterface di;
				public		int id;
				public		Sprite sprite;

				public DialogGroup (int a1, character[] a2, string[] a3, DialogInterface a4, int a5, Sprite a6)
				{
						type = a1;
						nameString = a2;
						contentString = a3;
						di = a4;
						id = a5;
						sprite = a6;
				}
		

		};	

		Queue<DialogGroup>bufferedDialog = new Queue<DialogGroup> ();
	
	
		public enum character
		{
				PLAYER=0,
				PRINCIPAL,
				CLEANINGLADY,
				DEMBEATER_FLAG,
				DEMBEATER_LEG,
				GIRLGOD,
				VOLDEMORT,
				DIMJACK,
				SYSTEM,
				NOPIC
		}
		;



		// Use this for initialization
		void Start ()
		{
				//test
				//toCreateDialog = true;
//				nameString = new character[]{character.PRINCIPAL,character.PRINCIPAL};
//				dialogString = new string[]{"Hello","Hello"};

		}

		public bool isOperating ()
		{
				return occupied;
		}


/*		public void startDialog (string _nameString, string _dialogString)
		{ 
				toCreateDialog = true;
				nameString = new string[]{_nameString};
				dialogString = new string[]{_dialogString};

		}*/
		public void startShowBigIcon (character[] _nameString, string[] _dialogString, DialogInterface di, int id, Sprite s)
		{
				if (occupied) {
						Debug.Log ("Buffering type3 dialog");
						DialogGroup dg = new DialogGroup (3, _nameString, _dialogString, di, id, s);
						bufferedDialog.Enqueue (dg);
						return;
				}

				occupied = true;
				myId = -1;
				curDi = null;
				toCreateDialog = true;

				nameString = _nameString;
				dialogString = _dialogString;
				myId = id;
				curDi = di;
				option_Mode = false;
				bigIcon_Mode = true;


				bigIconSprite = s;

		}

		public void startOptionDialog (character _nameString, string _dialogString, DialogInterface di, int id)
		{

				if (occupied) {
						Debug.Log ("Buffering type2 dialog");
						nameString = new character[]{_nameString};
						dialogString = new string[]{_dialogString};
						DialogGroup dg = new DialogGroup (2, nameString, dialogString, di, id, null);
						bufferedDialog.Enqueue (dg);
						return;
				}
				occupied = true;
				myId = -1;
				curDi = null;
				toCreateDialog = true;

				nameString = new character[]{_nameString};
				dialogString = new string[]{_dialogString};
				myId = id;
				curDi = di;
				option_Mode = true;
				bigIcon_Mode = false;

		}

		public void startDialogs (character[] _nameString, string[] _dialogString, DialogInterface di, int id)
		{
				if (occupied) {
						Debug.Log ("Buffering type1 dialog");
						DialogGroup dg = new DialogGroup (1, _nameString, _dialogString, di, id, null);
						bufferedDialog.Enqueue (dg);
						return;
				}
				occupied = true;
				myId = -1;
				curDi = null;
				toCreateDialog = true;
				nameString = _nameString;
				dialogString = _dialogString;
				myId = id;
				curDi = di;
				option_Mode = false;
				bigIcon_Mode = false;
		}

		public void onFinish ()
		{

	
				pic [0] = character.NOPIC;
				pic [1] = character.NOPIC;
				dialog_counter = 0;
				



				if (!option_Mode)
						curDi.onDialogFinish (myId, -1);
				else {
	
						if (playerOption)
								curDi.onDialogFinish (myId, 1);
						else
								curDi.onDialogFinish (myId, 0);

						playerOption = false;
						option_Mode = false;
				}

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!occupied) {
						if (bufferedDialog.Count != 0) {
								DialogGroup dg = bufferedDialog.Dequeue ();
								switch (dg.type) {
								case 3:
										startShowBigIcon (dg.nameString, dg.contentString, dg.di, dg.id, dg.sprite);
										break;
								case 2:
										startOptionDialog (dg.nameString [0], dg.contentString [0], dg.di, dg.id);
										break;
								case 1:
										startDialogs (dg.nameString, dg.contentString, dg.di, dg.id);
										break;
										return;
								}
						}
				}

				if (!dialogisOn) {

						if (toCreateDialog) {
								CreateDialog ();
								toCreateDialog = false;
								dialogisOn = true;
						} 
				
				}
				if (dialogisOn) {
						if (waitForClick)
						if (Input.GetMouseButtonDown (0)) {
								if (!option_Mode)
										isClick = true;
						}
						if (isClick) {
								gameObject.audio.Play ();
								if (cdialogString.Count > 0) {
										dialogInstance.transform.GetChild (1).GetComponent<TextMesh> ().text = cnameString.Dequeue ();
										dialogInstance.transform.GetChild (2).GetComponent<TextMesh> ().text = cdialogString.Dequeue ();
										//charaterpic
		
										if (pic [0] == nameString [nameString.Length - dialog_counter]) {
							
												if (dialogInstance.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (3).renderer.material.color = new Color (dialogInstance.transform.GetChild (3).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.b, 1f);
												if (dialogInstance.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (4).renderer.material.color = new Color (dialogInstance.transform.GetChild (4).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.b, 0.3f);
										} else if (pic [1] == nameString [nameString.Length - dialog_counter]) {

												if (dialogInstance.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (3).renderer.material.color = new Color (dialogInstance.transform.GetChild (3).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.b, 0.3f);
												if (dialogInstance.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (4).renderer.material.color = new Color (dialogInstance.transform.GetChild (4).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.b, 1f);
										} else {

												if (dialogInstance.transform.GetChild (3).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (3).renderer.material.color = new Color (dialogInstance.transform.GetChild (3).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (3).renderer.material.color.b, 0.3f);
												if (dialogInstance.transform.GetChild (4).GetComponent<SpriteRenderer> ().sprite != null)
														dialogInstance.transform.GetChild (4).renderer.material.color = new Color (dialogInstance.transform.GetChild (4).renderer.material.color.r, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.g, 
							                                                                           dialogInstance.transform.GetChild (4).renderer.material.color.b, 0.3f);

										}

										dialog_counter--;

										waitForClick = true;
										isClick = false;
								} else {
										
										Destroy (dialogInstance);

										onFinish ();
										dialogisOn = false;
										occupied = false;
										GamePause.continueGame ();
								}
						}
				
				}
			
		}

		private void swapPlayer (character[] myCStr)
		{
				if (myCStr.Length != 3)
						return;
				if (myCStr [0] == character.PLAYER) {
						myCStr [0] = myCStr [1];
						myCStr [1] = myCStr [2];
						myCStr [2] = character.PLAYER;
				} else if (myCStr [1] == character.PLAYER) {
						myCStr [1] = myCStr [2];
						myCStr [2] = character.PLAYER;
				}

		}

		private void setPic ()
		{
				if (bigIcon_Mode) {

						dialogInstance.transform.GetChild (7).GetComponent<SpriteRenderer> ().sprite = bigIconSprite;


						return;
				}
				character[] myCStr = nameString.Distinct ().ToArray ();
				swapPlayer (myCStr);


				for (int i=0; i<myCStr.Length; i++) {
						if (i >= 2)
								return;
						pic [i] = myCStr [i];


						switch (myCStr [i]) {

						case character.PLAYER:	
								
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogPlayer;
								break;
						case character.PRINCIPAL:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogPrincipal;
								break;
						case character.CLEANINGLADY:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogCleaningLady;
								break;
						case character.DEMBEATER_FLAG:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDembeater_Flag;
								break;
						case character.DEMBEATER_LEG:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDembeater_Leg;
								break;

						case character.GIRLGOD:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogGirlGod;
								break;
						case character.VOLDEMORT:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogVoldemort;
								break;
						case character.DIMJACK:
								dialogInstance.transform.GetChild (i + 3).GetComponent<SpriteRenderer> ().sprite = dialogDimjack;
								break;
						}

				}	
		}
		
		public void onChildrenTouched (string name)
		{
				switch (name) {
				case "yesBtn":
						Debug.Log ("Yes clicked");
						gameObject.audio.Play ();
						playerOption = true;
						break;
				case "noBtn":
						Debug.Log ("No clicked");
						gameObject.audio.Play ();
						playerOption = false;
						break;
				}

			
				
				Destroy (dialogInstance);
				onFinish ();
				dialogisOn = false;
				occupied = false;
				GamePause.continueGame ();
		}

		void CreateDialog ()
		{
				Debug.Log ("Creating Dialogue");
		
				
				for (int i=0; i<nameString.Length; i++) {

						switch ((int)nameString [i]) {
						case 0:
								cnameString.Enqueue ("You");
								break;
						case 1:
								cnameString.Enqueue ("Principal");
								break;
						case 2:
								cnameString.Enqueue ("Cleaning Lady");
								break;
						case 3:
								cnameString.Enqueue ("Dembeater1");
								break;
						case 4:
								cnameString.Enqueue ("Dembeater2");
								break;
						case 5:
								cnameString.Enqueue ("The (girl)?");
								break;
						case 6:
								cnameString.Enqueue ("Voldemort");
								break;
						case 7:
								cnameString.Enqueue ("DumJack");
								break;
						case 8:
								cnameString.Enqueue ("System");
								break;
						}
			
				}	
				dialog_counter = nameString.Length;

				foreach (string s in dialogString)
						cdialogString.Enqueue (s);

				Vector3 position = Camera.mainCamera.gameObject.transform.position;
				position.y -= 3;
				position.z -= 3.5f;
		if (Application.loadedLevelName == "SecretChamberBefore" || Application.loadedLevelName == "SecretChamberAfter"|| Application.loadedLevelName == "sundial") {
						Debug.Log ("Detect 2D scene by DS");

						position.x = 0;
						position.z = 0;
						dialogInstance = Instantiate (dialogPrefab, position, Quaternion.Euler (0, 0, 0))as GameObject;
				} else {

						dialogInstance = Instantiate (dialogPrefab, position, Quaternion.Euler (90, 0, 0))as GameObject;

				}
				if (!option_Mode)
						dialogInstance.transform.GetChild (5).gameObject.SetActive (false);
				if (!option_Mode)
						dialogInstance.transform.GetChild (6).gameObject.SetActive (false);

				setPic ();
				dialogInstance.transform.localScale -= new Vector3 (0.2F, 0.2F, 0.8F);
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
				GamePause.pauseGame ();
			
		}
}

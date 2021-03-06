﻿using UnityEngine;
using System.Collections;

public class Multi_QTETriangleHelper : MonoBehaviour {

	public Multi_QTETriangle parent;

	void Start () {
		if (Application.loadedLevelName != "sundial")
		do {
			parent = GameObject.Find ("QTETri(Clone)").GetComponent<Multi_QTETriangle> ();
		} while(parent==null);
	}
	
	
	void  OnTriggerEnter2D (Collider2D col)
	{
		//if (parent != null)parent.slotTriggerEnter (col);
	}
	
	void  OnTriggerStay2D (Collider2D col)
	{
		if (parent != null)parent.baseTriggerStay (col);
	}
	void  OnTriggerExit2D (Collider2D col)
	{
		if (parent != null)parent.baseTriggerExit (col);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ticker : MonoBehaviour {

	public RectTransform tick;
	float currtime;

	public float against;
	
private void Start()
{
	currtime+=0.5f;
}
	void FixedUpdate () {
		currtime+=Time.deltaTime;
	
		float sec=  currtime%60;
		if (sec>against){
			currtime=0;
			tick.eulerAngles=new Vector3(0,0,(tick.eulerAngles.z-6<=-360)?0:tick.eulerAngles.z-6);

		}
		
	}
}

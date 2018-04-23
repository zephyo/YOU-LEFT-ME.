using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ticker : MonoBehaviour {

	public RectTransform tick;
	float currtime;
	
	// Update is called once per frame
	void FixedUpdate () {
		currtime+=Time.deltaTime;
	
		int sec=  Convert.ToInt32(currtime%60);
		if (sec>=2){
			currtime=0;
			tick.eulerAngles=new Vector3(0,0,(tick.eulerAngles.z-6<=-360)?0:tick.eulerAngles.z-6);

		}
		
	}
}

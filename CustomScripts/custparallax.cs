using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class custparallax : MonoBehaviour {

	// Use this for initialization
	public RectTransform par;
	public float speed;
	public bool YToo;
	float origX, origY;

	void Awake(){
		origX=par.anchoredPosition.x;
		origY=par.anchoredPosition.y;

	}
	void Update () {
		float x= Input.mousePosition.x,y=Input.mousePosition.y;
		if (x>0 && x<Screen.width && y>0 && y<Screen.height){
			x-=Screen.width/2;
			if (YToo){

			par.anchoredPosition=new Vector2(origX+(x*speed),origY+(y*speed/2));}
			else{
			par.anchoredPosition=new Vector2(origX+(x*speed),origY);}
		}
		
	}
}

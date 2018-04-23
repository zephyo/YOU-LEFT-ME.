using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {

	float time=15;
	float cur;

	Vector3 begin, end;
	
	// Update is called once per frame

	private void Awake()
	{
		begin=new Vector3(1387.9f, 776.3f,300.8f);
		end= new Vector3(-566, -516.7f, -148.5f);
	}
	void FixedUpdate () {
			//from 1286, 776.3,290.4

			/*
			1211.1 676.6 290.4
			
			483.9 186.5 142.3

			176.3 -18.6 16.6
			
			-154.7 -220.6 -84

			-566 -516.7 -148.5
			 */

			cur+=Time.deltaTime;
			float t=cur/time;
			transform.localPosition=Vector3.Lerp(begin, end, t);
			if (t>1){
				cur=0;
			}

	}
}

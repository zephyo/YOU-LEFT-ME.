using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
 using UnityEngine.EventSystems;
 using UnityEngine.UI;


public class sendmessage : MonoBehaviour  {


	public SayDialog say;
	public MenuDialog menu;
	public Texture2D cursor;
	

	

    public void Enter()
    {
		if (say.canvasGroup.alpha ==0 && !menu.IsActive())
		Cursor.SetCursor(cursor,new Vector2(cursor.width/2,cursor.height/2),CursorMode.Auto);


	}
	public void Exit()
    {
			
     	   Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
	}

	public void Click(Image s)
     {
		 	if (say.canvasGroup.alpha ==0 && !menu.IsActive())
        Fungus.Flowchart.BroadcastFungusMessage (s.sprite.name);
		
     }

	 public void ClickString(string s)
     {
		 	if (say.canvasGroup.alpha ==0 && !menu.IsActive())
        Fungus.Flowchart.BroadcastFungusMessage (s);
     }
}

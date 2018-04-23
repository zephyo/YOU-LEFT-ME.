using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 using TMPro;

public class links : MonoBehaviour {

	// Use this for initialization
	 public TextMeshProUGUI textMeshPro;
	     public void Clicked()
     {
         
         int linkIndex = TMP_TextUtilities.FindIntersectingLink (textMeshPro, Input.mousePosition, null);
         if (linkIndex != -1) {
             TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo [linkIndex];
             switch (linkInfo.GetLinkID ()) {
             case "id_1":
                 Application.OpenURL ("https://twitter.com/zephybite");
                 break;
             case "id_2":
                 Application.OpenURL ("https://zephyo.tumblr.com");
                 break;
			case "id_3":
                 Application.OpenURL ("https://facebook.com/zephybite");
                 break;
                 case "id_4":
                 Application.OpenURL ("https://soundcloud.com/zephyo/i-woke-up-next-to-you-again");
                 break;
             }	
         }
     }

          public void Clicked2()
     {
         
         int linkIndex = TMP_TextUtilities.FindIntersectingLink (textMeshPro, Input.mousePosition, null);
         if (linkIndex != -1) {
             TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo [linkIndex];
             switch (linkInfo.GetLinkID ()) {
             case "id_1":
                 Application.OpenURL ("https://twitter.com/zephybite");
                 break;
             case "id_2":
                 Application.OpenURL ("https://zephyo.tumblr.com");
                 break;
			case "id_3":
                 Application.OpenURL ("https://facebook.com/zephybite");
                 break;
                 case "id_4":
                 Application.OpenURL ("https://soundcloud.com/zephyo/i-woke-up-next-to-you-again");
                 break;
             }	
         }
     }
 }


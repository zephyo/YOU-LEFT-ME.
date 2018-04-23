// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace Fungus
{
    /// <summary>
    /// Displays a button in a multiple choice menu.
    /// </summary>
    [CommandInfo("remember",
                 "camerasky",
                 "travel")]
    [AddComponentMenu("")]
    public class camerasky : Command
    {
        
      
      public float time;

      public Color c;

        #region Public members

        public override void OnEnter()
        {
            Image bg = manager.instance.transform.GetChild(0).GetComponent<Image>();
            Color current = bg.color;

            LeanTween.value(manager.instance.gameObject, (float v)=>{
                float rat= v/1;
                  bg.color = new Color (Mathf.Lerp(current.r, c.r, rat),Mathf.Lerp(current.g, c.g, rat),Mathf.Lerp(current.b, c.b, rat),1 );
            }, 0, 1, 5);
			

            Continue();
        }



        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
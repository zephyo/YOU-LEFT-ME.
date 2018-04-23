// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System;

namespace Fungus
{
    /// <summary>
    /// Displays a button in a multiple choice menu.
    /// </summary>
    [CommandInfo("remember",
                 "strobe",
                 "travel")]
    [AddComponentMenu("")]
    public class strobe : Command
    {
        
      
      public float time;

        #region Public members

        public override void OnEnter()
        {
          
			StartCoroutine(manager.instance.Strobe(1,time));

            Continue();
        }



        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
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
                 "black",
                 "travel")]
    [AddComponentMenu("")]
    public class black : Command
    {
        
      
      public bool Blacken;

        #region Public members

        public override void OnEnter()
        {
            

            manager m = GameObject.FindGameObjectWithTag("manager").GetComponent<manager>();

			if (Blacken){
                m.Black();
                m.strobing=true;
            } 
            else m.ForceBlackBegone();

            Continue();
        }

        public override string GetSummary(){
            if (Blacken) return "blacken";
            else return "begone";
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

namespace Fungus
{
    /// <summary>
    /// Displays a button in a multiple choice menu.
    /// </summary>
    [CommandInfo("remember",
                 "suiciending",
                 "travel")]
    [AddComponentMenu("")]
    public class suiciending : Command
    {
        
      
      public endings e;

        #region Public members

        public override void OnEnter()
        {
            manager m = GameObject.FindGameObjectWithTag("manager").GetComponent<manager>();

         if (e==endings.suicide) m.suicideending();
         else if (e==endings.good) m.good();
          else if (e==endings.ghost) m.ghost();


            Continue();
        }



        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
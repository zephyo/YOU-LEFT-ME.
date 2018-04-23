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
                 "GoToScene",
                 "travel")]
    [AddComponentMenu("")]
    public class GoToScene : Command
    {
        
        public scenes before, after;


        #region Public members

        public override void OnEnter()
        {
          

             StartCoroutine(manager.instance.switchscenes(before,after));


            Continue();
        }



        public override string GetSummary()
        {
          
            return before.ToString()+ " : " +after.ToString();
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
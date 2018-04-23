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
                 "Travel menu",
                 "travel")]
    [AddComponentMenu("")]
    public class TravelMenu : Command
    {
        [Tooltip("Text to display on the menu button")]
        [SerializeField]
        protected string text = "Option Text";

        public scenes before, after;

        public bool interactable = true;


        #region Public members

        public override void OnEnter()
        {
          

            var menuDialog = MenuDialog.GetMenuDialog();
            if (menuDialog != null)
            {
                menuDialog.SetActive(true);

                var flowchart = GetFlowchart();
                string displayText = flowchart.SubstituteVariables(text);

                menuDialog.AddOption(displayText, interactable, ()=>{
                        

                        StartCoroutine(manager.instance.switchscenes(before,after));

                });
            }


            Continue();
        }



        public override string GetSummary()
        {
          
            return text + " : " + before.ToString()+ " : " +after.ToString();
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
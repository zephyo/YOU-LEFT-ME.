// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections.Generic;

namespace Fungus
{
    /// <summary>
    /// Displays a button in a multiple choice menu.
    /// </summary>
    [CommandInfo("remember",
                 "setTime",
                 "travel")]
    [AddComponentMenu("")]
    public class setTime : Command
    {

        public int addhours = 0;

        #region Public members

        public override void OnEnter()
        {

            manager m = GameObject.FindGameObjectWithTag("manager").GetComponent<manager>();
            DateTime dt = m.getTime();
            if (dt.Hour + addhours < 24)
            {
                dt = dt.AddHours(addhours);
            }

            PlayerPrefs.SetString("time", dt.ToString("M/d/yyyy h:mm tt"));

            m.setTime();

            Continue();
        }



        public override string GetSummary()
        {

            return "";
        }

        public override Color GetButtonColor()
        {
            return new Color32(184, 210, 235, 255);
        }

        #endregion


    }
}
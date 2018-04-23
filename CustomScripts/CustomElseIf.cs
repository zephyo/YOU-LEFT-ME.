// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.
    /// </summary>
    [CommandInfo("Flow", 
                 "Custom Else If", 
                 "Marks the start of a command block to be executed when the preceding If statement is False and the test expression is true.")]
    [AddComponentMenu("")]
    public class CustomElseIf : Condition
    {
        protected override bool IsElseIf { get { return true; } }



        #region Public members

		public string key;
		public int value;


        public override bool OpenBlock()
        {
            return true;
        }

        public override bool CloseBlock()
        {
            return true;
        }


		protected override bool EvaluateCondition(){
				
            bool condition = false;
          
           
           
      
                condition = PlayerPrefs.GetInt(key,0)>value;
        
           
            return condition;
		}
		 
          protected override bool HasNeededProperties()
        {
            return (key.ToString() != "");
        } 

        public override Color GetButtonColor()
        {
            return new Color32(253, 253, 150, 255);
        }

        #endregion
    }
}
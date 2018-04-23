using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
  [CommandInfo("Flow", 
                 "CustomIf2", 
                 "If the test expression is true, execute the following command block.")]
    [AddComponentMenu("")]
public class CustomIf2 :  Condition
    {
        #region Public members

		public string key;
		public int value;

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


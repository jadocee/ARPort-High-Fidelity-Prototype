using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;


    public class TimedDialog : DialogController
    {
        // Start is called before the first frame update
        private IEnumerator<WaitForSeconds> AlertMessage()
        {
            yield return new WaitForSeconds(8);
            OpenOkayDialog("Attention User", "Attention Gate A is now boarding. Please make your way to the boarding area");
            
        }
        
        private void Awake()
        {
            StartCoroutine(AlertMessage());
  
        }
    }

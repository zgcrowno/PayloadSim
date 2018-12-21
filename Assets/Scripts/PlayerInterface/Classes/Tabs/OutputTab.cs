using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputTab : TextInputTab {

    public const string HeaderText = "OUTPUT";
    
	new void Start() {
        base.Start();
        iField.onValueChanged.AddListener(delegate { StartCoroutine(UpdateScrollbarValue()); });
        headerText.text = HeaderText;iField.readOnly = true;
    }

    public void FormulateOutput(string str)
    {
        //Temporary behavior until inputProcessing class is implemented
        if(!iField.text.Equals(""))
        {
            iField.text += "\n\n";
        }
        iField.text += "The input [" + str + "] is not a recognized command.";

        if(!superTab.IsFrontmost())
        {
            seen = false;
        }
    }

    public IEnumerator UpdateScrollbarValue()
    {
        yield return new WaitForEndOfFrame();

        if (iField.verticalScrollbar.IsActive())
        {
            //For some reason, we have to set the value to 0 first in order to truly execute the behavior associated with setting the value to 1 the first time the scrollbar becomes active
            iField.verticalScrollbar.value = 0;
            iField.verticalScrollbar.value = 1;
        }
    }
}

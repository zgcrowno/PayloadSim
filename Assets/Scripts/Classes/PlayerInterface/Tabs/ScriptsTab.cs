using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsTab : TextInputTab {

    public const string HeaderText = "SCRIPTS";
    
	new public void Start() {
        base.Start();
        headerText.text = HeaderText;
    }
}

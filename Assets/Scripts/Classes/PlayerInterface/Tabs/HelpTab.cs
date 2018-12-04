using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTab : ObjectTab {

    public const string HeaderText = "HELP";
    
	new public void Start() {
        base.Start();
        headerText.text = HeaderText;
    }
}

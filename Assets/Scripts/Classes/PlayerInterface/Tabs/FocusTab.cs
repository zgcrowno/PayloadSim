using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusTab : SubTab {

    public const string HeaderText = "FOCUS";
    
	new public void Start () {
        base.Start();
        headerText.text = HeaderText;
    }
}

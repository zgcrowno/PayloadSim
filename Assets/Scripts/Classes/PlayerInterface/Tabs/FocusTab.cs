using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusTab : ObjectTab {

    public const string HeaderText = "FOCUS";
    
	new public void Start () {
        objectCamera = GameObject.Find("FocusCamera").GetComponent<Camera>();
        headerText.text = HeaderText;
        base.Start();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTab : ObjectTab {

    public const string HeaderText = "LEVELS";
    
	new public void Start() {
        objectCamera = GameObject.Find("LevelsCamera").GetComponent<Camera>();
        headerText.text = HeaderText;
        base.Start();
    }
}

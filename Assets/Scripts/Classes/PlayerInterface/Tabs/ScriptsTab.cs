using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsTab : SubTab {

    public const string HeaderText = "SCRIPTS";

    new public void Awake()
    {
        base.Awake();
        headerText = HeaderText;
    }
    
	new public void Start () {
        base.Start();
    }
	
	new public void Update () {
		
	}

    public override void Draw()
    {
        base.Draw();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputTab : SubTab {

    public const string HeaderText = "OUTPUT";

    new void Awake()
    {
        base.Awake();
        headerText = HeaderText;
    }
    
	new void Start () {
        base.Start();
    }
	
	new void Update () {
		
	}

    public override void Draw()
    {
        base.Draw();
    }
}

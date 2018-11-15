using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTab : SubTab {

    public const string HeaderText = "NOTES";

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

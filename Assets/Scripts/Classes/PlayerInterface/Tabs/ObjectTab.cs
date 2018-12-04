using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTab : SubTab {

	// Use this for initialization
	new public void Start () {
        base.Start();
        SetUp(new Vector2(superTab.brt.anchoredPosition.x, superTab.brt.anchoredPosition.y), new Vector2(superTab.brt.sizeDelta.x, superTab.brt.sizeDelta.y));
    }
	
	// Update is called once per frame
	new public void Update () {
		
	}
}

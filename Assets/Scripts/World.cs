using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public int numTabs = 8;

    public GameObject[] tabs = new GameObject[8];

	// Use this for initialization
	void Start () {
        //Initialize the Tab objects
		for(int i = 0; i < numTabs; i++)
        {
            tabs[i] = new GameObject("Tab" + i);
            Tab tab = tabs[i].AddComponent<Tab>();
            tab.depth = tabs.Length - 1 - i;
            tab.headerRect = new Rect(i * (Screen.width / 8), 0, Screen.width / 8, Screen.height / 20);
            tab.bodyRect = new Rect(0, Screen.height / 20, Screen.width, Screen.height - (Screen.height / 20));
            tab.prevPos.Push(new Vector2(tab.headerRect.x, tab.headerRect.y));
            tab.prevPos.Push(new Vector2(tab.bodyRect.x, tab.bodyRect.y));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method which sets one tab's hasFocus variable to true, while setting all other tabs' hasFocus variable to false
    public void SetTabDepths(Tab tab)
    {
        int otherTabDepth = 1;
        for(int i = tabs.Length - 1; i >= 0; i--)
        {
            if (tabs[i].name.Equals(tab.gameObject.name))
            {
                tabs[i].GetComponent<Tab>().depth = 0;
            }
            else
            {
                tabs[i].GetComponent<Tab>().depth = otherTabDepth;
                otherTabDepth++;
            }
        }
    }

    public Tab GetTabByDepth(int depth)
    {
        foreach(GameObject tabObject in tabs)
        {
            Tab tab = tabObject.GetComponent<Tab>();
            if(tab.depth == depth)
            {
                return tab;
            }
        }
        return null;
    }
}

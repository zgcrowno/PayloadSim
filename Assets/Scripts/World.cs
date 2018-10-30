using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public int numTabs = 8;

    public List<SuperTab> superTabs = new List<SuperTab>();

	// Use this for initialization
	void Start () {
        //Initialize the Tab objects
		for(int i = 0; i < numTabs; i++)
        {
            GameObject superTabObject = new GameObject("SuperTab" + i);
            SuperTab superTab = superTabObject.AddComponent<SuperTab>();
            superTab.depth = i;
            superTab.placement = i;
            superTab.SetUpWholeRect(0, 0, Screen.width, Screen.height);
            superTab.subTabs = new List<SubTab>();
            superTabs.Add(superTab);

            GameObject subTabObject = new GameObject("SubTab" + i);
            SubTab subTab = subTabObject.AddComponent<SubTab>();
            subTab.superTab = superTab;
            subTab.quadrants = new Rect[4];
            for(int j = 0; j < 4; j++)
            {
                subTab.quadrants[j] = new Rect();
            }
            subTab.SetUpWholeRect(superTab.wholeRect.x, superTab.wholeRect.y + superTab.headerRect.height, superTab.wholeRect.width, superTab.wholeRect.height - superTab.headerRect.height);
            superTab.subTabs.Add(subTab);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method which sets one SuperTab's depth is set to the lowest, and all others are made greater than
    public void SetSuperTabToShallow(SuperTab superTab)
    {
        int otherSuperTabDepth = 1;
        for(int i = superTabs.Count - 1; i >= 0; i--)
        {
            if (superTabs[i].name.Equals(superTab.gameObject.name))
            {
                superTabs[i].depth = 0;
            }
            else
            {
                superTabs[i].depth = otherSuperTabDepth;
                otherSuperTabDepth++;
            }
        }
    }

    public SuperTab GetSuperTabByDepth(int depth)
    {
        foreach(SuperTab superTab in superTabs)
        {
            if(superTab.depth == depth)
            {
                return superTab;
            }
        }
        return null;
    }
}

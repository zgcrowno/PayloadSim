using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour {

    public const int MaxSuperTabs = 8;

    public List<SuperTab> superTabs = new List<SuperTab>();

    // Use this for initialization
    void Start () {
        //Initialize the Tab objects for testing purposes
        int depth = 0;
        for (int i = 0; i < SuperTab.MaxSubTabs; i++)
        {
            SuperTab superTab = new GameObject("SuperTab" + i).AddComponent<SuperTab>();
            superTab.subTabs = new List<SubTab>();
            superTab.headerText = "Some Text" + i;
            superTabs.Add(superTab);
            
            SubTab subTab = new GameObject("SubTab" + i).AddComponent<SubTab>();
            subTab.superTab = superTab;
            subTab.headerText = "More Text" + i;
            subTab.quadrants = new Rect[4];
            for (int j = 0; j < 4; j++)
            {
                subTab.quadrants[j] = new Rect();
            }
            superTab.subTabs.Add(subTab);

            subTab.depth = depth;
            depth++;
            superTab.depth = depth;
            depth++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Method by which SuperTab headers are organized so as to be always next to one another, thus disallowing dead space amongst SuperTab headers
    public void OrganizeSuperTabHeaders()
    {
        foreach (SuperTab superTab in superTabs)
        {
            superTab.headerRect.x = superTab.wholeRect.x + (GetSuperTabIndex(superTab) * superTab.headerRect.width);
        }
    }

    //Method by which a SuperTab's depth is set to anywhere from 0-7, these ints corresponding to the depth of the passed SuperTab
    //relative to all others, and all SuperTabs possessing a depth >= the passed value are incremented incidentally
    public void SetSuperTabToDepth(SuperTab superTab, int depth)
    {
        SuperTab SuperTabCurrentlyAtPassedDepth = GetSuperTabByDepth(depth);
        List<SuperTab> SuperTabsBelowThatCurrentlyAtPassedDepth = GetSuperTabsBelow(SuperTabCurrentlyAtPassedDepth);

        int startingDepth = 0;

        foreach (SuperTab supt in SuperTabsBelowThatCurrentlyAtPassedDepth)
        {
            startingDepth++;

            foreach(SubTab subt in supt.subTabs)
            {
                startingDepth++;
            }
        }

        foreach(SubTab subt in superTab.subTabs)
        {
            subt.depth = startingDepth;
            startingDepth++;
        }

        superTab.depth = startingDepth;
        startingDepth++;

        foreach(SuperTab st in superTabs)
        {
            if(st != superTab && !SuperTabsBelowThatCurrentlyAtPassedDepth.Contains(st))
            {
                foreach(SubTab subt in st.subTabs)
                {
                    subt.depth = startingDepth;
                    startingDepth++;
                }

                st.depth = startingDepth;
                startingDepth++;
            }
        }
    }

    //Returns the index of the passed SuperTab
    public int GetSuperTabIndex(SuperTab superTab)
    {
        return superTabs.IndexOf(superTab);
    }

    //Returns the SuperTab whose depth is anywhere from 0-7, these ints corresponding to the depth of the passed SuperTab
    //relative to all others.
    public SuperTab GetSuperTabByDepth(int depth)
    {
        SuperTab superTabToReturn = null;

        foreach(SuperTab superTab in superTabs)
        {
            List<SuperTab> superTabsBelow = GetSuperTabsBelow(superTab);
            if(superTabsBelow.Count == depth)
            {
                superTabToReturn = superTab;
            }
        }

        return superTabToReturn;
    }

    //Returns a list containing all of those SuperTabs which have a depth less than the SuperTab passed as a parameter
    public List<SuperTab> GetSuperTabsBelow(SuperTab superTab)
    {
        List<SuperTab> superTabsBelow = new List<SuperTab>();
        foreach(SuperTab st in superTabs)
        {
            if(st.depth < superTab.depth)
            {
                superTabsBelow.Add(st);
            }
        }
        return superTabsBelow;
    }
}

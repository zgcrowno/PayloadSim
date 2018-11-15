using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour {

    public const int MaxSuperTabs = 8;

    public List<SuperTab> superTabs = new List<SuperTab>();
    public Texture2D resizeHorizontalCursor;
    public Texture2D resizeVerticalCursor;
    public Texture2D resizeUpLeftCursor;
    public Texture2D resizeUpRightCursor;
    
    void Start () {
        //Initialize the Tab objects for testing purposes
        int depth = 0;
        for(int i = 0; i < MaxSuperTabs; i++)
        {
            SuperTab superTab = new GameObject().AddComponent<SuperTab>();
            superTabs.Add(superTab);
            SubTab subTab = new SubTab();
            switch(i)
            {
                case 0:
                    subTab = new GameObject("InputSubTab").AddComponent<InputTab>();
                    superTab.gameObject.name = "InputSuperTab";
                    break;
                case 1:
                    subTab = new GameObject("OutputSubTab").AddComponent<OutputTab>();
                    superTab.gameObject.name = "OutputSuperTab";
                    break;
                case 2:
                    subTab = new GameObject("FocusSubTab").AddComponent<FocusTab>();
                    superTab.gameObject.name = "FocusSuperTab";
                    break;
                case 3:
                    subTab = new GameObject("LevelsSubTab").AddComponent<LevelsTab>();
                    superTab.gameObject.name = "LevelsSuperTab";
                    break;
                case 4:
                    subTab = new GameObject("ScriptsSubTab").AddComponent<ScriptsTab>();
                    superTab.gameObject.name = "ScriptsSuperTab";
                    break;
                case 5:
                    subTab = new GameObject("ProgramSubTab").AddComponent<ProgramTab>();
                    superTab.gameObject.name = "ProgramSuperTab";
                    break;
                case 6:
                    subTab = new GameObject("NotesSubTab").AddComponent<NotesTab>();
                    superTab.gameObject.name = "NotesSuperTab";
                    break;
                case 7:
                    subTab = new GameObject("HelpSubTab").AddComponent<HelpTab>();
                    superTab.gameObject.name = "HelpSuperTab";
                    break;
            }
            subTab.superTab = superTab;
            superTab.headerText = subTab.headerText;
            superTab.subTabs.Add(subTab);
            subTab.depth = depth;
            depth++;
            superTab.depth = depth;
            depth++;
        }

        //Set up various cursor textures
        resizeHorizontalCursor = (Texture2D)Resources.Load("Textures/resizeHorizontal");
        resizeVerticalCursor = (Texture2D)Resources.Load("Textures/resizeVertical");
        resizeUpLeftCursor = (Texture2D)Resources.Load("Textures/resizeUpLeft");
        resizeUpRightCursor = (Texture2D)Resources.Load("Textures/resizeUpRight");
    }
	
	void Update () {
		
	}

    public void OnGUI()
    {
        SetAppropriateCursor();
    }
    
    /*
     * Method by which SuperTab headers are organized so as to be always next to one another, thus disallowing dead space amongst SuperTab headers
     */
    public void OrganizeSuperTabHeaders()
    {
        foreach (SuperTab superTab in superTabs)
        {
            superTab.headerRect.x = superTab.wholeRect.x + (GetSuperTabIndex(superTab) * superTab.headerRect.width);
        }
    }

    /*
     * Method by which a SuperTab's depth is set to anywhere from 0-7, these ints corresponding to the depth of the passed SuperTab relative to all others, and all SuperTabs possessing a depth >= the passed value are incremented incidentally
     * @param superTab The SuperTab whose depth we're setting
     * @param depth The depth to which we're setting the passed SuperTab
     */
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

    /*
     * Returns the index of the passed SuperTab in superTabs
     * @param superTab The SuperTab whose index we're returning
     * @return The index of the passed SuperTab in superTabs
     */
    public int GetSuperTabIndex(SuperTab superTab)
    {
        return superTabs.IndexOf(superTab);
    }
    
    /*
     * Returns the SuperTab whose depth is anywhere from 0-7, these ints corresponding to the depth of the passed SuperTab relative to all others
     * @param depth The depth by which we're grabbing the SuperTab
     * @return The SuperTab whose depth corresponds to the passed depth
     */
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

    /*
    * Returns a list containing all of those SuperTabs which have a depth less than the SuperTab passed as a parameter
    * @param superTab The SuperTab below whose depth we're grabbing all of the other SuperTabs
    * @return A list of SuperTabs whose depths are less than the passed SuperTab
    */
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

    /*
    * Ensures that the mouse cursor is set to the Texture2D object appropriate for the occasion
    */
    public void SetAppropriateCursor()
    {
        SuperTab currentSuperTab = GetSuperTabByDepth(0);
        Texture2D cursorToUse = null;
        Vector2 hotSpot = Vector2.zero;

        foreach(SubTab subTab in currentSuperTab.subTabs)
        {
            if(!subTab.headerRect.Contains(Event.current.mousePosition))
            {
                if (subTab.resizeRects[0].Contains(Event.current.mousePosition)
                    || subTab.resizeRects[1].Contains(Event.current.mousePosition)
                    || subTab.resizingWhat == SubTab.Left
                    || subTab.resizingWhat == SubTab.Right)
                {
                    //Left or right
                    cursorToUse = resizeHorizontalCursor;
                    hotSpot = new Vector2(resizeHorizontalCursor.width / 2, 5);
                }
                else if (subTab.resizeRects[2].Contains(Event.current.mousePosition)
                         || subTab.resizeRects[3].Contains(Event.current.mousePosition)
                         || subTab.resizingWhat == SubTab.Upper
                         || subTab.resizingWhat == SubTab.Lower)
                {
                    //Top or bottom
                    cursorToUse = resizeVerticalCursor;
                    hotSpot = new Vector2(5, resizeVerticalCursor.height / 2);
                }
                else if (subTab.resizeRects[4].Contains(Event.current.mousePosition)
                         || subTab.resizeRects[6].Contains(Event.current.mousePosition)
                         || subTab.resizingWhat == SubTab.LowerLeft
                         || subTab.resizingWhat == SubTab.UpperRight)
                {
                    //Bottom-left or top-right
                    cursorToUse = resizeUpRightCursor;
                    hotSpot = new Vector2(11.5f, 11.5f);
                }
                else if (subTab.resizeRects[5].Contains(Event.current.mousePosition)
                         || subTab.resizeRects[7].Contains(Event.current.mousePosition)
                         || subTab.resizingWhat == SubTab.UpperLeft
                         || subTab.resizingWhat == SubTab.LowerRight)
                {
                    //Top-left or bottom-right
                    cursorToUse = resizeUpLeftCursor;
                    hotSpot = new Vector2(11.5f, 11.5f);
                }
            }
        }

        Cursor.SetCursor(cursorToUse, hotSpot, CursorMode.Auto);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour {

    public const int MinSuperTabs = 1;
    public const int MaxSuperTabs = 8;

    public List<SuperTab> superTabs = new List<SuperTab>(MaxSuperTabs);

    public Canvas canvas;
    public Texture2D resizeHorizontalCursor;
    public Texture2D resizeVerticalCursor;
    public Texture2D resizeUpLeftCursor;
    public Texture2D resizeUpRightCursor;
    
    void Start () {
        //Load the canvas
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();

        //Initialize the Tab objects for testing purposes
        for(int i = 0; i < MaxSuperTabs; i++)
        {
            GameObject superTab = Instantiate(Resources.Load("Prefabs/SuperTabPrefab") as GameObject);
            superTabs.Add(superTab.GetComponent<SuperTab>());
            GameObject subTab = new GameObject();
            switch (i)
            {
                case 0:
                    subTab = Instantiate(Resources.Load("Prefabs/InputTabPrefab") as GameObject);
                    subTab.gameObject.name = "InputSubTab";
                    superTab.gameObject.name = "InputSuperTab";
                    superTabs[i].headerText.text = InputTab.HeaderText;
                    break;
                case 1:
                    subTab = Instantiate(Resources.Load("Prefabs/OutputTabPrefab") as GameObject);
                    subTab.gameObject.name = "OutputSubTab";
                    superTab.gameObject.name = "OutputSuperTab";
                    superTabs[i].headerText.text = OutputTab.HeaderText;
                    break;
                case 2:
                    subTab = Instantiate(Resources.Load("Prefabs/FocusTabPrefab") as GameObject);
                    subTab.gameObject.name = "FocusSubTab";
                    superTab.gameObject.name = "FocusSuperTab";
                    superTabs[i].headerText.text = FocusTab.HeaderText;
                    break;
                case 3:
                    subTab = Instantiate(Resources.Load("Prefabs/LevelsTabPrefab") as GameObject);
                    subTab.gameObject.name = "LevelsSubTab";
                    superTab.gameObject.name = "LevelsSuperTab";
                    superTabs[i].headerText.text = LevelsTab.HeaderText;
                    break;
                case 4:
                    subTab = Instantiate(Resources.Load("Prefabs/ScriptsTabPrefab") as GameObject);
                    subTab.gameObject.name = "ScriptsSubTab";
                    superTab.gameObject.name = "ScriptsSuperTab";
                    superTabs[i].headerText.text = ScriptsTab.HeaderText;
                    break;
                case 5:
                    subTab = Instantiate(Resources.Load("Prefabs/ProgramTabPrefab") as GameObject);
                    subTab.gameObject.name = "ProgramSubTab";
                    superTab.gameObject.name = "ProgramSuperTab";
                    superTabs[i].headerText.text = ProgramTab.HeaderText;
                    break;
                case 6:
                    subTab = Instantiate(Resources.Load("Prefabs/NotesTabPrefab") as GameObject);
                    subTab.gameObject.name = "NotesSubTab";
                    superTab.gameObject.name = "NotesSuperTab";
                    superTabs[i].headerText.text = NotesTab.HeaderText;
                    break;
                case 7:
                    subTab = Instantiate(Resources.Load("Prefabs/HelpTabPrefab") as GameObject);
                    subTab.gameObject.name = "HelpSubTab";
                    superTab.gameObject.name = "HelpSuperTab";
                    superTabs[i].headerText.text = HelpTab.HeaderText;
                    break;
            }
            subTab.GetComponent<SubTab>().superTab = superTab.GetComponent<SuperTab>();
            superTab.GetComponent<SuperTab>().subTabs.Add(subTab.GetComponent<SubTab>());
        }

        //Set up various cursor textures
        resizeHorizontalCursor = (Texture2D)Resources.Load("Textures/resizeHorizontal");
        resizeVerticalCursor = (Texture2D)Resources.Load("Textures/resizeVertical");
        resizeUpLeftCursor = (Texture2D)Resources.Load("Textures/resizeUpLeft");
        resizeUpRightCursor = (Texture2D)Resources.Load("Textures/resizeUpRight");
    }

    void FixedUpdate()
    {
        SetAppropriateCursor();
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
     * Sets all of the PlayerInterface's SuperTabs' headers' texts to their appropriate, context-sensitive values
     */ 
    public void SetHeaderTexts()
    {
        foreach(SuperTab superTab in superTabs)
        {
            superTab.SetHeaderText();
        }
    }

    /*
     * Method by which SuperTab headers are organized so as to be always next to one another, thus disallowing dead space amongst SuperTab headers
     */
    public void OrganizeSuperTabHeaders()
    {
        foreach (SuperTab superTab in superTabs)
        {
            superTab.hrt.anchoredPosition = new Vector2(superTab.rt.anchoredPosition.x + (GetSuperTabIndex(superTab) * superTab.hrt.sizeDelta.x), superTab.hrt.anchoredPosition.y);
        }
    }

    /*
    * Ensures that the mouse cursor is set to the Texture2D object appropriate for the occasion
    */
    public void SetAppropriateCursor()
    {
        if (canvas.transform.childCount > 0) {
            SuperTab currentSuperTab = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject.GetComponent<SuperTab>();
            Texture2D cursorToUse = null;
            Vector2 hotSpot = Vector2.zero;

            foreach (SubTab subTab in currentSuperTab.subTabs)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(subTab.hrt, Input.mousePosition))
                {
                    if (subTab.resizeRects[0].Contains(Input.mousePosition)
                        || subTab.resizeRects[1].Contains(Input.mousePosition)
                        || subTab.resizingWhat == SubTab.Left
                        || subTab.resizingWhat == SubTab.Right)
                    {
                        //Left or right
                        cursorToUse = resizeHorizontalCursor;
                        hotSpot = new Vector2(resizeHorizontalCursor.width / 2, 5);
                    }
                    else if (subTab.resizeRects[2].Contains(Input.mousePosition)
                             || subTab.resizeRects[3].Contains(Input.mousePosition)
                             || subTab.resizingWhat == SubTab.Upper
                             || subTab.resizingWhat == SubTab.Lower)
                    {
                        //Top or bottom
                        cursorToUse = resizeVerticalCursor;
                        hotSpot = new Vector2(5, resizeVerticalCursor.height / 2);
                    }
                    else if (subTab.resizeRects[4].Contains(Input.mousePosition)
                             || subTab.resizeRects[6].Contains(Input.mousePosition)
                             || subTab.resizingWhat == SubTab.LowerLeft
                             || subTab.resizingWhat == SubTab.UpperRight)
                    {
                        //Bottom-left or top-right
                        cursorToUse = resizeUpRightCursor;
                        hotSpot = new Vector2(11.5f, 11.5f);
                    }
                    else if (subTab.resizeRects[5].Contains(Input.mousePosition)
                             || subTab.resizeRects[7].Contains(Input.mousePosition)
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
}

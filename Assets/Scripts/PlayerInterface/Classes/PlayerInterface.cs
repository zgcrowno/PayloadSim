using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour {

    public const int MinSuperTabs = 1;
    public const int MaxSuperTabs = 8;

    public List<SuperTab> superTabs = new List<SuperTab>(MaxSuperTabs);

    public Canvas canvas;
    
    void Start () {
        //Load the canvas
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();

        //Initialize the Tab objects for testing purposes
        for(int i = 0; i < MaxSuperTabs; i++)
        {
            GameObject superTab = Instantiate(Resources.Load("Prefabs/PlayerInterface/BottomLeftPrefab") as GameObject);
            superTab.AddComponent<SuperTab>();
            superTabs.Add(superTab.GetComponent<SuperTab>());
            GameObject subTab = Instantiate(Resources.Load("Prefabs/PlayerInterface/BottomLeftPrefab") as GameObject);
            switch (i)
            {
                case 0:
                    subTab.AddComponent<InputTab>();
                    subTab.gameObject.name = "InputSubTab";
                    superTab.gameObject.name = "InputSuperTab";
                    superTabs[i].headerText.text = InputTab.HeaderText;
                    break;
                case 1:
                    subTab.AddComponent<OutputTab>();
                    subTab.gameObject.name = "OutputSubTab";
                    superTab.gameObject.name = "OutputSuperTab";
                    superTabs[i].headerText.text = OutputTab.HeaderText;
                    break;
                case 2:
                    subTab.AddComponent<FocusTab>();
                    subTab.gameObject.name = "FocusSubTab";
                    superTab.gameObject.name = "FocusSuperTab";
                    superTabs[i].headerText.text = FocusTab.HeaderText;
                    break;
                case 3:
                    subTab.AddComponent<LevelsTab>();
                    subTab.gameObject.name = "LevelsSubTab";
                    superTab.gameObject.name = "LevelsSuperTab";
                    superTabs[i].headerText.text = LevelsTab.HeaderText;
                    break;
                case 4:
                    subTab.AddComponent<ScriptsTab>();
                    subTab.gameObject.name = "ScriptsSubTab";
                    superTab.gameObject.name = "ScriptsSuperTab";
                    superTabs[i].headerText.text = ScriptsTab.HeaderText;
                    break;
                case 5:
                    subTab.AddComponent<ProgramTab>();
                    subTab.gameObject.name = "ProgramSubTab";
                    superTab.gameObject.name = "ProgramSuperTab";
                    superTabs[i].headerText.text = ProgramTab.HeaderText;
                    break;
                case 6:
                    subTab.AddComponent<NotesTab>();
                    subTab.gameObject.name = "NotesSubTab";
                    superTab.gameObject.name = "NotesSuperTab";
                    superTabs[i].headerText.text = NotesTab.HeaderText;
                    break;
                case 7:
                    subTab.AddComponent<HelpTab>();
                    subTab.gameObject.name = "HelpSubTab";
                    superTab.gameObject.name = "HelpSuperTab";
                    superTabs[i].headerText.text = HelpTab.HeaderText;
                    break;
            }
            subTab.GetComponent<SubTab>().superTab = superTab.GetComponent<SuperTab>();
            superTab.GetComponent<SuperTab>().subTabs.Add(subTab.GetComponent<SubTab>());
        }
    }

    /*
     * Returns the SubTab of the designated type
     * @param type The type of SubTab for which we're searching
     * @return The SubTab of the designated type
     */ 
    public SubTab GetSubTabByType(Type type)
    {
        foreach (SuperTab superTab in superTabs)
        {
            foreach (SubTab subTab in superTab.subTabs)
            {
                if (subTab.GetType() == type)
                {
                    return subTab;
                }
            }
        }

        return null;
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
}

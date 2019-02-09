using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InputTab : TextInputTab
{

    public const string HeaderText = "INPUT";

    public List<string> prevInputs = new List<string>();

    public bool connected; // This bool represents whether or not the player is currently connected to any device on the network via their InputTab

    new public void Start()
    {
        base.Start();
        headerText.text = HeaderText;
        iField.lineType = TMP_InputField.LineType.MultiLineSubmit;
        iField.onSubmit.AddListener(ExecuteInput);
        iField.onValueChanged.AddListener(ProcessInput);
    }

    public void ProcessInput(string str)
    {
        str = str.Trim().ToLower();

        string[] strArr = str.Split(new char[] {Delimiter.Space}, Command.MaxElements);
        
        if(!connected)
        {
            if(strArr.Length >= 1)
            {
                if(strArr[0].Equals(Command.Bypass))
                {
                    if(strArr.Length >= 2)
                    {
                        foreach(Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                        {
                            if(hackable.name.Length >= strArr[1].Length && hackable.name.ToLower().Substring(0, strArr[1].Length).Equals(strArr[1]))
                            {
                                hackable.nameIcon.SetActive(true);
                            }
                            else
                            {
                                hackable.nameIcon.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                        {
                            hackable.nameIcon.SetActive(true);
                        }
                    }
                }
                else
                {
                    foreach (Clickable hackable in pi.GetAllObjectsOfType(typeof(IHackable)))
                    {
                        hackable.nameIcon.SetActive(false);
                    }
                }
            }
        }
        else
        {

        }
    }

    public void ExecuteInput(string str)
    {
        OutputTab outputTab = (OutputTab)pi.GetSubTabByType(typeof(OutputTab));

        str = str.Trim().ToLower();

        prevInputs.Add(str);
        iField.text = "";

        outputTab.FormulateOutput(str);

        iField.ActivateInputField();
    }
}

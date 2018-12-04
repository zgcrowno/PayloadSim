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

    new public void Start()
    {
        base.Start();
        headerText.text = HeaderText;
        iField.lineType = TMP_InputField.LineType.MultiLineSubmit;
        iField.onSubmit.AddListener(ProcessInput);
    }

    public void ProcessInput(string str)
    {
        OutputTab outputTab = (OutputTab) pi.GetSubTabByType(typeof(OutputTab));

        prevInputs.Add(str);
        iField.text = "";

        outputTab.FormulateOutput(str);

        iField.ActivateInputField();
    }
}

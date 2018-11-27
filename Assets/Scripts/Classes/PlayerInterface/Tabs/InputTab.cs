using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTab : SubTab
{

    public const string HeaderText = "INPUT";

    public GameObject iField;

    new public void Start()
    {
        base.Start();
        headerText.text = HeaderText;
        iField = Instantiate(Resources.Load("Prefabs/InputFieldPrefab") as GameObject);
        iField.transform.SetParent(body.transform);
    }
}
